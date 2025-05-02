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

const Dashboard = () => {
  const { stats, loading } = useStats();

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
    { label: "Balance", value: stats.balance },
    { label: "My Wins", value: stats.myWins },
    { label: "My Auctions", value: stats.myAuctions },
  ];

  const adminCards = [
    { label: "Total Users", value: stats.userCount },
    { label: "Total Auctions", value: stats.auctionCount },
    { label: "Active Auctions", value: stats.activeAuctionCount },
    { label: "Ended Auctions", value: stats.endedAuctionCount },
    { label: "Donations", value: stats.donationCount },
    { label: "Total Bids Sum", value: stats.totalBidAmount },
    { label: "Average Bid", value: stats.avgBidAmount },
  ];

  return (
    <Container className="py-5">
      <h5 className="mb-4">{isAdmin ? "Admin Dashboard" : "My Activity"}</h5>
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
          <h5 className="mb-3">Monthly Bids Activity</h5>
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
          <h5 className="mb-3">Top Auctions</h5>
          <Table striped bordered>
            <thead>
              <tr>
                <th>Name</th>
                <th>Bids</th>
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
          <h5 className="mb-3">Top Users</h5>
          <Table striped bordered>
            <thead>
              <tr>
                <th>User</th>
                <th>Total Bids</th>
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
