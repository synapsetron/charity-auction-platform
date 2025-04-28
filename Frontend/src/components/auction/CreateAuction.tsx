import { useState } from 'react';
import { Container, Form, Button, Toast, ToastContainer } from 'react-bootstrap';
import { createAuction } from '../../api/auction';

const CreateAuction = () => {
  const [formData, setFormData] = useState({
    title: '',
    description: '',
    startingPrice: 0,
    imageUrl: '',
    startTime: '',
    endTime: '',
  });

  const [showToast, setShowToast] = useState(false);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const formattedData = {
        ...formData,
        startTime: new Date(formData.startTime),
        endTime: new Date(formData.endTime),
      };

      await createAuction(formattedData);
      
      // Очистити форму
      setFormData({
        title: '',
        description: '',
        startingPrice: 0,
        imageUrl: '',
        startTime: '',
        endTime: '',
      });

      // Показати успішне повідомлення
      setShowToast(true);

    } catch (error) {
      console.error('Помилка при створенні аукціону', error);
      alert('Не вдалося створити аукціон.'); // Можна замінити також на Toast для помилки
    }
  };

  return (
    <Container className="py-4">
      <h4 className="mb-4">Створити новий аукціон</h4>
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
          Створити аукціон
        </Button>
      </Form>

      {/* Тост повідомлення про успіх */}
      <ToastContainer position="top-end" className="p-3">
        <Toast bg="success" show={showToast} onClose={() => setShowToast(false)} delay={3000} autohide>
          <Toast.Body className="text-white">
            Аукціон успішно створено!
          </Toast.Body>
        </Toast>
      </ToastContainer>
    </Container>
  );
};

export default CreateAuction;
