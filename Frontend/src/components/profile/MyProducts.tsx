import { useEffect, useState } from 'react';
import { Container, Table, Spinner, Button, ButtonGroup, Alert, Modal } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { getMyAuctions, deleteAuction, closeAuction } from '../../api/auction';
import { AuctionResponseWithBidsDTO } from '../../types/auctionTypes';

const MyProducts = () => {
  const [auctions, setAuctions] = useState<AuctionResponseWithBidsDTO[]>([]);
  const [loading, setLoading] = useState(true);
  const [successMessage, setSuccessMessage] = useState<string | null>(null);
  const [showModal, setShowModal] = useState(false);
  const [modalAction, setModalAction] = useState<'delete' | 'close' | null>(null);
  const [selectedAuctionId, setSelectedAuctionId] = useState<string | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    fetchMyAuctions();
  }, []);

  const fetchMyAuctions = async () => {
    try {
      const data = await getMyAuctions();
      setAuctions(data);
    } catch (error) {
      console.error('Помилка при завантаженні аукціонів', error);
    } finally {
      setLoading(false);
    }
  };

  const handleViewDetails = (auctionId: string) => {
    navigate(`/auction/${auctionId}`);
  };

  const handleEditAuction = (auctionId: string) => {
    navigate(`/auction/edit/${auctionId}`);
  };

  const openModal = (auctionId: string, action: 'delete' | 'close') => {
    setSelectedAuctionId(auctionId);
    setModalAction(action);
    setShowModal(true);
  };

  const confirmModalAction = async () => {
    if (!selectedAuctionId || !modalAction) return;
    try {
      if (modalAction === 'delete') {
        await deleteAuction(selectedAuctionId);
        setSuccessMessage('Аукціон успішно видалено.');
      } else if (modalAction === 'close') {
        await closeAuction(selectedAuctionId);
        setSuccessMessage('Аукціон успішно закрито.');
      }
      fetchMyAuctions();
    } catch (error) {
      console.error('Помилка при обробці дії', error);
    } finally {
      setShowModal(false);
      setSelectedAuctionId(null);
      setModalAction(null);
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
      <h4>Мої товари</h4>

      {/* Повідомлення про успіх */}
      {successMessage && (
        <Alert variant="success" onClose={() => setSuccessMessage(null)} dismissible>
          {successMessage}
        </Alert>
      )}

      {/* Блок з прокруткою таблиці */}
      <div style={{ overflowX: 'auto', maxHeight: '600px' }}>
        <Table bordered hover responsive className="align-middle">
          <thead className="table-light">
            <tr>
              <th>S.N</th>
              <th>Назва</th>
              <th>Комісія</th>
              <th>Стартова ціна</th>
              <th>Кількість ставок</th>
              <th>Зображення</th>
              <th>Перевірено</th>
              <th>Продано</th>
              <th>Дії</th>
            </tr>
          </thead>
          <tbody>
            {auctions.map((auction, index) => (
              <tr key={auction.id}>
                <td>{index + 1}</td>
                <td>{auction.title}</td>
                <td>{(auction.startingPrice * 0.05).toFixed(2)} $</td>
                <td>{auction.startingPrice.toFixed(2)} $</td>
                <td>{auction.bids.length}</td>
                <td>
                  <img
                    src={auction.imageUrl}
                    alt={auction.title}
                    style={{ width: '60px', height: '40px', objectFit: 'cover' }}
                  />
                </td>
                <td>{auction.isApproved ? 'Так' : 'Ні'}</td>
                <td>{auction.isSold ? 'Так' : 'Ні'}</td>
                <td>
                  <ButtonGroup size="sm">
                    <Button variant="primary" onClick={() => handleViewDetails(auction.id)}>Переглянути</Button>
                    <Button variant="warning" onClick={() => handleEditAuction(auction.id)}>Редагувати</Button>
                    <Button variant="secondary" onClick={() => openModal(auction.id, 'close')}>Закрити</Button>
                    <Button variant="danger" onClick={() => openModal(auction.id, 'delete')}>Видалити</Button>
                  </ButtonGroup>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
      </div>

      {/* Модальне вікно підтвердження */}
      <Modal show={showModal} onHide={() => setShowModal(false)} centered>
        <Modal.Header closeButton>
          <Modal.Title>Підтвердження дії</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          Ви впевнені, що хочете {modalAction === 'delete' ? 'видалити' : 'закрити'} цей аукціон?
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowModal(false)}>
            Скасувати
          </Button>
          <Button variant="danger" onClick={confirmModalAction}>
            Так, підтвердити
          </Button>
        </Modal.Footer>
      </Modal>
    </Container>
  );
};

export default MyProducts;
