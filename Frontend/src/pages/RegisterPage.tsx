import { useState } from 'react';
import { Container, Row, Col, Form, Button } from 'react-bootstrap';


import { Link, useNavigate } from 'react-router-dom';
import { registerUser } from '../api/auth';
import { UserRegisterDTO } from '../types/authTypes';

const RegisterPage = () => {
  const navigate = useNavigate();

  const [formData, setFormData] = useState<UserRegisterDTO>({
    firstName: '',
    lastName: '',
    email: '',
    password: '',
  });

  const [confirmPassword, setConfirmPassword] = useState<string>('');
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);

    if (formData.password !== confirmPassword) {
      setError('Паролі не співпадають');
      return;
    }

    try {
      setLoading(true);
      await registerUser(formData);
      navigate('/login'); // редирект на логін після успішної реєстрації
    } catch (err: any) {
      setError(err.response?.data?.message || 'Не вдалося зареєструватися');
    } finally {
      setLoading(false);
    }
  };

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

              <Form onSubmit={handleSubmit}>
                <Form.Group className="mb-3" controlId="formFirstName">
                  <Form.Label>Ім’я *</Form.Label>
                  <Form.Control 
                    type="text" 
                    name="firstName" 
                    placeholder="Введіть ім’я" 
                    value={formData.firstName} 
                    onChange={handleChange} 
                    required 
                  />
                </Form.Group>

                <Form.Group className="mb-3" controlId="formLastName">
                  <Form.Label>Прізвище *</Form.Label>
                  <Form.Control 
                    type="text" 
                    name="lastName" 
                    placeholder="Введіть прізвище" 
                    value={formData.lastName} 
                    onChange={handleChange} 
                    required 
                  />
                </Form.Group>

                <Form.Group className="mb-3" controlId="formEmail">
                  <Form.Label>Email *</Form.Label>
                  <Form.Control 
                    type="email" 
                    name="email" 
                    placeholder="Введіть email" 
                    value={formData.email} 
                    onChange={handleChange} 
                    required 
                  />
                </Form.Group>

                <Form.Group className="mb-3" controlId="formPassword">
                  <Form.Label>Пароль *</Form.Label>
                  <Form.Control 
                    type="password" 
                    name="password" 
                    placeholder="Введіть пароль" 
                    value={formData.password} 
                    onChange={handleChange} 
                    required 
                  />
                </Form.Group>

                <Form.Group className="mb-3" controlId="formConfirmPassword">
                  <Form.Label>Підтвердіть пароль *</Form.Label>
                  <Form.Control 
                    type="password" 
                    placeholder="Повторіть пароль" 
                    value={confirmPassword} 
                    onChange={e => setConfirmPassword(e.target.value)} 
                    required 
                  />
                </Form.Group>

                {error && <p className="text-danger">{error}</p>}

                <Button type="submit" variant="success" className="w-100 rounded-0 mb-3" disabled={loading}>
                  {loading ? 'Створення акаунта...' : 'Створити акаунт'}
                </Button>
              </Form>

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
