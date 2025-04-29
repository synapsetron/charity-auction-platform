import { useState, useEffect } from 'react';
import { Container, Form, Button, Toast, ToastContainer, Spinner, Alert } from 'react-bootstrap';
import { createAuction, getAuctionById, updateAuction } from '../../api/auction';
import { useNavigate } from 'react-router-dom';

type AuctionFormProps = {
  auctionId?: string;
  onClose?: () => void;
};

const AuctionForm = ({ auctionId, onClose }: AuctionFormProps) => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    title: '',
    description: '',
    startingPrice: 0,
    imageUrl: '',
    startTime: '',
    endTime: '',
  });
  const [isActive, setIsActive] = useState(true);
  const [showToast, setShowToast] = useState(false);
  const [loading, setLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  useEffect(() => {
    if (auctionId) {
      fetchAuction();
    }
  }, [auctionId]);

  const fetchAuction = async () => {
    try {
      setLoading(true);
      const auction = await getAuctionById(auctionId!);
      setFormData({
        title: auction.title,
        description: auction.description || '',
        startingPrice: auction.startingPrice,
        imageUrl: auction.imageUrl,
        startTime: new Date(auction.startTime).toISOString().slice(0, 16),
        endTime: new Date(auction.endTime).toISOString().slice(0, 16),
      });
      setIsActive(auction.isActive);
    } catch (error) {
      console.error('Помилка при завантаженні аукціону', error);
      setErrorMessage('Не вдалося завантажити дані аукціону.');
    } finally {
      setLoading(false);
    }
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setErrorMessage(null); // очистить предыдущее сообщение
    try {
      const formattedData = {
        ...formData,
        startTime: new Date(formData.startTime),
        endTime: new Date(formData.endTime),
      };

      if (auctionId) {
        await updateAuction({
          ...formattedData,
          id: auctionId,
          isActive,
        });
        setShowToast(true);
        setTimeout(() => {
          onClose ? onClose() : navigate('/my-products');
        }, 1500);
      } else {
        await createAuction(formattedData);
        setFormData({
          title: '',
          description: '',
          startingPrice: 0,
          imageUrl: '',
          startTime: '',
          endTime: '',
        });
        setIsActive(true);
        setShowToast(true);
      }

    } catch (error) {
      console.error('Помилка при збереженні аукціону', error);
      setErrorMessage('Не вдалося зберегти аукціон. Перевірте дані та спробуйте ще раз.');
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

      {/* Повідомлення про помилку */}
      {errorMessage && (
        <Alert variant="danger" onClose={() => setErrorMessage(null)} dismissible>
          {errorMessage}
        </Alert>
      )}

      <Form onSubmit={handleSubmit}>
        <Form.Group className="mb-3">
          <Form.Label>Назва *</Form.Label>
          <Form.Control
            type="text"
            name="title"
            value={formData.title}
            onChange={handleChange}
            required
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Опис</Form.Label>
          <Form.Control
            as="textarea"
            name="description"
            rows={3}
            value={formData.description}
            onChange={handleChange}
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Стартова ціна *</Form.Label>
          <Form.Control
            type="number"
            step="0.01"
            name="startingPrice"
            value={formData.startingPrice}
            onChange={handleChange}
            required
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Посилання на зображення *</Form.Label>
          <Form.Control
            type="text"
            name="imageUrl"
            value={formData.imageUrl}
            onChange={handleChange}
            required
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Дата початку *</Form.Label>
          <Form.Control
            type="datetime-local"
            name="startTime"
            value={formData.startTime}
            onChange={handleChange}
            required
          />
        </Form.Group>

        <Form.Group className="mb-4">
          <Form.Label>Дата завершення *</Form.Label>
          <Form.Control
            type="datetime-local"
            name="endTime"
            value={formData.endTime}
            onChange={handleChange}
            required
          />
        </Form.Group>

        <Button variant="primary" type="submit">
          {auctionId ? 'Оновити аукціон' : 'Створити аукціон'}
        </Button>
      </Form>

      {/* Тост повідомлення про успіх */}
      <ToastContainer position="top-end" className="p-3">
        <Toast bg="success" show={showToast} onClose={() => setShowToast(false)} delay={2000} autohide>
          <Toast.Body className="text-white">
            {auctionId ? 'Аукціон успішно оновлено!' : 'Аукціон успішно створено!'}
          </Toast.Body>
        </Toast>
      </ToastContainer>
    </Container>
  );
};

export default AuctionForm;
