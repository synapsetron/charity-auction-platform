import { useEffect, useState } from "react";
import {
  Container,
  Table,
  Button,
  Spinner,
  Modal,
} from "react-bootstrap";
import { getAllAuctions } from "../../api/auction";
import { blockAuction, deleteAuction, unblockAuction } from "../../api/adminApi";
import { AuctionResponseDTO } from "../../types/auctionTypes";
import { useTranslation } from "react-i18next";
import { useModal } from "../../hooks/useModal"; // убедись, что путь корректный

const AllAuctionsManagement = () => {
  const [auctions, setAuctions] = useState<AuctionResponseDTO[]>([]);
  const [loading, setLoading] = useState(true);
  const { t } = useTranslation();

  const {
    isOpen: isDeleteModalOpen,
    data: selectedAuctionId,
    openModal: openDeleteModal,
    closeModal: closeDeleteModal,
  } = useModal<string | null>(null);

  useEffect(() => {
    fetchAuctions();
  }, []);

  const fetchAuctions = async () => {
    try {
      const data = await getAllAuctions();
      setAuctions(data);
    } catch (error) {
      console.error(t("admin.auctions.load_error"), error);
    } finally {
      setLoading(false);
    }
  };

  const confirmDelete = async () => {
    if (selectedAuctionId) {
      await deleteAuction(selectedAuctionId);
      closeDeleteModal();
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
      <h4 className="mb-4">{t("admin.auctions.title")}</h4>
      <Table bordered hover responsive>
        <thead className="table-light">
          <tr>
            <th>{t("admin.auctions.name")}</th>
            <th>{t("admin.auctions.description")}</th>
            <th>{t("admin.auctions.start")}</th>
            <th>{t("admin.auctions.end")}</th>
            <th>{t("admin.auctions.status")}</th>
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
              <td>
                {auction.isActive ? (
                  <span className="text-success fw-semibold">
                    {t("admin.auctions.active")}
                  </span>
                ) : (
                  <span className="text-danger fw-semibold">
                    {t("admin.auctions.blocked")}
                  </span>
                )}
              </td>
              <td className="d-flex gap-2">
                <Button size="sm" variant="primary" disabled>
                  {t("admin.auctions.edit")}
                </Button>
                <Button
                  size="sm"
                  variant={auction.isActive ? "warning" : "success"}
                  onClick={() => handleToggleBlock(auction.id, auction.isActive)}
                >
                  {auction.isActive
                    ? t("admin.auctions.block")
                    : t("admin.auctions.unblock")}
                </Button>
                <Button
                  size="sm"
                  variant="danger"
                  onClick={() => openDeleteModal(auction.id)}
                >
                  {t("admin.auctions.delete")}
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>

      <Modal show={isDeleteModalOpen} onHide={closeDeleteModal} centered>
        <Modal.Header closeButton>
          <Modal.Title>{t("admin.auctions.confirm_title")}</Modal.Title>
        </Modal.Header>
        <Modal.Body>{t("admin.auctions.confirm_delete")}</Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={closeDeleteModal}>
            {t("admin.auctions.cancel")}
          </Button>
          <Button variant="danger" onClick={confirmDelete}>
            {t("admin.auctions.confirm")}
          </Button>
        </Modal.Footer>
      </Modal>
    </Container>
  );
};

export default AllAuctionsManagement;
