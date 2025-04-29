import { Container, Row, Col, Card, Table, Button } from "react-bootstrap";
import { BsCashCoin } from "react-icons/bs";
import { CiMedal } from "react-icons/ci";
import { GiBarbedStar } from "react-icons/gi";
import { MdOutlineCategory, MdDashboard } from "react-icons/md";
import { HiOutlineUsers } from "react-icons/hi2";
import { NavLink } from "react-router-dom";

const Dashboard = () => {
  const role = "admin";

  const statCards = [
    { icon: <BsCashCoin size={50} />, label: "Баланс", value: 500 },
    { icon: <CiMedal size={50} />, label: "Виграні лоти", value: 2 },
    { icon: <GiBarbedStar size={50} />, label: "Мої товари", value: 100 },
  ];

  if (role === "admin") {
    statCards.push(
      { icon: <MdOutlineCategory size={50} />, label: "Всі товари", value: 50 },
      {
        icon: <HiOutlineUsers size={50} />,
        label: "Всі користувачі",
        value: 100,
      }
    );
  }

  return (
    <Container className="py-5">
      <h5 className="mb-4">Моя активність</h5>
      <Row className="gy-4">
        {statCards.map((card, idx) => (
          <Col md={4} key={idx}>
            <Card className="text-center border-success bg-light h-100">
              <Card.Body className="d-flex flex-column align-items-center gap-3">
                <div className="text-success">{card.icon}</div>
                <h2>{card.value}</h2>
                <Card.Text>{card.label}</Card.Text>
              </Card.Body>
            </Card>
          </Col>
        ))}
      </Row>
    </Container>
  );
};

export const UserProduct = () => {
  return (
    <Container className="pb-5">
      <h5 className="mb-4 mt-5">Покупки</h5>
      <Table bordered hover responsive className="bg-white rounded">
        <thead className="table-light">
          <tr>
            <th>Назва</th>
            <th>ID Ставки</th>
            <th>Сума ставки (USD)</th>
            <th>Зображення</th>
            <th>Статус</th>
            <th>Дія</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>Auction Title 01</td>
            <td>Bidding_HvO253gT</td>
            <td>1222.90</td>
            <td>
              <img
                src="https://bidout-react.vercel.app/images/bg/order1.png"
                alt="Auction"
                width={40}
                height={40}
              />
            </td>
            <td>
              <span className="text-success fw-semibold">Успішно</span>
            </td>
            <td className="text-center">
              <NavLink to="#" className="text-success">
                <MdDashboard size={22} />
              </NavLink>
            </td>
          </tr>
        </tbody>
      </Table>
    </Container>
  );
};

export default Dashboard;
