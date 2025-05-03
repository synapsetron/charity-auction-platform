import { useLocation, Link } from "react-router-dom";
import { Container, Card, Button } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import { FaCheckCircle } from "react-icons/fa";

const PaymentSuccessPage = () => {
  const location = useLocation();
  const { t } = useTranslation();

  const params = new URLSearchParams(location.search);
  const status = params.get("status") || t("payment.status_paid");

  return (
    <Container
      className="d-flex justify-content-center align-items-center"
      style={{ minHeight: "80vh" }}
    >
      <Card className="text-center p-5 shadow-lg border-0">
        <FaCheckCircle size={64} className="text-success mb-4" />
        <h2 className="text-success mb-3">{t("payment.success_title")}</h2>
        <p className="text-muted fs-5">
          {t("payment.success_message")} <strong>{status}</strong>
        </p>

        <Button
          as={Link as any}
          to="/auctions"
          variant="outline-success"
          className="mt-4 px-4 py-2"
        >
          {t("payment.back_to_auctions")}
        </Button>
      </Card>
    </Container>
  );
};

export default PaymentSuccessPage;
