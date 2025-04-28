import { useEffect, useState } from 'react';
import { Container, Table, Button, Spinner, Alert } from 'react-bootstrap';
import { getAllUsers, blockUser, unblockUser, deleteUser } from '../../api/adminApi';
import { useAuth } from '../../context/AuthContext';

interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  role: string;
  lockoutEnd: string | null;
}

const UserManagement = () => {
  const { user } = useAuth();
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  // Завантаження користувачів при завантаженні сторінки
  useEffect(() => {
    if (user?.role === 'admin') {
      fetchUsers();
    }
  }, [user]);

  // Отримання всіх користувачів
  const fetchUsers = async () => {
    try {
      const data = await getAllUsers();
      setUsers(data);
    } catch (err) {
      console.error('Помилка при завантаженні користувачів', err);
      setError('Не вдалося завантажити користувачів.');
    } finally {
      setLoading(false);
    }
  };

  // Блокування користувача
  const handleBlock = async (id: string) => {
    try {
      await blockUser(id);
      await fetchUsers(); // Перезапитати після блокування
    } catch (err) {
      console.error('Помилка при блокуванні', err);
      setError('Не вдалося заблокувати користувача.');
    }
  };

  // Розблокування користувача
  const handleUnblock = async (id: string) => {
    try {
      await unblockUser(id);
      await fetchUsers(); // Перезапитати після розблокування
    } catch (err) {
      console.error('Помилка при розблокуванні', err);
      setError('Не вдалося розблокувати користувача.');
    }
  };

  // Видалення користувача
  const handleDelete = async (id: string) => {
    if (window.confirm('Ви впевнені, що хочете видалити користувача?')) {
      try {
        await deleteUser(id);
        await fetchUsers();
      } catch (err) {
        console.error('Помилка при видаленні', err);
        setError('Не вдалося видалити користувача.');
      }
    }
  };

  // Перевірка чи користувач заблокований
  const isUserBlocked = (lockoutEnd: string | null) => {
    return lockoutEnd ? new Date(lockoutEnd) > new Date() : false;
  };

  // Показати спіннер при завантаженні
  if (loading) {
    return (
      <Container className="py-5 text-center">
        <Spinner animation="border" />
      </Container>
    );
  }

  // Якщо не адмін — заборона доступу
  if (user?.role !== 'admin') {
    return (
      <Container className="py-5 text-center">
        <h3>У вас немає прав доступу</h3>
      </Container>
    );
  }

  return (
    <Container className="py-4">
      <h4 className="mb-4">Управління користувачами</h4>

      {error && <Alert variant="danger">{error}</Alert>}

      {/* Обгортка для скролу */}
      <div style={{ maxHeight: '500px', overflowY: 'auto' }}>
        <Table bordered hover responsive>
          <thead className="table-light">
            <tr>
              <th>Ім'я</th>
              <th>Прізвище</th>
              <th>Email</th>
              <th>Роль</th>
              <th>Статус</th>
              <th>Дії</th>
            </tr>
          </thead>
          <tbody>
            {users.map((u) => {
              const blocked = isUserBlocked(u.lockoutEnd);
              return (
                <tr key={u.id}>
                  <td>{u.firstName}</td>
                  <td>{u.lastName}</td>
                  <td>{u.email}</td>
                  <td>{u.role}</td>
                  <td>
                    {blocked ? (
                      <span className="text-danger fw-bold">Заблокований</span>
                    ) : (
                      <span className="text-success fw-bold">Активний</span>
                    )}
                  </td>
                  <td className="d-flex gap-2">
                    {blocked ? (
                      <Button size="sm" variant="success" onClick={() => handleUnblock(u.id)}>
                        Розблокувати
                      </Button>
                    ) : (
                      <Button size="sm" variant="warning" onClick={() => handleBlock(u.id)}>
                        Заблокувати
                      </Button>
                    )}
                    <Button size="sm" variant="danger" onClick={() => handleDelete(u.id)}>
                      Видалити
                    </Button>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </Table>
      </div>
    </Container>
  );
};

export default UserManagement;
