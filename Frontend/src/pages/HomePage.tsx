import { Container, Row, Col, Button, Form } from "react-bootstrap";
import { FaSearch } from "react-icons/fa";
import zsuBackground from "../assets/images/zsu-hero-background.png";
import zsuSupport from "../assets/images/zsu_support.jpg";
import { useTranslation } from "react-i18next";

const HomePage = () => {
  const { t } = useTranslation();

  return (
    <>
      {/* Hero Section */}
      <div
        className="position-relative"
        style={{
          background: `linear-gradient(to left, rgba(4, 25, 75, 0.8) 0%, rgba(17, 65, 58, 0.6) 35%, rgba(17, 65, 58, 0.3) 55%, rgba(17, 65, 58, 0) 70%), url(${zsuBackground}) center/cover no-repeat`,
          color: "white",
          minHeight: "100vh",
          display: "flex",
          alignItems: "center",
          overflow: "hidden",
        }}
      >
        <Container className="py-5" style={{ fontSize: "1.3rem" }}>
          <Row className="align-items-center text-center text-md-start position-relative">
            <Col md={6} className="pe-md-5" style={{ zIndex: 2 }}>
              <h1 className="fw-bold mb-4 display-4">
                {t("homepage.title")}
              </h1>
              <p className="mb-5">{t("homepage.subtitle")}</p>
              <Form
                className="d-flex bg-white rounded-pill overflow-hidden px-4 py-3 align-items-center mx-auto mx-md-0"
                style={{ maxWidth: "600px" }}
              >
                <FaSearch className="text-muted me-3 fs-5" />
                <Form.Control
                  type="text"
                  placeholder={t("homepage.search_placeholder")}
                  className="border-0 shadow-none flex-grow-1 fs-5"
                />
                <Button variant="success" className="rounded-pill px-5 py-2 fs-5">
                  {t("homepage.search_button")}
                </Button>
              </Form>
              <div className="d-flex gap-5 justify-content-center justify-content-md-start mt-5">
                <div className="text-center">
                  <h4 className="fw-bold">82M+</h4>
                  <small>{t("homepage.total_product")}</small>
                </div>
                <div className="text-center">
                  <h4 className="fw-bold">82M+</h4>
                  <small>{t("homepage.total_auction")}</small>
                </div>
                <div className="text-center">
                  <h4 className="fw-bold">54</h4>
                  <small>{t("homepage.total_category")}</small>
                </div>
              </div>
            </Col>
          </Row>
        </Container>
      </div>

      {/* About Section */}
      <section
        style={{
          backgroundColor: "#fff",
          color: "#000",
          padding: "6rem 0",
          fontSize: "1.2rem",
        }}
      >
        <Container>
          <Row className="align-items-center">
            <Col md={6}>
              <h2 className="fw-bold mb-4 display-5">{t("homepage.about_title")}</h2>
              <p>{t("homepage.about_description")}</p>
              <Button variant="success" className="mt-4 px-4 py-2 fs-5">
                {t("homepage.about_button")}
              </Button>
            </Col>
            <Col md={6}>
              <img
                src={zsuSupport}
                alt="Support Ukraine"
                className="img-fluid rounded shadow"
              />
            </Col>
          </Row>
        </Container>
      </section>

      {/* How it works */}
      <section
        style={{
          backgroundColor: "#fff",
          color: "#000",
          padding: "6rem 0",
          fontSize: "1.1rem",
        }}
      >
        <Container>
          <h2 className="text-center fw-bold mb-5 display-6">
            {t("homepage.how_title")}
          </h2>
          <Row className="text-center">
            <Col md={4}>
              <h4 className="fw-semibold">{t("homepage.how_step_1")}</h4>
              <p>{t("homepage.how_step_1_desc")}</p>
            </Col>
            <Col md={4}>
              <h4 className="fw-semibold">{t("homepage.how_step_2")}</h4>
              <p>{t("homepage.how_step_2_desc")}</p>
            </Col>
            <Col md={4}>
              <h4 className="fw-semibold">{t("homepage.how_step_3")}</h4>
              <p>{t("homepage.how_step_3_desc")}</p>
            </Col>
          </Row>
        </Container>
      </section>

      {/* Popular Auctions */}
      <section
        style={{
          backgroundColor: "#fff",
          color: "#000",
          padding: "6rem 0",
          fontSize: "1.1rem",
        }}
      >
        <Container>
          <h2 className="text-center fw-bold mb-5 display-6">
            {t("homepage.popular_title")}
          </h2>
          <Row>
            {[1, 2, 3].map((i) => (
              <Col md={4} key={i}>
                <div className="border rounded p-4 shadow-sm mb-4">
                  <img
                    src={`/assets/images/auction${i}.jpg`}
                    alt={`Auction ${i}`}
                    className="img-fluid rounded mb-3"
                  />
                  <h5 className="fw-bold">{`${t("homepage.auction_title")} ${i}`}</h5>
                  <p>
                    {t("homepage.auction_price")}: {500 + i * 100} ₴
                  </p>
                  <Button variant="outline-success" size="lg">
                    {t("homepage.auction_button")}
                  </Button>
                </div>
              </Col>
            ))}
          </Row>
        </Container>
      </section>
    </>
  );
};

export default HomePage;
