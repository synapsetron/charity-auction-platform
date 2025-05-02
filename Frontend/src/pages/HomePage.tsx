import { Container, Row, Col, Button, Form } from "react-bootstrap";
import { FaSearch } from "react-icons/fa";
import zsuBackground from "../assets/images/zsu-hero-background.png"; // новий фон
import zsuSupport from "../assets/images/zsu_support.jpg";



const handleTestPayment = async () => {
  try {
    const response = await fetch("http://localhost:5000/api/payment/liqpay", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        userId: "test-user",
        auctionId: "29fe88ee-701c-42c3-9a4f-7d1926d1c23f",
        amount: 250.0,
        currency: "UAH",
        description: "Test charity donation"
      })
    });

    const html = await response.text();
    const win = window.open("", "_blank");

    if (win) {
      win.document.write(html);
      win.document.close();
    } else {
      alert("🔒 Ваш браузер заблокував відкриття вікна для оплати. Дозвольте pop-up у налаштуваннях.");
    }
  } catch (err) {
    console.error("Error triggering test payment:", err);
    alert("❌ Не вдалося ініціювати оплату. Перевірте консоль.");
  }
};


const HomePage = () => {
  return (
    <>
      {/* Головна секція з фоном */}
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
                Build, sell & collect digital items.
              </h1>
              <p className="mb-5">
                Підтримай Збройні сили України через цифрові аукціони. Купуй
                унікальні лоти, допомагай разом!
              </p>
              <Form
                className="d-flex bg-white rounded-pill overflow-hidden px-4 py-3 align-items-center mx-auto mx-md-0"
                style={{ maxWidth: "600px" }}
              >
                <FaSearch className="text-muted me-3 fs-5" />
                <Form.Control
                  type="text"
                  placeholder="Search product..."
                  className="border-0 shadow-none flex-grow-1 fs-5"
                />
                <Button
                  variant="success"
                  className="rounded-pill px-5 py-2 fs-5"
                >
                  Search
                </Button>
              </Form>
              <div className="d-flex gap-5 justify-content-center justify-content-md-start mt-5">
                <div className="text-center">
                  <h4 className="fw-bold">82M+</h4>
                  <small>Total Product</small>
                </div>
                <div className="text-center">
                  <h4 className="fw-bold">82M+</h4>
                  <small>Total Auction</small>
                </div>
                <div className="text-center">
                  <h4 className="fw-bold">54</h4>
                  <small>Total Category</small>
                </div>
                <Button
                  onClick={handleTestPayment}
                  className="btn btn-warning mt-4"
                >
                  🔥 Тестова оплата 1₴
                </Button>
              </div>
            </Col>

            {/* Праву частину (Col md={6}) видаляємо — фон сам справляється */}
          </Row>
        </Container>
      </div>

      {/* Секція About */}
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
              <h2 className="fw-bold mb-4 display-5">
                Наша мета — підтримка ЗСУ через цифрові аукціони
              </h2>
              <p>
                Ми створили платформу, де кожен може виставити лот, прийняти
                участь в аукціоні та допомогти українській армії. Всі зібрані
                кошти йдуть виключно на потреби захисників.
              </p>
              <Button variant="success" className="mt-4 px-4 py-2 fs-5">
                Дізнатися більше
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
            Як працює платформа
          </h2>
          <Row className="text-center">
            <Col md={4}>
              <h4 className="fw-semibold">1. Створи лот</h4>
              <p>
                Опиши цифровий товар чи послугу, яку ти хочеш виставити на
                аукціон.
              </p>
            </Col>
            <Col md={4}>
              <h4 className="fw-semibold">2. Приймай участь</h4>
              <p>
                Ставки робляться онлайн. Переможець — найвища ставка до
                дедлайну.
              </p>
            </Col>
            <Col md={4}>
              <h4 className="fw-semibold">3. Допомагай ЗСУ</h4>
              <p>Усі кошти автоматично йдуть до волонтерського фонду.</p>
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
            Популярні аукціони
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
                  <h5 className="fw-bold">Унікальний NFT арт {i}</h5>
                  <p>Стартова ціна: {500 + i * 100} ₴</p>
                  <Button variant="outline-success" size="lg">
                    Детальніше
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
