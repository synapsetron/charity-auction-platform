import { useEffect, useState } from 'react';
import { Container, Form, Button, Spinner, Alert } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { useAuction } from '../../hooks/useAuction';
import { useForm } from '../../hooks/useForm';
import { useNotification } from '../../hooks/useNotification';
import { useTranslation } from 'react-i18next';

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

const AuctionForm = ({ auctionId, onClose }: AuctionFormProps) => {
  const navigate = useNavigate();
  const { t } = useTranslation();
  const [isActive, setIsActive] = useState(true);

  const { loading, error, fetchAuctionById, createNewAuction, updateExistingAuction } = useAuction();
  const { values, errors, touched, handleChange, handleBlur, validateForm, resetForm } = useForm(
    initialValues,
    {
      title: (value: string) => !value ? t('auction_form.validation.title') : null,
      startingPrice: (value: number) => value <= 0 ? t('auction_form.validation.starting_price') : null,
      imageUrl: (value: string) => !value ? t('auction_form.validation.image_url') : null,
      startTime: (value: string) => !value ? t('auction_form.validation.start_time') : null,
      endTime: (value: string) => !value ? t('auction_form.validation.end_time') : null,
    }
  );
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
      console.error(t('auction_form.errors.load'), error);
      showNotification('error', t('auction_form.errors.load'));
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
        showNotification('success', t('auction_form.notifications.updated'));
        setTimeout(() => {
          onClose ? onClose() : navigate('/my-products');
        }, 1500);
      } else {
        await createNewAuction(values);
        resetForm();
        setIsActive(true);
        showNotification('success', t('auction_form.notifications.created'));
      }
    } catch (error) {
      console.error(t('auction_form.errors.save'), error);
      showNotification('error', t('auction_form.errors.save'));
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
      <h4 className="mb-4">{auctionId ? t('auction_form.edit_title') : t('auction_form.create_title')}</h4>

      {error && (
        <Alert variant="danger" dismissible>
          {error}
        </Alert>
      )}

      <Form onSubmit={handleSubmit}>
        <Form.Group className="mb-3">
          <Form.Label>{t('auction_form.fields.title')}</Form.Label>
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
          <Form.Label>{t('auction_form.fields.description')}</Form.Label>
          <Form.Control
            as="textarea"
            name="description"
            rows={3}
            value={values.description}
            onChange={(e) => handleChange('description', e.target.value)}
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>{t('auction_form.fields.starting_price')}</Form.Label>
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
          <Form.Label>{t('auction_form.fields.image_url')}</Form.Label>
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
          <Form.Label>{t('auction_form.fields.start_time')}</Form.Label>
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
          <Form.Label>{t('auction_form.fields.end_time')}</Form.Label>
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
          {auctionId ? t('auction_form.update_button') : t('auction_form.create_button')}
        </Button>
      </Form>
    </Container>
  );
};

export default AuctionForm;
