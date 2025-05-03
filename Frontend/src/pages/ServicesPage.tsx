import {
  Container,
  Row,
  Col,
  Card,
  Button
} from "react-bootstrap";
import {
  FaGavel,
  FaHandshake,
  FaChartLine,
  FaRocket,
  FaUsers,
  FaLock
} from "react-icons/fa";
import { motion } from "framer-motion";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";

const ServicesPage = () => {
  const navigate = useNavigate();
  const [isMounted, setIsMounted] = useState(false);
  const { t } = useTranslation();

  useEffect(() => {
    setIsMounted(true);
  }, []);

  const icons = [
    <FaGavel size={36} className="text-success" />,
    <FaHandshake size={36} className="text-success" />,
    <FaChartLine size={36} className="text-success" />,
    <FaRocket size={36} className="text-success" />,
    <FaUsers size={36} className="text-success" />,
    <FaLock size={36} className="text-success" />
  ];

  const services = t("servicespage.services", { returnObjects: true }) as {
    title: string;
    desc: string;
  }[];

  return (
    <Container className="py-5">
      <motion.h2
        className="text-center mb-5"
        initial={{ opacity: 0, y: -20 }}
        animate={isMounted ? { opacity: 1, y: 0 } : {}}
        transition={{ duration: 0.6 }}
      >
        {t("servicespage.title")}
      </motion.h2>

      <Row className="gy-4">
        {services.map((s, idx) => (
          <Col md={6} lg={4} key={idx}>
            <motion.div
              className="h-100"
              initial={{ opacity: 0, y: 30 }}
              animate={isMounted ? { opacity: 1, y: 0 } : {}}
              transition={{ duration: 0.4, delay: idx * 0.1 }}
            >
              <Card className="shadow text-center h-100">
                <Card.Body className="d-flex flex-column justify-content-center align-items-center p-4">
                  <div className="mb-3">{icons[idx]}</div>
                  <Card.Title className="fw-bold mb-2">{s.title}</Card.Title>
                  <Card.Text className="text-muted">{s.desc}</Card.Text>
                </Card.Body>
              </Card>
            </motion.div>
          </Col>
        ))}
      </Row>

      <motion.div
        className="text-center mt-5"
        initial={{ opacity: 0, scale: 0.95 }}
        animate={isMounted ? { opacity: 1, scale: 1 } : {}}
        transition={{ duration: 0.5 }}
      >
        <h4 className="mb-3">{t("servicespage.cta_title")}</h4>
        <p>{t("servicespage.cta_text")}</p>
        <Button
          variant="success"
          size="lg"
          onClick={() => navigate("/contact")}
        >
          {t("servicespage.cta_button")}
        </Button>
      </motion.div>
    </Container>
  );
};

export default ServicesPage;
