import { useEffect, useState } from 'react';
import { Container, Table, Button, Spinner } from 'react-bootstrap';
import { getAllAuctions, deleteAuction } from '../../api/adminApi'; // добавь если нужно правильные методы
import { blockAuction, unblockAuction } from '../../api/adminApi'; // если блокировать/разблокировать
import { AuctionResponseDTO } from '../../types/auctionTypes'; // твой тип аукциона

const AllAuctionsManagement = () => {
  const [auctions, setAuctions] = useState<AuctionResponseDTO[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchAuctions();
  }, []);

  const fetchAuctions = async () => {
    try {
      const data = await getAllAuctions(); // здесь API для получения всех аукционов
      setAuctions(data);
    } catch (error) {
      console.error('Помилка завантаження аукціонів', error);
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id: string) => {
    if (window.confirm('Ви впевнені, що хочете видалити аукціон?')) {
      await deleteAuction(id);
      await fetchAuctions();
    }
  };

  const handleToggleBlock = async (id: string, isActive: boolean) => {
    if (isActive) {
      await blockAuction(id);
    } else {
      await unblockAuction(id);
    }
    await fetchAuctions();
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
      <h4 className="mb-4">Усі аукціони</h4>
      <Table bordered hover responsive>
        <thead className="table-light">
          <tr>
            <th>Назва</th>
            <th>Опис</th>
            <th>Початок</th>
            <th>Кінець</th>
            <th>Статус</th>
            <th>Дія</th>
          </tr>
        </thead>
        <tbody>
          {auctions.map((auction) => (
            <tr key={auction.id}>
              <td>{auction.title}</td>
              <td>{auction.description}</td>
              <td>{new Date(auction.startTime).toLocaleString()}</td>
              <td>{new Date(auction.endTime).toLocaleString()}</td>
              <td>
                {auction.isActive ? (
                  <span className="text-success fw-semibold">Активний</span>
                ) : (
                  <span className="text-danger fw-semibold">Заблокований</span>
                )}
              </td>
              <td className="d-flex gap-2">
                <Button size="sm" variant="primary" disabled>
                  Редагувати
                </Button>
                <Button
                  size="sm"
                  variant={auction.isActive ? 'warning' : 'success'}
                  onClick={() => handleToggleBlock(auction.id, auction.isActive)}
                >
                  {auction.isActive ? 'Блокувати' : 'Розблокувати'}
                </Button>
                <Button size="sm" variant="danger" onClick={() => handleDelete(auction.id)}>
                  Видалити
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </Container>
  );
};

export default AllAuctionsManagement;
