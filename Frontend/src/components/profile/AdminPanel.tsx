import { useState } from "react";
import { Container, Tab, Nav } from "react-bootstrap";
import AuctionModeration from "./AuctionModeration";
import AllAuctionsManagement from "./AllAuctionsManagement";
import UserManagement from "./UserManagement";
import { useTranslation } from "react-i18next";

const AdminPanel = () => {
  const [key, setKey] = useState("moderation");
  const { t } = useTranslation();

  return (
    <Container className="py-4">
      <h3 className="mb-4">{t("admin.title")}</h3>
      <Tab.Container
        activeKey={key}
        onSelect={(k) => setKey(k || "moderation")}
      >
        <Nav variant="tabs" className="mb-4">
          <Nav.Item>
            <Nav.Link eventKey="moderation">{t("admin.tabs.moderation")}</Nav.Link>
          </Nav.Item>
          <Nav.Item>
            <Nav.Link eventKey="all-auctions">{t("admin.tabs.all_auctions")}</Nav.Link>
          </Nav.Item>
          <Nav.Item>
            <Nav.Link eventKey="users">{t("admin.tabs.users")}</Nav.Link>
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
