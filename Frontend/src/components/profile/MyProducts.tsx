import { useState, useEffect } from "react";
import {
  Container,
  Table,
  Spinner,
  Button,
  ButtonGroup,
  Alert,
} from "react-bootstrap";
import { AuctionResponseWithBidsDTO } from "../../types/auctionTypes";
import AuctionForm from "../auction/AuctionForm";
import { useAuction } from "../../hooks/useAuction";
import { useModal } from "../../hooks/useModal";
import { Modal } from "../common/Modal";

const MyProducts = () => {
  const [auctions, setAuctions] = useState<AuctionResponseWithBidsDTO[]>([]);
  const { loading, error, fetchMyAuctions } = useAuction();
  const { isOpen, data, openModal, closeModal } = useModal<string>();

  useEffect(() => {
    loadAuctions();
  }, []);

  const loadAuctions = async () => {
    try {
      const data = await fetchMyAuctions();
      setAuctions(data);
    } catch (error) {
      console.error("Помилка при завантаженні аукціонів", error);
    }
  };

  const handleViewDetails = (auctionId: string) => {
    window.open(`/auction/${auctionId}`, "_blank");
  };

  const handleEditAuction = (auctionId: string) => {
    openModal(auctionId);
  };

  const handleCloseEdit = () => {
    closeModal();
    loadAuctions();
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

      {error && (
        <Alert variant="danger" className="mb-4">
          {error}
        </Alert>
      )}

      <div style={{ overflowX: "auto", maxHeight: "600px" }}>
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
              <th>Статус</th>
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
                    style={{
                      width: "60px",
                      height: "40px",
                      objectFit: "cover",
                    }}
                  />
                </td>
                <td>{auction.isApproved ? "Так" : "Ні"}</td>
                <td>{auction.isSold ? "Так" : "Ні"}</td>
                <td>
                  {auction.isActive ? (
                    <span className="text-success fw-semibold">Активний</span>
                  ) : (
                    <span className="text-muted fw-semibold">Завершений</span>
                  )}
                </td>
                <td>
                  <ButtonGroup size="sm">
                    <Button
                      variant="primary"
                      onClick={() => handleViewDetails(auction.id)}
                    >
                      Переглянути
                    </Button>
                    <Button
                      variant="warning"
                      onClick={() => handleEditAuction(auction.id)}
                      disabled={!auction.isActive}
                    >
                      Редагувати
                    </Button>
                  </ButtonGroup>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
      </div>

      <Modal
        isOpen={isOpen}
        onClose={handleCloseEdit}
        title="Редагувати аукціон"
        size="lg"
      >
        <AuctionForm auctionId={data} onClose={handleCloseEdit} />
      </Modal>
    </Container>
  );
};

export default MyProducts;
