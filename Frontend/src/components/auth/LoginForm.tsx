import { Form, Button } from 'react-bootstrap';
import { FaFacebook, FaGoogle } from 'react-icons/fa';
import { Link, useNavigate } from 'react-router-dom';
import { useState } from 'react';
import { loginUser } from '../../api/auth';
import { useAuth } from '../../context/AuthContext';

interface LoginFormProps {
  onForgotPassword: () => void;
}

const LoginForm = ({ onForgotPassword }: LoginFormProps) => {
  const { setUser } = useAuth();
  const navigate = useNavigate();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [errorMessage, setErrorMessage] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const loggedUser = await loginUser({ email, password });
      setUser(loggedUser);
      navigate('/profile/dashboard');
    } catch (error: any) {
      setErrorMessage(error.message || 'Помилка входу');
    }
  };

  return (
    <div className="bg-white p-4 rounded shadow">
      <div className="text-center mb-4">
        <h5>Новий користувач</h5>
        <p className="mt-2">
          Ще не маєш акаунту? <Link to="/register">Зареєструйся тут</Link>
        </p>
      </div>

      <Form onSubmit={handleSubmit}>
        <Form.Group className="mb-3" controlId="formEmail">
          <Form.Label>Введіть Email *</Form.Label>
          <Form.Control
            type="email"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </Form.Group>

        <Form.Group className="mb-3" controlId="formPassword">
          <Form.Label>Пароль *</Form.Label>
          <Form.Control
            type="password"
            placeholder="Пароль"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </Form.Group>

        {errorMessage && (
          <div className="text-danger mb-3">{errorMessage}</div>
        )}

        <Button type="submit" variant="success" className="w-100 rounded-0 mb-3">
          Увійти
        </Button>

        <div className="text-center">
          <Button variant="link" className="text-success p-0" onClick={onForgotPassword}>
            Забули пароль?
          </Button>
        </div>
      </Form>

      <div className="text-center border rounded p-3 mt-4">
        <h6 className="mb-3">АБО УВІЙТИ ЧЕРЕЗ</h6>
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
        Натискаючи кнопку входу, ви погоджуєтесь з
        <span className="text-success text-decoration-underline mx-1">умовами</span> та
        <span className="text-success text-decoration-underline ms-1">політикою конфіденційності</span>.
      </p>
    </div>
  );
};

export default LoginForm;
