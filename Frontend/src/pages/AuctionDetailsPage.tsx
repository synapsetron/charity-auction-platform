import { useEffect, useState, useRef } from "react";
import { useParams } from "react-router-dom";
import {
  Container,
  Row,
  Col,
  Button,
  Form,
  Tab,
  Tabs,
  Badge,
  Table,
} from "react-bootstrap";
import { AuctionResponseWithBidsDTO } from "../types/auctionTypes";
import { donateBid } from "../api/bid";
import { BidInfoDTO } from "../types/bidTypes";
import { getAuctionById } from "../api/auction";
import { getCurrentUser } from "../api/auth";
import { HubConnectionBuilder, HubConnection } from "@microsoft/signalr";
import Countdown from "react-countdown";
import { toast } from "react-toastify";

const API_URL = process.env.REACT_APP_BACKEND_URL;

const AuctionDetailsPage = () => {
  const [hasEnded, setHasEnded] = useState(false);
  const { id } = useParams<{ id: string }>();
  const [auction, setAuction] = useState<AuctionResponseWithBidsDTO | null>(null);
  const [currentUserId, setCurrentUserId] = useState<string | null>(null);
  const [bidAmount, setBidAmount] = useState<number>(0);
  const [key, setKey] = useState<string>("description");
  const connectionRef = useRef<HubConnection | null>(null);
  const isMounted = useRef(true);

  const refreshAuction = async () => {
    if (!id) return;
    try {
      const updatedAuction = await getAuctionById(id);
      setAuction(updatedAuction);
    } catch (error) {
      console.error("Failed to refresh auction:", error);
    }
  };

  useEffect(() => {
    isMounted.current = true;

    const setupAuctionPage = async () => {
      if (!id) return;

      try {
        const user = await getCurrentUser();
        setCurrentUserId(user.id);

        const auctionData = await getAuctionById(id);
        if (isMounted.current) {
          setAuction(auctionData);
          const highestBid = auctionData.bids.length > 0
            ? Math.max(...auctionData.bids.map(b => b.amount))
            : auctionData.startingPrice;
          setBidAmount(highestBid + 1);
        }

        if (connectionRef.current) {
          await connectionRef.current.stop();
          connectionRef.current = null;
        }

        const newConnection = new HubConnectionBuilder()
          .withUrl(`${API_URL}/auctionHub`)
          .withAutomaticReconnect()
          .build();

        newConnection.on("ReceiveNewBid", (bid: BidInfoDTO) => {
          if (isMounted.current) {
            setAuction(prev => {
              if (!prev) return prev;
              if (prev.bids.some(b => b.id === bid.id)) return prev;
              return { ...prev, bids: [bid, ...prev.bids] };
            });
          }
        });

        await newConnection.start();
        if (isMounted.current) {
          await newConnection.invoke("JoinAuctionGroup", id);
          connectionRef.current = newConnection;
        }
      } catch (error) {
        console.error("Error in setupAuctionPage:", error);
      }
    };

    setupAuctionPage();

    return () => {
      isMounted.current = false;
      if (connectionRef.current) {
        connectionRef.current.stop();
        connectionRef.current = null;
      }
    };
  }, [id]);

  if (!auction || !currentUserId) {
    return <Container className="py-5 text-center">Loading...</Container>;
  }

  const currentBid = auction.bids.length > 0
    ? Math.max(...auction.bids.map(b => b.amount))
    : auction.startingPrice;

  const highestBid = auction.bids.length > 0
    ? Math.max(...auction.bids.map(b => b.amount))
    : auction.startingPrice;

  const winningBid = auction.bids.find(b => b.amount === highestBid);
  const isWinner = winningBid?.userId === currentUserId;
  const userBid = auction.bids.find(b => b.userId === currentUserId && !b.isDonated);
  const canDonate = !isWinner && userBid;
  const totalRaised = auction.bids.reduce((sum, bid) => sum + bid.amount, 0);

  const handleBidSubmit = () => {
    toast.info(`Submitting bid: $${bidAmount}`, { position: "top-center" });
  };

  const handleDonateBid = async () => {
    if (!userBid) return;
    try {
      await donateBid(userBid.id);
      toast.success(`Thank you for donating your bid of $${userBid.amount}!`, { position: "top-center" });
      setAuction(prev => {
        if (!prev) return prev;
        const updatedBids = prev.bids.map(b =>
          b.id === userBid.id ? { ...b, isDonated: true } : b
        );
        return { ...prev, bids: updatedBids };
      });
    } catch (error) {
      console.error("Failed to donate bid", error);
      toast.error("Failed to donate your bid. Please try again.", { position: "top-center" });
    }
  };

  const handlePayment = async () => {
    const response = await fetch(`${API_URL}/api/payment/liqpay`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        userId: currentUserId,
        auctionId: auction.id,
        amount: winningBid?.amount,
        currency: "UAH",
        description: `Payment for auction '${auction.title}'`
      })
    });
    const html = await response.text();
    const win = window.open("", "_blank");
    if (win) {
      win.document.write(html);
      win.document.close();
    }
  };

  return (
    <Container className="py-5">
      <Row className="mb-5 align-items-center">
        <Col md={6}>
          <div className="p-3 bg-white rounded shadow-sm" style={{ position: "relative" }}>
            <img
              src={auction.imageUrl}
              alt={auction.title}
              className="w-100"
              style={{
                height: "550px",
                objectFit: "contain",
                borderRadius: "10px",
                opacity: !auction.isActive ? 0.5 : 1,
              }}
            />
            {!auction.isActive && isWinner && !auction.isSold && (
              <div
                style={{
                  position: "absolute",
                  top: "50%",
                  left: "50%",
                  transform: "translate(-50%, -50%)",
                  fontSize: "48px",
                  color: "#28a745",
                }}
              >
                🔒
              </div>
            )}
          </div>
        </Col>
        <Col md={6}>
          <h2 className="fw-bold mb-3">{auction.title}</h2>
          <p className="text-muted">{auction.description}</p>

          <div className="d-flex justify-content-between mb-3">
            <span>Item condition:</span>
            <Badge bg="success">New</Badge>
          </div>
          <div className="d-flex justify-content-between mb-3">
            <span>Item verified:</span>
            <Badge bg="danger">No</Badge>
          </div>

          <div className="bg-light p-4 rounded shadow-sm mb-4">
            <h5 className="text-center mb-3 fw-semibold">Time left</h5>
            <Countdown
              date={new Date(auction.endTime)}
              onComplete={() => {
                setHasEnded(true);
                refreshAuction();
              }}
              renderer={({ days, hours, minutes, seconds, completed }) => {
                if (completed) {
                  return <h4 className="text-danger text-center">Auction Ended</h4>;
                }
                return (
                  <Row className="text-center">
                    <Col><h4>{days}</h4><small>Days</small></Col>
                    <Col><h4>{hours}</h4><small>Hours</small></Col>
                    <Col><h4>{minutes}</h4><small>Minutes</small></Col>
                    <Col><h4>{seconds}</h4><small>Seconds</small></Col>
                  </Row>
                );
              }}
            />
          </div>

          <div className="mb-2">
            <h6 className="text-muted mb-1">Auction ends:</h6>
            <div className="fw-bold">{new Date(auction.endTime).toLocaleDateString()}</div>
            <div className="text-muted">Timezone: UTC 0</div>
          </div>

          <div className="d-flex justify-content-between my-3">
            <div>
              <span className="text-muted">Price:</span>
              <span className="fw-bold text-primary ms-2">${auction.startingPrice}</span>
            </div>
            <div>
              <span className="text-muted">Current Bid:</span>
              <span className="fw-bold text-success ms-2">${currentBid}</span>
            </div>
          </div>

          {!auction.isActive && isWinner && !auction.isSold && (
            <>
              <h5 className="text-success text-center mb-3">🎉 You have won the auction!</h5>
              <Button variant="success" className="w-100 mb-3" onClick={handlePayment}>
                Pay Now
              </Button>
            </>
          )}

          {auction.isActive && (
            <Form className="d-flex" onSubmit={(e) => {
              e.preventDefault();
              handleBidSubmit();
            }}>
              <Form.Control
                type="number"
                min={currentBid + 1}
                value={bidAmount}
                onChange={(e) => setBidAmount(Number(e.target.value))}
                className="me-2"
                style={{ height: "50px" }}
              />
              <Button
                type="submit"
                variant="success"
                style={{ height: "50px", width: "120px" }}
              >
                Submit
              </Button>
            </Form>
          )}

          {auction.bids.length > 0 &&
            new Date(auction.endTime) < new Date() &&
            canDonate && (
              <Button
                variant="outline-success"
                className="w-100 mt-3"
                onClick={handleDonateBid}
              >
                Donate your Bid (${userBid?.amount})
              </Button>
          )}

          <div className="bg-light p-3 rounded shadow-sm mt-4">
            <h5 className="text-center">Total Raised:</h5>
            <h4 className="text-success text-center">${totalRaised}</h4>
          </div>
        </Col>
      </Row>
      <Tabs
        activeKey={key}
        onSelect={(k) => setKey(k || "description")}
        className="mb-5 justify-content-center"
        variant="pills"
        fill
      >
        <Tab eventKey="description" title={<span className="fw-semibold px-3">Description</span>}>
          <p className="mt-3">{auction.description}</p>
        </Tab>
        <Tab eventKey="history" title={<span className="fw-semibold px-3">Auction History</span>}>
          <div className="mt-3">
            {auction.bids.length === 0 ? (
              <p>No bids yet</p>
            ) : (
              <Table striped bordered hover responsive>
                <thead>
                  <tr>
                    <th>Date</th>
                    <th>Bid Amount (USD)</th>
                    <th>User</th>
                  </tr>
                </thead>
                <tbody>
                  {auction.bids
                    .sort((a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime())
                    .map((bid) => (
                      <tr key={bid.id}>
                        <td>{new Date(bid.createdAt).toLocaleDateString()}</td>
                        <td>${bid.amount}</td>
                        <td>{bid.userName ?? "Unknown"}</td>
                      </tr>
                    ))}
                </tbody>
              </Table>
            )}
          </div>
        </Tab>
        <Tab eventKey="reviews" title={<span className="fw-semibold px-3">Reviews(2)</span>}>
          <p className="mt-3">No reviews yet. Be the first to review!</p>
        </Tab>
        <Tab eventKey="products" title={<span className="fw-semibold px-3">More Products</span>}>
          <p className="mt-3">Other products coming soon...</p>
        </Tab>
      </Tabs>
    </Container>
  );
};

export default AuctionDetailsPage;
