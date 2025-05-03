import { Card, Badge, Button, OverlayTrigger, Tooltip } from "react-bootstrap";
import { Link } from "react-router-dom";
import { AuctionResponseWithBidsDTO } from "../../types/auctionTypes";
import { FaGavel, FaMoneyBillWave, FaHeart, FaCheckCircle } from "react-icons/fa";
import { motion } from "framer-motion";
import { useTranslation } from "react-i18next";

interface AuctionCardProps {
  auction: AuctionResponseWithBidsDTO;
}

export const AuctionCard: React.FC<AuctionCardProps> = ({ auction }) => {
  const { t } = useTranslation();
  const bidsCount = auction.bids.length;
  const currentBid =
    bidsCount > 0
      ? Math.max(...auction.bids.map((bid) => bid.amount))
      : auction.startingPrice;

  const buyNowPrice = currentBid + 500;

  return (
    <motion.div
      whileHover={{ scale: 1.015 }}
      transition={{ type: "spring", stiffness: 200, damping: 15 }}
    >
      <Card
        className="h-100 border-0 rounded-4 overflow-hidden shadow-sm"
        style={{
          backgroundColor: "#f0f8ff",
          boxShadow: "0 8px 20px rgba(0, 0, 0, 0.05)",
          borderRadius: "1rem",
          border: "1px solid #e6e6e6",
        }}
      >
        <div className="position-relative">
          <Link to={`/auction/${auction.id}`}>
            <Card.Img
              variant="top"
              src={auction.imageUrl}
              alt={auction.title}
              style={{
                height: "240px",
                objectFit: "contain",
                borderBottom: "1px solid #eaeaea",
              }}
            />
          </Link>

          <Badge
            bg="success"
            className="position-absolute top-0 start-0 m-2 px-3 py-1 rounded-pill shadow-sm"
          >
            <FaCheckCircle className="me-1 mb-1" /> {t("auction_card.active")}
          </Badge>
          <Badge
            bg="primary"
            className="position-absolute top-0 end-0 m-2 px-3 py-1 rounded-pill shadow-sm"
          >
            {bidsCount} {bidsCount === 1 ? t("auction_card.bid") : t("auction_card.bids")}
          </Badge>
        </div>

        <Card.Body className="d-flex flex-column px-4 py-3">
          <Card.Title className="text-center fw-semibold fs-6 mb-3 text-dark">
            {auction.title}
          </Card.Title>

          <div className="d-flex justify-content-around text-center mb-3">
            <div>
              <FaGavel className="text-success mb-1" />
              <div className="fw-bold text-success">${currentBid.toFixed(2)}</div>
              <small className="text-muted">{t("auction_card.current_bid")}</small>
            </div>
            <div className="border-start" style={{ height: "50px" }}></div>
            <div>
              <FaMoneyBillWave className="text-danger mb-1" />
              <div className="fw-bold text-danger">${buyNowPrice.toFixed(2)}</div>
              <small className="text-muted">{t("auction_card.buy_now")}</small>
            </div>
          </div>

          <div className="d-flex justify-content-between align-items-center mt-auto">
            <Button
              as={Link as any}
              to={`/auction/${auction.id}`}
              variant="success"
              className="w-100 me-2 rounded-pill fw-semibold"
            >
              {t("auction_card.place_bid")}
            </Button>
            <OverlayTrigger
              placement="top"
              overlay={<Tooltip>{t("auction_card.add_to_favorites")}</Tooltip>}
            >
              <Button variant="light" className="rounded-circle p-2 shadow-sm">
                <FaHeart className="text-danger" />
              </Button>
            </OverlayTrigger>
          </div>
        </Card.Body>
      </Card>
    </motion.div>
  );
};
