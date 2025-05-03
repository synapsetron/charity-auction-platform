import { Container, Card, Button } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import { FaTimesCircle } from "react-icons/fa";
import { Link } from "react-router-dom";

const PaymentFailPage = () => {
  const { t } = useTranslation();

  return (
    <Container className="d-flex justify-content-center align-items-center" style={{ minHeight: "80vh" }}>
      <Card className="text-center p-5 shadow-lg border-0">
        <FaTimesCircle size={64} className="text-danger mb-4" />
        <h2 className="text-danger mb-3">{t("payment.fail_title")}</h2>
        <p className="text-muted fs-5">{t("payment.fail_message")}</p>

        <Button as={Link as any} to="/contact" variant="outline-danger" className="mt-4 px-4 py-2">
          {t("payment.contact_support")}
        </Button>
      </Card>
    </Container>
  );
};

export default PaymentFailPage;
