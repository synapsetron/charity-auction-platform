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

const ServicesPage = () => {
  const navigate = useNavigate();
  const [isMounted, setIsMounted] = useState(false);

  useEffect(() => {
    // Триггерим анимацию при монтировании
    setIsMounted(true);
  }, []);

  const services = [
    {
      icon: <FaGavel size={36} className="text-success" />,
      title: "Auction Management",
      desc:
        "Easily list and manage auctions for both digital and physical items. " +
        "Schedule start and end times, set reserve prices, and control approval flows."
    },
    {
      icon: <FaHandshake size={36} className="text-success" />,
      title: "User Engagement",
      desc:
        "Boost participation with real-time bidding updates, notification alerts, and " +
        "personalized experiences tailored to each bidder."
    },
    {
      icon: <FaChartLine size={36} className="text-success" />,
      title: "Analytics & Reports",
      desc:
        "Track user behavior, bid frequency, donation history and more with powerful " +
        "real-time analytics and custom dashboards."
    },
    {
      icon: <FaRocket size={36} className="text-success" />,
      title: "Performance Optimization",
      desc:
        "Built for scale — lightning-fast performance with server-side rendering, caching, " +
        "and CDN delivery to ensure the best UX."
    },
    {
      icon: <FaUsers size={36} className="text-success" />,
      title: "Community Building",
      desc:
        "Create a loyal user base with leaderboards, donation streaks, trust scores, and " +
        "gamification to drive interaction and cause impact."
    },
    {
      icon: <FaLock size={36} className="text-success" />,
      title: "Security & Privacy",
      desc:
        "State-of-the-art security including two-factor authentication, end-to-end encryption, " +
        "audit logs, and GDPR compliance."
    }
  ];

  return (
    <Container className="py-5">
      <motion.h2
        className="text-center mb-5"
        initial={{ opacity: 0, y: -20 }}
        animate={isMounted ? { opacity: 1, y: 0 } : {}}
        transition={{ duration: 0.6 }}
      >
        What We Offer
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
                  <div className="mb-3">{s.icon}</div>
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
        <h4 className="mb-3">Ready to get started?</h4>
        <p>
          Let us help you launch your own charity auction platform with ease and confidence.
        </p>
        <Button
          variant="success"
          size="lg"
          onClick={() => navigate("/contact")}
        >
          Contact Our Team
        </Button>
      </motion.div>
    </Container>
  );
};

export default ServicesPage;
