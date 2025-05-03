import { useEffect, useState } from 'react';
import { Container, Form, Button, Spinner, Alert } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { useAuction } from '../../hooks/useAuction';
import { useForm } from '../../hooks/useForm';
import { useNotification } from '../../hooks/useNotification';

type AuctionFormProps = {
  auctionId?: string;
  onClose?: () => void;
};

const initialValues = {
  title: '',
  description: '',
  startingPrice: 0,
  imageUrl: '',
  startTime: '',
  endTime: '',
};

const validationRules = {
  title: (value: string) => !value ? 'Назва обов\'язкова' : null,
  startingPrice: (value: number) => value <= 0 ? 'Ціна повинна бути більше 0' : null,
  imageUrl: (value: string) => !value ? 'Посилання на зображення обов\'язкове' : null,
  startTime: (value: string) => !value ? 'Дата початку обов\'язкова' : null,
  endTime: (value: string) => !value ? 'Дата завершення обов\'язкова' : null,
};

const AuctionForm = ({ auctionId, onClose }: AuctionFormProps) => {
  const navigate = useNavigate();
  const [isActive, setIsActive] = useState(true);
  
  const { loading, error, fetchAuctionById, createNewAuction, updateExistingAuction } = useAuction();
  const { values, errors, touched, handleChange, handleBlur, validateForm, resetForm } = useForm(initialValues, validationRules);
  const { showNotification } = useNotification();

  useEffect(() => {
    if (auctionId) {
      loadAuction();
    }
  }, [auctionId]);

  const loadAuction = async () => {
    try {
      const auction = await fetchAuctionById(auctionId!);
      handleChange('title', auction.title);
      handleChange('description', auction.description || '');
      handleChange('startingPrice', auction.startingPrice);
      handleChange('imageUrl', auction.imageUrl);
      handleChange('startTime', new Date(auction.startTime).toISOString().slice(0, 16));
      handleChange('endTime', new Date(auction.endTime).toISOString().slice(0, 16));
      setIsActive(auction.isActive);
    } catch (error) {
      console.error('Помилка при завантаженні аукціону', error);
      showNotification('error', 'Не вдалося завантажити дані аукціону');
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!validateForm()) {
      return;
    }

    try {
      if (auctionId) {
        await updateExistingAuction(auctionId, { ...values, isActive });
        showNotification('success', 'Аукціон успішно оновлено!');
        setTimeout(() => {
          onClose ? onClose() : navigate('/my-products');
        }, 1500);
      } else {
        await createNewAuction(values);
        resetForm();
        setIsActive(true);
        showNotification('success', 'Аукціон успішно створено!');
      }
    } catch (error) {
      console.error('Помилка при збереженні аукціону', error);
      showNotification('error', 'Не вдалося зберегти аукціон. Перевірте дані та спробуйте ще раз.');
    }
  };

  if (loading) {
    return (
      <Container className="py-5 text-center">
        <Spinner animation="border" />
      </Container>
    );
  }

  return (
    <Container className="py-4">
      <h4 className="mb-4">{auctionId ? 'Редагувати аукціон' : 'Створити новий аукціон'}</h4>

      {error && (
        <Alert variant="danger" onClose={() => {}} dismissible>
          {error}
        </Alert>
      )}

      <Form onSubmit={handleSubmit}>
        <Form.Group className="mb-3">
          <Form.Label>Назва *</Form.Label>
          <Form.Control
            type="text"
            name="title"
            value={values.title}
            onChange={(e) => handleChange('title', e.target.value)}
            onBlur={() => handleBlur('title')}
            isInvalid={touched.title && !!errors.title}
          />
          <Form.Control.Feedback type="invalid">
            {errors.title}
          </Form.Control.Feedback>
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Опис</Form.Label>
          <Form.Control
            as="textarea"
            name="description"
            rows={3}
            value={values.description}
            onChange={(e) => handleChange('description', e.target.value)}
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Стартова ціна *</Form.Label>
          <Form.Control
            type="number"
            step="0.01"
            name="startingPrice"
            value={values.startingPrice}
            onChange={(e) => handleChange('startingPrice', parseFloat(e.target.value))}
            onBlur={() => handleBlur('startingPrice')}
            isInvalid={touched.startingPrice && !!errors.startingPrice}
          />
          <Form.Control.Feedback type="invalid">
            {errors.startingPrice}
          </Form.Control.Feedback>
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Посилання на зображення *</Form.Label>
          <Form.Control
            type="text"
            name="imageUrl"
            value={values.imageUrl}
            onChange={(e) => handleChange('imageUrl', e.target.value)}
            onBlur={() => handleBlur('imageUrl')}
            isInvalid={touched.imageUrl && !!errors.imageUrl}
          />
          <Form.Control.Feedback type="invalid">
            {errors.imageUrl}
          </Form.Control.Feedback>
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Дата початку *</Form.Label>
          <Form.Control
            type="datetime-local"
            name="startTime"
            value={values.startTime}
            onChange={(e) => handleChange('startTime', e.target.value)}
            onBlur={() => handleBlur('startTime')}
            isInvalid={touched.startTime && !!errors.startTime}
          />
          <Form.Control.Feedback type="invalid">
            {errors.startTime}
          </Form.Control.Feedback>
        </Form.Group>

        <Form.Group className="mb-4">
          <Form.Label>Дата завершення *</Form.Label>
          <Form.Control
            type="datetime-local"
            name="endTime"
            value={values.endTime}
            onChange={(e) => handleChange('endTime', e.target.value)}
            onBlur={() => handleBlur('endTime')}
            isInvalid={touched.endTime && !!errors.endTime}
          />
          <Form.Control.Feedback type="invalid">
            {errors.endTime}
          </Form.Control.Feedback>
        </Form.Group>

        <Button variant="primary" type="submit">
          {auctionId ? 'Оновити аукціон' : 'Створити аукціон'}
        </Button>
      </Form>
    </Container>
  );
};

export default AuctionForm;
