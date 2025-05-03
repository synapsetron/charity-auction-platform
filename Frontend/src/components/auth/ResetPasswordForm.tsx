import { Form, Button } from 'react-bootstrap';
import { useState } from 'react';
import { resetPassword } from '../../api/auth';
import { useTranslation } from 'react-i18next';

interface ResetPasswordFormProps {
  onBackToLogin: () => void;
}

const ResetPasswordForm = ({ onBackToLogin }: ResetPasswordFormProps) => {
  const { t } = useTranslation();
  const [email, setEmail] = useState('');
  const [successMessage, setSuccessMessage] = useState('');
  const [errorMessage, setErrorMessage] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSuccessMessage('');
    setErrorMessage('');

    try {
      await resetPassword({ email });
      setSuccessMessage(t('reset.success'));
    } catch (error: any) {
      setErrorMessage(error.message || t('reset.error'));
    }
  };

  return (
    <div className="bg-white p-4 rounded shadow">
      <h5 className="text-center mb-4">{t('reset.title')}</h5>

      <Form onSubmit={handleSubmit}>
        <Form.Group className="mb-3" controlId="formResetEmail">
          <Form.Label>{t('reset.email_label')}</Form.Label>
          <Form.Control
            type="email"
            placeholder={t('reset.email_placeholder')}
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
          {t('reset.submit')}
        </Button>

        <Button variant="link" className="w-100 text-success p-0" onClick={onBackToLogin}>
          {t('reset.back_to_login')}
        </Button>
      </Form>
    </div>
  );
};

export default ResetPasswordForm;
