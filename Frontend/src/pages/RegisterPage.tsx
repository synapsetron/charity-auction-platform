import { Container, Row, Col, Form, Button } from 'react-bootstrap';
import { FaFacebook, FaGoogle } from 'react-icons/fa';
import { Link } from 'react-router-dom';

const RegisterPage = () => {
  return (
    <section className="py-5 position-relative" style={{ backgroundColor: '#f0f2f5', minHeight: '100vh' }}>
      <div className="position-absolute top-50 start-0 translate-middle bg-success rounded-circle opacity-25" style={{ width: '300px', height: '300px', filter: 'blur(100px)' }}></div>

      <div className="py-5 bg-dark text-white">
        <Container>
          <h3 className="mb-3">Реєстрація</h3>
          <div className="d-flex align-items-center gap-3">
            <h5 className="text-success fw-normal">Головна</h5>
            <h5 className="text-white fw-normal">/</h5>
            <h5 className="text-white fw-normal">Реєстрація</h5>
          </div>
        </Container>
      </div>

      <Container className="my-5">
        <Row className="justify-content-center">
          <Col md={6} lg={5}>
            <div className="bg-white p-4 rounded shadow">
              <div className="text-center mb-4">
                <h5>Реєстрація</h5>
                <p className="mt-2">
                  Вже маєш акаунт? <Link to="/login">Увійти</Link>
                </p>
              </div>
              <Form>
                <Form.Group className="mb-3" controlId="formUsername">
                  <Form.Label>Ім’я користувача *</Form.Label>
                  <Form.Control type="text" placeholder="Введіть ім’я" required />
                </Form.Group>

                <Form.Group className="mb-3" controlId="formEmail">
                  <Form.Label>Email *</Form.Label>
                  <Form.Control type="email" placeholder="Введіть email" required />
                </Form.Group>

                <Form.Group className="mb-3" controlId="formPassword">
                  <Form.Label>Пароль *</Form.Label>
                  <Form.Control type="password" placeholder="Введіть пароль" required />
                </Form.Group>

                <Form.Group className="mb-3" controlId="formConfirmPassword">
                  <Form.Label>Підтвердіть пароль *</Form.Label>
                  <Form.Control type="password" placeholder="Повторіть пароль" required />
                </Form.Group>

                <Form.Group className="mb-4 d-flex align-items-center gap-2">
                  <Form.Check type="checkbox" id="terms" />
                  <Form.Label htmlFor="terms" className="mb-0">
                    Я погоджуюсь з умовами використання
                  </Form.Label>
                </Form.Group>

                <Button type="submit" variant="success" className="w-100 rounded-0 mb-3">
                  Створити акаунт
                </Button>
              </Form>

              <div className="text-center border rounded p-3 mt-4">
                <h6 className="mb-3">АБО ЗАРЕЄСТРУЙСЯ ЧЕРЕЗ</h6>
                <div className="d-flex justify-content-center gap-3">
                  <Button variant="danger" className="d-flex align-items-center gap-2">
                    <FaGoogle /> Google
                  </Button>
                  <Button variant="primary" className="d-flex align-items-center gap-2">
                    <FaFacebook /> Facebook
                  </Button>
                </div>
              </div>

              <p className="text-center mt-4">
                Натискаючи кнопку реєстрації, ви погоджуєтесь з
                <span className="text-success text-decoration-underline mx-1">умовами</span> та
                <span className="text-success text-decoration-underline ms-1">політикою конфіденційності</span>.
              </p>
            </div>
          </Col>
        </Row>
      </Container>

      <div className="position-absolute bottom-0 end-0 translate-middle bg-success rounded-circle opacity-25" style={{ width: '300px', height: '300px', filter: 'blur(100px)' }}></div>
    </section>
  );
};

export default RegisterPage;