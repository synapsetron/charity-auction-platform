import { Container, Row, Col, Card, Spinner, Table } from "react-bootstrap";
import { useStats } from "../../hooks/useStats";
import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  ResponsiveContainer,
  LabelList,
} from "recharts";
import { useTranslation } from "react-i18next";

const Dashboard = () => {
  const { stats, loading } = useStats();
  const { t } = useTranslation();

  if (loading || !stats) {
    return (
      <Container className="py-5 text-center">
        <Spinner animation="border" />
      </Container>
    );
  }

  const isAdmin = stats.role === "Admin";
  const isSeller = stats.role === "Seller";

  const sellerCards = [
    { label: t("dashboard.balance"), value: stats.balance },
    { label: t("dashboard.my_wins"), value: stats.myWins },
    { label: t("dashboard.my_auctions"), value: stats.myAuctions },
  ];

  const adminCards = [
    { label: t("dashboard.total_users"), value: stats.userCount },
    { label: t("dashboard.total_auctions"), value: stats.auctionCount },
    { label: t("dashboard.active_auctions"), value: stats.activeAuctionCount },
    { label: t("dashboard.ended_auctions"), value: stats.endedAuctionCount },
    { label: t("dashboard.donations"), value: stats.donationCount },
    { label: t("dashboard.total_bids_sum"), value: stats.totalBidAmount },
    { label: t("dashboard.average_bid"), value: stats.avgBidAmount },
  ];

  return (
    <Container className="py-5">
      <h5 className="mb-4">
        {isAdmin ? t("dashboard.admin_title") : t("dashboard.seller_title")}
      </h5>

      <Row className="gy-4">
        {(isSeller ? sellerCards : adminCards).map((card, idx) => (
          <Col md={4} key={idx}>
            <Card className="text-center border-success bg-light h-100">
              <Card.Body>
                <h2>{card.value ?? 0}</h2>
                <Card.Text>{card.label}</Card.Text>
              </Card.Body>
            </Card>
          </Col>
        ))}
      </Row>

      {isAdmin && (stats.monthlyBids?.length ?? 0) > 0 && (
        <div className="mt-5">
          <h5 className="mb-3">{t("dashboard.monthly_bids")}</h5>
          <ResponsiveContainer width="100%" height={300}>
            <BarChart data={stats.monthlyBids ?? []}>
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis dataKey="month" />
              <YAxis allowDecimals={false} />
              <Tooltip />
              <Bar dataKey="count" fill="#28a745">
                <LabelList dataKey="count" position="top" />
              </Bar>
            </BarChart>
          </ResponsiveContainer>
        </div>
      )}

      {isAdmin && (stats.topAuctions?.length ?? 0) > 0 && (
        <div className="mt-5">
          <h5 className="mb-3">{t("dashboard.top_auctions")}</h5>
          <Table striped bordered>
            <thead>
              <tr>
                <th>{t("dashboard.auction_name")}</th>
                <th>{t("dashboard.bids")}</th>
              </tr>
            </thead>
            <tbody>
              {(stats.topAuctions ?? []).map((a) => (
                <tr key={a.auctionId}>
                  <td>{a.auctionName}</td>
                  <td>{a.bidCount}</td>
                </tr>
              ))}
            </tbody>
          </Table>
        </div>
      )}

      {isAdmin && (stats.topUsers?.length ?? 0) > 0 && (
        <div className="mt-5">
          <h5 className="mb-3">{t("dashboard.top_users")}</h5>
          <Table striped bordered>
            <thead>
              <tr>
                <th>{t("dashboard.user")}</th>
                <th>{t("dashboard.total_bids")}</th>
              </tr>
            </thead>
            <tbody>
              {(stats.topUsers ?? []).map((u) => (
                <tr key={u.userId}>
                  <td>{u.userName}</td>
                  <td>{u.totalBids}</td>
                </tr>
              ))}
            </tbody>
          </Table>
        </div>
      )}
    </Container>
  );
};

export default Dashboard;
