import { useEffect, useState } from "react";
import {
  Container,
  Table,
  Spinner,
  Button,
  ButtonGroup,
  Card,
} from "react-bootstrap";
import { getMyAuctions } from "../../api/auction";
import { AuctionResponseWithBidsDTO } from "../../types/auctionTypes";
import AuctionForm from "../auction/AuctionForm";

const MyProducts = () => {
  const [auctions, setAuctions] = useState<AuctionResponseWithBidsDTO[]>([]);
  const [loading, setLoading] = useState(true);
  const [editAuctionId, setEditAuctionId] = useState<string | null>(null);

  useEffect(() => {
    fetchMyAuctions();
  }, []);

  const fetchMyAuctions = async () => {
    try {
      const data = await getMyAuctions();
      setAuctions(data);
    } catch (error) {
      console.error("Помилка при завантаженні аукціонів", error);
    } finally {
      setLoading(false);
    }
  };

  const handleViewDetails = (auctionId: string) => {
    window.open(`/auction/${auctionId}`, "_blank");
  };

  const handleEditAuction = (auctionId: string) => {
    setEditAuctionId(auctionId);
  };

  const handleCloseEdit = () => {
    setEditAuctionId(null);
    fetchMyAuctions();
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

      {/* Таблиця аукціонів */}
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

      {/* Вбудована форма редагування */}
      {editAuctionId && (
        <Card className="mt-4 shadow-sm">
          <Card.Header className="d-flex justify-content-between align-items-center">
            <strong>Редагувати аукціон</strong>
            <Button
              variant="outline-danger"
              size="sm"
              onClick={handleCloseEdit}
            > Закрити
            </Button>
          </Card.Header>
          <Card.Body>
            <AuctionForm auctionId={editAuctionId} onClose={handleCloseEdit} />
          </Card.Body>
        </Card>
      )}
    </Container>
  );
};

export default MyProducts;
