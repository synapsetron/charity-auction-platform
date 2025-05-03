import { useEffect, useState } from 'react';
import { Container, Table, Button, Spinner, Alert } from 'react-bootstrap';
import { getAllUsers, blockUser, unblockUser, deleteUser } from '../../api/adminApi';
import { useAuth } from '../../context/AuthContext';
import { useModal } from '../../hooks/useModal';
import { Modal } from '../common/Modal';
import { useTranslation } from 'react-i18next';

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
  const { t } = useTranslation();
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const {
    isOpen: isDeleteModalOpen,
    data: selectedUser,
    openModal: openDeleteModal,
    closeModal: closeDeleteModal,
  } = useModal<User | null>();

  useEffect(() => {
    if (user?.role === 'Admin') {
      fetchUsers();
    }
  }, [user]);

  const fetchUsers = async () => {
    try {
      const data = await getAllUsers();
      setUsers(data);
    } catch (err) {
      console.error(t('user_management.errors.load'), err);
      setError(t('user_management.errors.load'));
    } finally {
      setLoading(false);
    }
  };

  const handleBlock = async (id: string) => {
    try {
      await blockUser(id);
      await fetchUsers();
    } catch (err) {
      console.error(t('user_management.errors.block'), err);
      setError(t('user_management.errors.block'));
    }
  };

  const handleUnblock = async (id: string) => {
    try {
      await unblockUser(id);
      await fetchUsers();
    } catch (err) {
      console.error(t('user_management.errors.unblock'), err);
      setError(t('user_management.errors.unblock'));
    }
  };

  const confirmDelete = (user: User) => {
    openDeleteModal(user);
  };

  const handleDelete = async () => {
    if (!selectedUser) return;
    try {
      await deleteUser(selectedUser.id);
      await fetchUsers();
    } catch (err) {
      console.error(t('user_management.errors.delete'), err);
      setError(t('user_management.errors.delete'));
    } finally {
      closeDeleteModal();
    }
  };

  const isUserBlocked = (lockoutEnd: string | null) => {
    return lockoutEnd ? new Date(lockoutEnd) > new Date() : false;
  };

  if (loading) {
    return (
      <Container className="py-5 text-center">
        <Spinner animation="border" />
      </Container>
    );
  }

  if (user?.role !== 'Admin') {
    return (
      <Container className="py-5 text-center">
        <h3>{t('user_management.access_denied')}</h3>
      </Container>
    );
  }

  return (
    <Container className="py-4">
      <h4 className="mb-4">{t('user_management.title')}</h4>

      {error && <Alert variant="danger">{error}</Alert>}

      <div style={{ maxHeight: '500px', overflowY: 'auto' }}>
        <Table bordered hover responsive>
          <thead className="table-light">
            <tr>
              <th>{t('user_management.table.first_name')}</th>
              <th>{t('user_management.table.last_name')}</th>
              <th>{t('user_management.table.email')}</th>
              <th>{t('user_management.table.role')}</th>
              <th>{t('user_management.table.status')}</th>
              <th>{t('user_management.table.actions')}</th>
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
                      <span className="text-danger fw-bold">{t('user_management.blocked')}</span>
                    ) : (
                      <span className="text-success fw-bold">{t('user_management.active')}</span>
                    )}
                  </td>
                  <td className="d-flex gap-2">
                    {blocked ? (
                      <Button size="sm" variant="success" onClick={() => handleUnblock(u.id)}>
                        {t('user_management.unblock')}
                      </Button>
                    ) : (
                      <Button size="sm" variant="warning" onClick={() => handleBlock(u.id)}>
                        {t('user_management.block')}
                      </Button>
                    )}
                    <Button size="sm" variant="danger" onClick={() => confirmDelete(u)}>
                      {t('user_management.delete')}
                    </Button>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </Table>
      </div>

      <Modal
        isOpen={isDeleteModalOpen}
        onClose={closeDeleteModal}
        title={t('user_management.confirm_delete_title')}
        confirmText={t('user_management.confirm')}
        cancelText={t('user_management.cancel')}
        showFooter
        onConfirm={handleDelete}
      >
        <p>
          {t('user_management.confirm_delete_text', {
            name: `${selectedUser?.firstName} ${selectedUser?.lastName}`,
          })}
        </p>
      </Modal>
    </Container>
  );
};

export default UserManagement;
