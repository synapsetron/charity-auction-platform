import { useEffect, useState } from "react";
import { Card, Spinner, Alert, Button, Badge, Row, Col } from "react-bootstrap";
import { getBidHistory, donateBid } from "../../api/bid";
import { Link } from "react-router-dom";
import { BidResponseWithWinnerDTO } from "types/bidTypes";

const MyBidsPage: React.FC = () => {
  const [bids, setBids] = useState<BidResponseWithWinnerDTO[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchHistory = async () => {
      try {
        const response = await getBidHistory();
        setBids(response);
      } catch (err) {
        setError("Не вдалося завантажити ставки.");
      } finally {
        setLoading(false);
      }
    };

    fetchHistory();
  }, []);

  const handleDonate = async (bidId: string) => {
    try {
      await donateBid(bidId);
      setBids((prev) =>
        prev.map((b) => (b.id === bidId ? { ...b, isDonated: true } : b))
      );
    } catch {
      alert("Помилка при спробі задонатити.");
    }
  };

  const handleRemove = (bidId: string) => {
    setBids((prev) => prev.filter((b) => b.id !== bidId));
  };

  if (loading) return <Spinner animation="border" />;
  if (error) return <Alert variant="danger">{error}</Alert>;

  return (
    <div>
      <h4 className="mb-4">Мої ставки</h4>
      {bids.length === 0 ? (
        <p>Ви ще не брали участь у жодному аукціоні.</p>
      ) : (
        bids.map((bid) => (
          <Card key={bid.id} className="mb-3 shadow-sm">
            <Card.Body>
              <Row className="align-items-center">
                <Col md={8}>
                  <h5 className="mb-1">{bid.auctionName}</h5>
                  <p className="mb-1 text-muted">
                    Сума: <strong>{bid.amount}</strong> грн
                  </p>
                  <p className="mb-1">
                    Дата ставки: {new Date(bid.createdAt).toLocaleString()}
                  </p>
                  <p>
                    Статус:{" "}
                    {bid.isWinner ? (
                      <Badge bg="success">Виграв</Badge>
                    ) : (
                      <Badge bg="secondary">Програв</Badge>
                    )}
                  </p>
                </Col>

                <Col md={4} className="text-end d-flex flex-column gap-2">
                  <Link
                    to={`/auction/${bid.auctionId}`}
                    className="btn btn-outline-primary btn-sm"
                  >
                    Перейти до аукціону
                  </Link>

                  {bid.isWinner ? (
                    <Button variant="success" size="sm">
                      Оплатити
                    </Button>
                  ) : bid.isDonated ? (
                    <Badge bg="info">Донат підтверджено</Badge>
                  ) : (
                    <>
                      <Button
                        variant="outline-success"
                        size="sm"
                        onClick={() => handleDonate(bid.id)}
                      >
                        Пожертвувати {bid.amount} грн
                      </Button>
                      <Button
                        variant="outline-danger"
                        size="sm"
                        onClick={() => handleRemove(bid.id)}
                      >
                        Видалити зі списку
                      </Button>
                    </>
                  )}
                </Col>
              </Row>
            </Card.Body>
          </Card>
        ))
      )}
    </div>
  );
};

export default MyBidsPage;
