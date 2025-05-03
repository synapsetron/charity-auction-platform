import { useEffect, useState } from "react";
import { Card, Spinner, Alert, Button, Badge, Row, Col } from "react-bootstrap";
import { getBidHistory, donateBid } from "../../api/bid";
import { Link } from "react-router-dom";
import { BidResponseWithWinnerDTO } from "types/bidTypes";
import { useTranslation } from "react-i18next";

const MyBidsPage: React.FC = () => {
  const { t } = useTranslation();
  const [bids, setBids] = useState<BidResponseWithWinnerDTO[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchHistory = async () => {
      try {
        const response = await getBidHistory();
        setBids(response);
      } catch (err) {
        setError(t("my_bids.error_load"));
      } finally {
        setLoading(false);
      }
    };

    fetchHistory();
  }, [t]);

  const handleDonate = async (bidId: string) => {
    try {
      await donateBid(bidId);
      setBids((prev) =>
        prev.map((b) => (b.id === bidId ? { ...b, isDonated: true } : b))
      );
    } catch {
      alert(t("my_bids.error_donate"));
    }
  };

  const handleRemove = (bidId: string) => {
    setBids((prev) => prev.filter((b) => b.id !== bidId));
  };

  if (loading) return <Spinner animation="border" />;
  if (error) return <Alert variant="danger">{error}</Alert>;

  return (
    <div>
      <h4 className="mb-4">{t("my_bids.title")}</h4>
      {bids.length === 0 ? (
        <p>{t("my_bids.no_bids")}</p>
      ) : (
        bids.map((bid) => (
          <Card key={bid.id} className="mb-3 shadow-sm">
            <Card.Body>
              <Row className="align-items-center">
                <Col md={8}>
                  <h5 className="mb-1">{bid.auctionName}</h5>
                  <p className="mb-1 text-muted">
                    {t("my_bids.amount")}: <strong>{bid.amount}</strong> {t("my_bids.UAH")}
                  </p>
                  <p className="mb-1">
                    {t("my_bids.date")}: {new Date(bid.createdAt).toLocaleString()}
                  </p>
                  <p className="mb-1">
                    {t("my_bids.status")}: {" "}
                    {bid.isAuctionActive ? (
                      <Badge bg="warning">{t("my_bids.pending")}</Badge>
                    ) : bid.isWinner ? (
                      <>
                        <Badge bg="success" className="me-1">
                          {t("my_bids.won")}
                        </Badge>
                        {bid.isAuctionSold ? (
                          <Badge bg="info">{t("my_bids.paid")}</Badge>
                        ) : (
                          <Badge bg="danger">{t("my_bids.unpaid")}</Badge>
                        )}
                      </>
                    ) : (
                      <Badge bg="secondary">{t("my_bids.lost")}</Badge>
                    )}
                  </p>
                </Col>

                <Col md={4} className="text-end d-flex flex-column gap-2">
                  <Link
                    to={`/auction/${bid.auctionId}`}
                    className="btn btn-outline-primary btn-sm"
                  >
                    {t("my_bids.go_to_auction")}
                  </Link>

                  {bid.isWinner ? (
                    !bid.isAuctionSold ? (
                      <Button variant="success" size="sm">
                        {t("my_bids.pay")}
                      </Button>
                    ) : null
                  ) : bid.isDonated ? (
                    <Badge bg="info">{t("my_bids.donated")}</Badge>
                  ) : (
                    <>
                      <Button
                        variant="outline-success"
                        size="sm"
                        onClick={() => handleDonate(bid.id)}
                      >
                        {t("my_bids.donate", { amount: bid.amount })}
                      </Button>
                      <Button
                        variant="outline-danger"
                        size="sm"
                        onClick={() => handleRemove(bid.id)}
                      >
                        {t("my_bids.remove")}
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
