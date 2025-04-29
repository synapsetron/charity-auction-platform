import { Card, Badge, Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { AuctionResponseWithBidsDTO } from "../../types/auctionTypes";
import { FaGavel, FaMoneyBillWave, FaHeart } from "react-icons/fa"; // Иконки через react-icons

interface AuctionCardProps {
  auction: AuctionResponseWithBidsDTO;
}

export const AuctionCard: React.FC<AuctionCardProps> = ({ auction }) => {
  const bidsCount = auction.bids.length;
  const currentBid =
    bidsCount > 0
      ? Math.max(...auction.bids.map((bid) => bid.amount))
      : auction.startingPrice;

  return (
    <Card className="h-100 shadow-sm d-flex flex-column">
      <div className="position-relative">
        <Link to={`/auction/${auction.id}`}>
          <Card.Img
            variant="top"
            src={auction.imageUrl}
            alt={auction.title}
            style={{
              height: "250px",
              objectFit: "contain",
              backgroundColor: "#f8f9fa",
              padding: "10px", // Чтоб фото не прилипало к краям
            }}
          />
        </Link>
        <Badge bg="success" className="position-absolute top-0 start-0 m-2">
          On Stock
        </Badge>
        <Badge bg="info" className="position-absolute top-0 end-0 m-2">
          {bidsCount} {bidsCount === 1 ? "Bid" : "Bids"}
        </Badge>
      </div>

      <Card.Body className="d-flex flex-column">
        <div className="flex-grow-1">
          <Card.Title className="text-center fs-6">{auction.title}</Card.Title>

          <div className="d-flex justify-content-around align-items-center my-2">
            <div className="text-center">
              <FaGavel className="mb-1" />
              <div className="text-success fw-bold">
                ${currentBid.toFixed(2)}
              </div>
              <small className="text-muted">Current Bid</small>
            </div>

            <div className="border-start mx-2" style={{ height: "50px" }}></div>

            <div className="text-center">
              <FaMoneyBillWave className="mb-1" />
              <div className="text-danger fw-bold">
                ${(currentBid + 500).toFixed(2)}
              </div>
              <small className="text-muted">Buy Now</small>
            </div>
          </div>
        </div>

        <div className="d-flex justify-content-between align-items-center mt-3">
          <Button
            as={Link as any}
            to={`/auction/${auction.id}`}
            variant="success"
            className="flex-grow-1 me-2"
          >
            Place Bid
          </Button>
          <Button variant="light">
            <FaHeart className="text-danger" />
          </Button>
        </div>
      </Card.Body>
    </Card>
  );
};
