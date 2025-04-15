import { Container, Row, Col, Form, Button } from 'react-bootstrap';
import { FaUserCircle } from 'react-icons/fa';
const ProfilePage = () => {
  return (
    <Container className="py-5">
      <section className="bg-white p-4 rounded shadow">
        <div className="d-flex align-items-center gap-4 mb-4">
        <FaUserCircle size={96} className="text-secondary" />
          <div>
            <h5 className="mb-0">Соня Бондаренко</h5>
            <small className="text-muted">example@gmail.com</small>
          </div>
        </div>

        <Form>
          <Row className="mb-4">
            <Col md={12}>
              <Form.Label>Повне ім’я</Form.Label>
              <Form.Control type="text" placeholder="Соня Бондаренко" readOnly className="text-capitalize" />
            </Col>
          </Row>

          <Row className="mb-4">
            <Col md={6}>
              <Form.Label>Номер телефону</Form.Label>
              <Form.Control type="text" placeholder="+38 067 123 45 67" />
            </Col>
            <Col md={6}>
              <Form.Label>Email</Form.Label>
              <Form.Control type="email" value="example@gmail.com" disabled />
            </Col>
          </Row>

          <Row className="mb-4">
            <Col md={12}>
              <Form.Label>Роль</Form.Label>
              <Form.Control type="text" placeholder="Адміністратор" required />
            </Col>
          </Row>

          <Row className="mb-4">
            <Col md={12}>
              <Form.Label>Посилання на фото профілю</Form.Label>
              <Form.Control type="text" placeholder="https://site.com/image.jpg" required />
            </Col>
          </Row>

          <Button variant="success" type="submit">Оновити профіль</Button>
        </Form>
      </section>
    </Container>
  );
};

export default ProfilePage;
