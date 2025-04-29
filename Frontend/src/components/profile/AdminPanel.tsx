import { useState } from "react";
import { Container, Tab, Nav } from "react-bootstrap";
import AuctionModeration from "./AuctionModeration";
import AllAuctionsManagement from "./AllAuctionsManagement";
import UserManagement from "./UserManagement";

const AdminPanel = () => {
  const [key, setKey] = useState("moderation");

  return (
    <Container className="py-4">
      <h3 className="mb-4">Панель адміністратора</h3>
      <Tab.Container
        activeKey={key}
        onSelect={(k) => setKey(k || "moderation")}
      >
        {/* Тепер вкладки ГОРИЗОНТАЛЬНО */}
        <Nav variant="tabs" className="mb-4">
          <Nav.Item>
            <Nav.Link eventKey="moderation">Модерація аукціонів</Nav.Link>
          </Nav.Item>
          <Nav.Item>
            <Nav.Link eventKey="all-auctions">Усі аукціони</Nav.Link>
          </Nav.Item>
          <Nav.Item>
            <Nav.Link eventKey="users">Користувачі</Nav.Link>
          </Nav.Item>
        </Nav>

        <Tab.Content>
          <Tab.Pane eventKey="moderation">
            <AuctionModeration />
          </Tab.Pane>
          <Tab.Pane eventKey="all-auctions">
            <AllAuctionsManagement />
          </Tab.Pane>
          <Tab.Pane eventKey="users">
            <UserManagement />
          </Tab.Pane>
        </Tab.Content>
      </Tab.Container>
    </Container>
  );
};

export default AdminPanel;
