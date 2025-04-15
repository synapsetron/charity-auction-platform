import { Container, Row, Col, Form, Button } from 'react-bootstrap';
import { FaUserCircle } from 'react-icons/fa';
import { useAuth } from '../context/AuthContext';

const ProfilePage = () => {
  const { user } = useAuth(); 

  if (!user) {
    return (
      <Container className="py-5">
        <h3>Користувач не знайдений. Потрібен вхід.</h3>
      </Container>
    );
  }

  return (
    <Container className="py-5">
      <section className="bg-white p-4 rounded shadow">
        <div className="d-flex align-items-center gap-4 mb-4">
          <FaUserCircle size={96} className="text-secondary" />
          <div>
            <h5 className="mb-0">{user.firstName} {user.lastName}</h5>
            <small className="text-muted">{user.email}</small>
          </div>
        </div>

        <Form>
          <Row className="mb-4">
            <Col md={12}>
              <Form.Label>Повне ім’я</Form.Label>
              <Form.Control type="text" value={`${user.firstName} ${user.lastName}`} readOnly className="text-capitalize" />
            </Col>
          </Row>

          <Row className="mb-4">
            <Col md={6}>
              <Form.Label>Email</Form.Label>
              <Form.Control type="email" value={user.email} disabled />
            </Col>
            <Col md={6}>
              <Form.Label>Роль</Form.Label>
              <Form.Control type="text" value={user.role} readOnly />
            </Col>
          </Row>

          <Row className="mb-4">
            <Col md={12}>
              <Form.Label>Баланс</Form.Label>
              <Form.Control type="text" value={`${user.balance} ₴`} readOnly />
            </Col>
          </Row>

          {/* Можешь добавить кнопку "Оновити профіль" позже */}
        </Form>
      </section>
    </Container>
  );
};

export default ProfilePage;
