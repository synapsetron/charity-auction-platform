import { Container, Row, Col, Image, Card, Badge } from "react-bootstrap";
import { motion } from "framer-motion";
import { FaFlag, FaHeart, FaUsers, FaGavel, FaCheckCircle } from "react-icons/fa";

const AboutPage = () => {
  return (
    <Container className="py-5">
      <motion.h2
        className="text-center mb-4"
        initial={{ opacity: 0, y: -20 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.6 }}
      >
        Про проект "ДоброБід"
      </motion.h2>

      <motion.p
        className="text-center text-muted mb-5"
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        transition={{ delay: 0.3 }}
      >
        Платформа, яка об'єднує добро, мистецтво та незламний дух українців.
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
            <h4 className="text-success">Наша місія</h4>
            <p>
              Ми створили "ДоброБід", щоб кожна людина могла стати частиною великої
              справи — підтримки Збройних Сил України. Через аукціони добрих справ,
              кожна ставка наближає нас до перемоги та миру.
            </p>
            <p>
              Ми переконані: навіть маленький внесок, зроблений із серцем, має
              величезну силу.
            </p>
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
          <h4 className="mb-3 text-success">Що ми робимо?</h4>
          <ul>
            <li>Організовуємо онлайн-аукціони з реальними та цифровими лотами.</li>
            <li>Працюємо з перевіреними волонтерами, митцями, ветеранами.</li>
            <li>Забезпечуємо прозору обробку кожної ставки.</li>
            <li>Розвиваємо культуру благодійності серед молоді та бізнесу.</li>
            <li>Щотижня публікуємо звіти про розподіл коштів.</li>
          </ul>
        </Card>
      </motion.div>

      <motion.div
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        transition={{ delay: 0.3, duration: 0.6 }}
        className="mb-5"
      >
        <h4 className="mb-4 text-success text-center">Як це працює?</h4>
        <Row className="gy-4">
          <Col md={4}>
            <Card className="p-3 shadow-sm text-center">
              <FaGavel size={40} className="text-success mb-3" />
              <h5>Створи або обери лот</h5>
              <p>
                Вистав лот на благодійний аукціон або знайди той, який тебе надихає.
              </p>
            </Card>
          </Col>
          <Col md={4}>
            <Card className="p-3 shadow-sm text-center">
              <FaUsers size={40} className="text-success mb-3" />
              <h5>Роби ставку</h5>
              <p>
                Приймай участь у торгах. Кожна ставка — це крок до перемоги.
              </p>
            </Card>
          </Col>
          <Col md={4}>
            <Card className="p-3 shadow-sm text-center">
              <FaCheckCircle size={40} className="text-success mb-3" />
              <h5>Отримай лот або подяку</h5>
              <p>
                Переможи — і ми доставимо лот, або просто підтримай як донор.
              </p>
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
        <h4 className="mb-3">Понад 1 000 000 грн вже передано на потреби армії!</h4>
        <p>
          Завдяки добрим людям і прозорим процесам ми перетворюємо ставки в
          реальну допомогу.
        </p>
        <Badge bg="light" text="dark" className="fs-6">
          Підтримай наших захисників зараз
        </Badge>
      </motion.div>

      <motion.div
        className="text-center"
        initial={{ opacity: 0, y: 30 }}
        whileInView={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.6 }}
        viewport={{ once: true }}
      >
        <h4 className="text-success mb-3">Приєднуйся до нас</h4>
        <p>
          Ти можеш бути частиною великої місії — зробити добру справу, що змінить
          хід історії. Разом ми — сила!
        </p>
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