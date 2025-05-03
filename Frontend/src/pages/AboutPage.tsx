import { Container, Row, Col, Image, Card, Badge } from "react-bootstrap";
import { motion } from "framer-motion";
import {
  FaUsers,
  FaHeart,
  FaFlag,
  FaGavel,
  FaCheckCircle,
} from "react-icons/fa";
import { useTranslation } from "react-i18next";

const AboutPage = () => {
  const { t } = useTranslation();

  return (
    <Container className="py-5">
      <motion.h2
        className="text-center mb-4"
        initial={{ opacity: 0, y: -20 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.6 }}
      >
        {t("aboutpage.title")}
      </motion.h2>

      <motion.p
        className="text-center text-muted mb-5"
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        transition={{ delay: 0.3 }}
      >
        {t("aboutpage.subtitle")}
      </motion.p>

      <Row className="align-items-center mb-5">
        <Col md={6}>
          <motion.div
            initial={{ opacity: 0, x: -50 }}
            animate={{ opacity: 1, x: 0 }}
            transition={{ duration: 0.6 }}
          >
            <Image
              src="https://upload.wikimedia.org/wikipedia/commons/5/5e/Flag_of_Ukraine.jpg"
              alt="Ukraine flag"
              fluid
              rounded
              className="shadow"
            />
          </motion.div>
        </Col>

        <Col md={6}>
          <motion.div
            initial={{ opacity: 0, x: 50 }}
            animate={{ opacity: 1, x: 0 }}
            transition={{ duration: 0.6 }}
          >
            <h4 className="text-success">{t("aboutpage.mission_title")}</h4>
            <p>{t("aboutpage.mission_p1")}</p>
            <p>{t("aboutpage.mission_p2")}</p>
          </motion.div>
        </Col>
      </Row>

      <motion.div
        className="mb-5"
        initial={{ opacity: 0, y: 30 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.6 }}
      >
        <Card className="p-4 shadow-lg border-0 bg-light">
          <h4 className="mb-3 text-success">
            {t("aboutpage.what_we_do_title")}
          </h4>
          <ul>
            {(
              t("aboutpage.what_we_do_list", {
                returnObjects: true,
              }) as string[]
            ).map((item, i) => (
              <li key={i}>{item}</li>
            ))}
          </ul>
        </Card>
      </motion.div>

      <motion.div
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        transition={{ delay: 0.3, duration: 0.6 }}
        className="mb-5"
      >
        <h4 className="mb-4 text-success text-center">
          {t("aboutpage.how_it_works_title")}
        </h4>
        <Row className="gy-4">
          <Col md={4}>
            <Card className="p-3 shadow-sm text-center">
              <FaGavel size={40} className="text-success mb-3" />
              <h5>{t("aboutpage.step_1_title")}</h5>
              <p>{t("aboutpage.step_1_desc")}</p>
            </Card>
          </Col>
          <Col md={4}>
            <Card className="p-3 shadow-sm text-center">
              <FaUsers size={40} className="text-success mb-3" />
              <h5>{t("aboutpage.step_2_title")}</h5>
              <p>{t("aboutpage.step_2_desc")}</p>
            </Card>
          </Col>
          <Col md={4}>
            <Card className="p-3 shadow-sm text-center">
              <FaCheckCircle size={40} className="text-success mb-3" />
              <h5>{t("aboutpage.step_3_title")}</h5>
              <p>{t("aboutpage.step_3_desc")}</p>
            </Card>
          </Col>
        </Row>
      </motion.div>

      <motion.div
        className="bg-success p-5 text-white rounded text-center mb-5"
        initial={{ opacity: 0 }}
        whileInView={{ opacity: 1 }}
        viewport={{ once: true }}
        transition={{ duration: 0.8 }}
      >
        <h4 className="mb-3">{t("aboutpage.total_transferred_title")}</h4>
        <p>{t("aboutpage.total_transferred_desc")}</p>
        <Badge bg="light" text="dark" className="fs-6">
          {t("aboutpage.support_badge")}
        </Badge>
      </motion.div>

      <motion.div
        className="text-center"
        initial={{ opacity: 0, y: 30 }}
        whileInView={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.6 }}
        viewport={{ once: true }}
      >
        <h4 className="text-success mb-3">{t("aboutpage.join_us_title")}</h4>
        <p>{t("aboutpage.join_us_desc")}</p>
        <Image
          src="https://armyinform.com.ua/wp-content/uploads/2022/12/dsc_0011.jpg"
          alt="Ukraine soldiers"
          fluid
          rounded
          style={{ maxHeight: "300px", marginTop: "1rem" }}
        />
      </motion.div>
    </Container>
  );
};

export default AboutPage;
