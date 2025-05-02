import { useEffect, useState } from "react";
import {
  Container,
  Row,
  Col,
  Form,
  InputGroup,
  Button,
  Spinner
} from "react-bootstrap";
import { AuctionCard } from "../components/auction/AuctionCard";
import { AuctionResponseWithBidsDTO } from "../types/auctionTypes";
import { getApprovedAuctions } from "../api/auction";
import { FaSearch } from "react-icons/fa";

export const AuctionsListPage = () => {
  const [auctions, setAuctions] = useState<AuctionResponseWithBidsDTO[]>([]);
  const [filtered, setFiltered] = useState<AuctionResponseWithBidsDTO[]>([]);
  const [search, setSearch] = useState("");
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchAuctions = async () => {
      try {
        const response = await getApprovedAuctions();
        setAuctions(response);
        setFiltered(response);
      } catch (error) {
        console.error("Error fetching auctions", error);
      } finally {
        setLoading(false);
      }
    };

    fetchAuctions();
  }, []);

  useEffect(() => {
    const keyword = search.toLowerCase();
    const filteredList = auctions.filter((a) =>
      a.title.toLowerCase().includes(keyword)
    );
    setFiltered(filteredList);
  }, [search, auctions]);

  return (
    <Container className="py-5">
      <div className="text-center mb-4">
        <h2 className="fw-bold">Explore Our Auctions</h2>
        <p className="text-muted">
          Discover exclusive items and support a great cause — every bid matters.
        </p>
      </div>

      <InputGroup className="mb-4">
        <Form.Control
          placeholder="Search auctions..."
          value={search}
          onChange={(e) => setSearch(e.target.value)}
        />
        <Button variant="success">
          <FaSearch />
        </Button>
      </InputGroup>

      {loading ? (
        <div className="text-center py-5">
          <Spinner animation="border" variant="success" />
        </div>
      ) : filtered.length === 0 ? (
        <div className="text-center py-5">
          <p className="text-muted">No auctions found matching your search.</p>
        </div>
      ) : (
        <Row xs={1} md={2} lg={3} className="g-4">
          {filtered.map((auction) => (
            <Col key={auction.id}>
              <AuctionCard auction={auction} />
            </Col>
          ))}
        </Row>
      )}
    </Container>
  );
};

export default AuctionsListPage;
