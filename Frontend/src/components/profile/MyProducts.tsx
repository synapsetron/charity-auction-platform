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
import { useTranslation } from "react-i18next";

const MyProducts = () => {
  const [auctions, setAuctions] = useState<AuctionResponseWithBidsDTO[]>([]);
  const { loading, error, fetchMyAuctions } = useAuction();
  const { isOpen, data, openModal, closeModal } = useModal<string>();
  const { t } = useTranslation();

  useEffect(() => {
    loadAuctions();
  }, []);

  const loadAuctions = async () => {
    try {
      const data = await fetchMyAuctions();
      setAuctions(data);
    } catch (error) {
      console.error(t("my_products.error"), error);
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
      <h4>{t("my_products.title")}</h4>

      {error && (
        <Alert variant="danger" className="mb-4">
          {t("my_products.error")}
        </Alert>
      )}

      <div style={{ overflowX: "auto", maxHeight: "600px" }}>
        <Table bordered hover responsive className="align-middle">
          <thead className="table-light">
            <tr>
              <th>{t("my_products.table.sn")}</th>
              <th>{t("my_products.table.title")}</th>
              <th>{t("my_products.table.fee")}</th>
              <th>{t("my_products.table.start_price")}</th>
              <th>{t("my_products.table.bids")}</th>
              <th>{t("my_products.table.image")}</th>
              <th>{t("my_products.table.approved")}</th>
              <th>{t("my_products.table.sold")}</th>
              <th>{t("my_products.table.status")}</th>
              <th>{t("my_products.table.actions")}</th>
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
                <td>
                  {auction.isApproved
                    ? t("my_products.table.yes")
                    : t("my_products.table.no")}
                </td>
                <td>
                  {auction.isSold
                    ? t("my_products.table.yes")
                    : t("my_products.table.no")}
                </td>
                <td>
                  {!auction.isApproved ? (
                    <span className="text-warning fw-semibold">
                      {t("my_products.table.pending")}
                    </span>
                  ) : auction.isActive ? (
                    <span className="text-success fw-semibold">
                      {t("my_products.table.active")}
                    </span>
                  ) : (
                    <span className="text-muted fw-semibold">
                      {t("my_products.table.ended")}
                    </span>
                  )}
                </td>
                <td>
                  <ButtonGroup size="sm">
                    <Button
                      variant="primary"
                      onClick={() => handleViewDetails(auction.id)}
                    >
                      {t("my_products.table.view")}
                    </Button>
                    <Button
                      variant="warning"
                      onClick={() => handleEditAuction(auction.id)}
                      disabled={!auction.isActive}
                    >
                      {t("my_products.table.edit")}
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
        title={t("my_products.modal.title")}
        size="lg"
      >
        <AuctionForm auctionId={data} onClose={handleCloseEdit} />
      </Modal>
    </Container>
  );
};

export default MyProducts;
