import { useEffect, useState } from "react";
import {
  Container,
  Table,
  Button,
  Spinner,
  Modal,
  Form,
} from "react-bootstrap";
import {
  getPendingAuctions,
  approveAuction,
  rejectAuction,
  deleteAuction,
} from "../../api/adminApi";
import { useAuth } from "../../context/AuthContext";
import { useTranslation } from "react-i18next";

interface Auction {
  id: string;
  title: string;
  description: string;
  startingPrice: number;
  startTime: string;
  endTime: string;
  isActive: boolean;
  isApproved: boolean;
}

const AuctionModeration = () => {
  const { user } = useAuth();
  const { t } = useTranslation();
  const [auctions, setAuctions] = useState<Auction[]>([]);
  const [loading, setLoading] = useState(true);

  const [showRejectModal, setShowRejectModal] = useState(false);
  const [rejectReason, setRejectReason] = useState("");
  const [selectedAuctionId, setSelectedAuctionId] = useState<string | null>(
    null
  );

  useEffect(() => {
    if (user?.role === "Admin") {
      fetchAuctions();
    }
  }, [user]);

  const fetchAuctions = async () => {
    try {
      const data = await getPendingAuctions();
      setAuctions(data);
    } catch (error) {
      console.error(t("moderation.load_error"), error);
    } finally {
      setLoading(false);
    }
  };

  const handleApprove = async (id: string) => {
    await approveAuction(id);
    await fetchAuctions();
  };

  const handleOpenRejectModal = (id: string) => {
    setSelectedAuctionId(id);
    setRejectReason("");
    setShowRejectModal(true);
  };

  const handleConfirmReject = async () => {
    if (selectedAuctionId && rejectReason.trim()) {
      await rejectAuction(selectedAuctionId, rejectReason);
      setShowRejectModal(false);
      await fetchAuctions();
    }
  };

  const handleDelete = async (id: string, title: string) => {
    const confirmed = window.confirm(
      t("moderation.confirm_delete_text", { title })
    );
    if (confirmed) {
      await deleteAuction(id);
      await fetchAuctions();
    }
  };

  if (loading) {
    return (
      <Container className="py-5 text-center">
        <Spinner animation="border" />
      </Container>
    );
  }

  if (user?.role !== "Admin") {
    return (
      <Container className="py-5 text-center">
        <h3>{t("moderation.access_denied")}</h3>
      </Container>
    );
  }

  return (
    <Container className="py-4">
      <h4 className="mb-4">{t("admin.moderation.moderation_title")}</h4>
      <Table bordered hover responsive>
        <thead className="table-light">
          <tr>
            <th>{t("admin.auctions.name")}</th>
            <th>{t("admin.auctions.description")}</th>
            <th>{t("admin.auctions.start")}</th>
            <th>{t("admin.auctions.end")}</th>
            <th>{t("admin.auctions.action")}</th>
          </tr>
        </thead>
        <tbody>
          {auctions.map((auction) => (
            <tr key={auction.id}>
              <td>{auction.title}</td>
              <td>{auction.description}</td>
              <td>{new Date(auction.startTime).toLocaleString()}</td>
              <td>{new Date(auction.endTime).toLocaleString()}</td>
              <td className="d-flex gap-2">
                <Button
                  size="sm"
                  variant="success"
                  onClick={() => handleApprove(auction.id)}
                >
                  {t("admin.moderation.approve")}
                </Button>
                <Button
                  size="sm"
                  variant="warning"
                  onClick={() => handleOpenRejectModal(auction.id)}
                >
                  {t("admin.moderation.reject")}
                </Button>
                <Button
                  size="sm"
                  variant="danger"
                  onClick={() => handleDelete(auction.id, auction.title)}
                >
                  {t("admin.moderation.delete")}
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>

      <Modal
        show={showRejectModal}
        onHide={() => setShowRejectModal(false)}
        centered
      >
        <Modal.Header closeButton>
          <Modal.Title>{t("moderation.reject_title")}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group className="mb-3" controlId="rejectReason">
              <Form.Label>{t("moderation.reject_reason")}</Form.Label>
              <Form.Control
                as="textarea"
                rows={3}
                value={rejectReason}
                onChange={(e) => setRejectReason(e.target.value)}
                placeholder={t("moderation.reject_placeholder") || ""}
              />
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowRejectModal(false)}>
            {t("moderation.cancel")}
          </Button>
          <Button
            variant="warning"
            onClick={handleConfirmReject}
            disabled={!rejectReason.trim()}
          >
            {t("moderation.reject")}
          </Button>
        </Modal.Footer>
      </Modal>
    </Container>
  );
};

export default AuctionModeration;
