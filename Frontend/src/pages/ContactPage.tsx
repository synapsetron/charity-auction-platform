import {
  Container,
  Row,
  Col,
  Form,
  Button,
  Card,
  FloatingLabel
} from "react-bootstrap";
import {
  FaEnvelope,
  FaPhone,
  FaMapMarkerAlt,
  FaPaperPlane
} from "react-icons/fa";
import { motion } from "framer-motion";
import { useTranslation } from "react-i18next";

const ContactPage = () => {
  const { t } = useTranslation();

  return (
    <Container className="py-5">
      <motion.h2
        className="text-center mb-4"
        initial={{ opacity: 0, y: -20 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.6 }}
      >
        {t("contactpage.title")}
      </motion.h2>

      <motion.p
        className="text-center mb-5 text-muted"
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        transition={{ delay: 0.3 }}
      >
        {t("contactpage.subtitle")}
      </motion.p>

      <Row className="gy-4">
        <Col md={6}>
          <motion.div
            initial={{ opacity: 0, x: -50 }}
            animate={{ opacity: 1, x: 0 }}
            transition={{ duration: 0.6 }}
          >
            <Card className="p-4 shadow-lg border-0 rounded-4">
              <h4 className="mb-3 text-success">{t("contactpage.form_title")}</h4>
              <Form>
                <FloatingLabel
                  controlId="contactName"
                  label={t("contactpage.name_label")}
                  className="mb-3"
                >
                  <Form.Control type="text" placeholder={t("contactpage.name_label")} />
                </FloatingLabel>

                <FloatingLabel
                  controlId="contactEmail"
                  label={t("contactpage.email_label")}
                  className="mb-3"
                >
                  <Form.Control type="email" placeholder="name@example.com" />
                </FloatingLabel>

                <FloatingLabel
                  controlId="contactMessage"
                  label={t("contactpage.message_label")}
                  className="mb-3"
                >
                  <Form.Control as="textarea" style={{ height: "120px" }} />
                </FloatingLabel>

                <Button variant="success" type="submit" size="lg">
                  <FaPaperPlane className="me-2" />
                  {t("contactpage.submit_button")}
                </Button>
              </Form>
            </Card>
          </motion.div>
        </Col>

        <Col md={6}>
          <motion.div
            initial={{ opacity: 0, x: 50 }}
            animate={{ opacity: 1, x: 0 }}
            transition={{ duration: 0.6 }}
          >
            <Card className="p-4 shadow-lg border-0 rounded-4 bg-light h-100">
              <h4 className="mb-3 text-success">{t("contactpage.contact_title")}</h4>

              <div className="mb-4">
                <FaMapMarkerAlt className="me-3 text-success" />
                <strong>{t("contactpage.address_heading")}</strong>
                <p className="ms-4 text-muted">{t("contactpage.address_text")}</p>
              </div>

              <div className="mb-4">
                <FaPhone className="me-3 text-success" />
                <strong>{t("contactpage.phone_heading")}</strong>
                <p className="ms-4 text-muted">{t("contactpage.phone_text")}</p>
              </div>

              <div>
                <FaEnvelope className="me-3 text-success" />
                <strong>{t("contactpage.email_heading")}</strong>
                <p className="ms-4 text-muted">{t("contactpage.email_text")}</p>
              </div>
            </Card>
          </motion.div>
        </Col>
      </Row>
    </Container>
  );
};

export default ContactPage;
