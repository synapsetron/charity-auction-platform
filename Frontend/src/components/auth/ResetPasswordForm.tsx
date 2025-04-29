import { Form, Button } from 'react-bootstrap';
import { useState } from 'react';
import { resetPassword } from '../../api/auth';

interface ResetPasswordFormProps {
  onBackToLogin: () => void;
}

const ResetPasswordForm = ({ onBackToLogin }: ResetPasswordFormProps) => {
  const [email, setEmail] = useState('');
  const [successMessage, setSuccessMessage] = useState('');
  const [errorMessage, setErrorMessage] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSuccessMessage('');
    setErrorMessage('');

    try {
      await resetPassword({ email });
      setSuccessMessage('Лист з новим паролем відправлено на вашу пошту!');
    } catch (error: any) {
      setErrorMessage(error.message || 'Сталася помилка');
    }
  };

  return (
    <div className="bg-white p-4 rounded shadow">
      <h5 className="text-center mb-4">Відновлення пароля</h5>

      <Form onSubmit={handleSubmit}>
        <Form.Group className="mb-3" controlId="formResetEmail">
          <Form.Label>Введіть Email *</Form.Label>
          <Form.Control
            type="email"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </Form.Group>

        {successMessage && (
          <div className="text-success text-center mb-3">{successMessage}</div>
        )}
        {errorMessage && (
          <div className="text-danger text-center mb-3">{errorMessage}</div>
        )}

        <Button type="submit" variant="success" className="w-100 rounded-0 mb-3">
          Відновити
        </Button>

        <Button variant="link" className="w-100 text-success p-0" onClick={onBackToLogin}>
          Повернутися до логіну
        </Button>
      </Form>
    </div>
  );
};

export default ResetPasswordForm;
