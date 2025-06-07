import { Form, Button } from 'react-bootstrap';
import { FaFacebook } from 'react-icons/fa';
import { Link, useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { loginUser, googleLoginUser } from '../../api/auth';
import { useAuth } from '../../context/AuthContext';
import { GoogleLogin } from '@react-oauth/google';
import { useTranslation } from 'react-i18next';
import i18n from '../../i18n';
interface LoginFormProps {
  onForgotPassword: () => void;
}

const LoginForm = ({ onForgotPassword }: LoginFormProps) => {
  const { user, setUser } = useAuth();
  const navigate = useNavigate();
  const { t } = useTranslation();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [errorMessage, setErrorMessage] = useState('');

  useEffect(() => {
    if (user) {
      navigate('/profile/dashboard');
    }
  }, [user]);

  const handleSubmit = async (e: React.FormEvent) => {
  e.preventDefault();
  try {
    const loggedUser = await loginUser({ email, password });
    setUser(loggedUser);

    setTimeout(() => {
      navigate('/profile/dashboard', { replace: true });
    }, 0);
  } catch (error: any) {
    const backendMessage = error.response?.data?.message;
    setErrorMessage(backendMessage || t('login.errors.login'));
  }
};

  const handleGoogleLoginSuccess = async (credentialResponse: any) => {
    try {
      if (credentialResponse.credential) {
        const loggedUser = await googleLoginUser(credentialResponse.credential);
        setUser(loggedUser);
      }
    } catch (error: any) {
      const backendMessage = error.response?.data?.message;
      setErrorMessage(backendMessage || t('login.errors.google'));
    }
  };

  const handleGoogleLoginError = () => {
    setErrorMessage(t('login.errors.google_fail'));
  };

  return (
    <div className="bg-white p-4 rounded shadow">
      <div className="text-center mb-4">
        <h5>{t('login.new_user')}</h5>
        <p className="mt-2">
          {t('login.no_account')} <Link to="/register">{t('login.register_here')}</Link>
        </p>
      </div>

      <Form onSubmit={handleSubmit}>
        <Form.Group className="mb-3" controlId="formEmail">
          <Form.Label>{t('login.email_label')}</Form.Label>
          <Form.Control
            type="email"
            placeholder={t('login.email_placeholder')}
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </Form.Group>

        <Form.Group className="mb-3" controlId="formPassword">
          <Form.Label>{t('login.password_label')}</Form.Label>
          <Form.Control
            type="password"
            placeholder={t('login.password_placeholder')}
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </Form.Group>

        {errorMessage && (
          <div className="text-danger mb-3">{errorMessage}</div>
        )}

        <Button type="submit" variant="success" className="w-100 rounded-0 mb-3">
          {t('login.login_button')}
        </Button>

        <div className="text-center">
          <Button variant="link" className="text-success p-0" onClick={onForgotPassword}>
            {t('login.forgot_password')}
          </Button>
        </div>
      </Form>

      <div className="text-center border rounded p-3 mt-4">
        <h6 className="mb-3">{t('login.or_login_with')}</h6>
        <div className="d-flex justify-content-center gap-3">
          <GoogleLogin
            onSuccess={handleGoogleLoginSuccess}
            onError={handleGoogleLoginError}
          />
        </div>
      </div>

      <p className="text-center mt-4">
        {t('login.agree_text')} <span className="text-success text-decoration-underline mx-1">{t('login.terms')}</span> {t('login.and')} <span className="text-success text-decoration-underline ms-1">{t('login.privacy')}</span>.
      </p>
    </div>
  );
};

export default LoginForm;
