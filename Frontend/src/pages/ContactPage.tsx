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

const ContactPage = () => {
  return (
    <Container className="py-5">
      <motion.h2
        className="text-center mb-4"
        initial={{ opacity: 0, y: -20 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.6 }}
      >
        Get in Touch With Us
      </motion.h2>

      <motion.p
        className="text-center mb-5 text-muted"
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        transition={{ delay: 0.3 }}
      >
        Have a question or just want to say hello? We'd love to hear from you!
      </motion.p>

      <Row className="gy-4">
        <Col md={6}>
          <motion.div
            initial={{ opacity: 0, x: -50 }}
            animate={{ opacity: 1, x: 0 }}
            transition={{ duration: 0.6 }}
          >
            <Card className="p-4 shadow-lg border-0 rounded-4">
              <h4 className="mb-3 text-success">Send Us a Message</h4>
              <Form>
                <FloatingLabel
                  controlId="contactName"
                  label="Your Name"
                  className="mb-3"
                >
                  <Form.Control type="text" placeholder="Name" />
                </FloatingLabel>

                <FloatingLabel
                  controlId="contactEmail"
                  label="Your Email"
                  className="mb-3"
                >
                  <Form.Control type="email" placeholder="name@example.com" />
                </FloatingLabel>

                <FloatingLabel
                  controlId="contactMessage"
                  label="Your Message"
                  className="mb-3"
                >
                  <Form.Control as="textarea" style={{ height: "120px" }} />
                </FloatingLabel>

                <Button variant="success" type="submit" size="lg">
                  <FaPaperPlane className="me-2" />
                  Send Message
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
              <h4 className="mb-3 text-success">Reach Us Directly</h4>

              <div className="mb-4">
                <FaMapMarkerAlt className="me-3 text-success" />
                <strong>Kyiv, Ukraine</strong>
                <p className="ms-4 text-muted">
                  Independence Square 1, Office 12
                </p>
              </div>

              <div className="mb-4">
                <FaPhone className="me-3 text-success" />
                <strong>+380 99 999 99 99</strong>
                <p className="ms-4 text-muted">Call us Mon–Fri, 10am–6pm</p>
              </div>

              <div>
                <FaEnvelope className="me-3 text-success" />
                <strong>support@dobro.bid</strong>
                <p className="ms-4 text-muted">We reply within 1 business day</p>
              </div>
            </Card>
          </motion.div>
        </Col>
      </Row>
    </Container>
  );
};

export default ContactPage;
