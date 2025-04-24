import { useEffect, useState } from "react";
import { Container, Row, Col } from "react-bootstrap";
import { AuctionCard } from "../components/auction/AuctionCard";
import { AuctionResponseWithBidsDTO } from "../types/auctionTypes";
import { getAllAuctions } from "../api/auction";

export const AuctionsListPage = () => {
    const [auctions, setAuctions] = useState<AuctionResponseWithBidsDTO[]>([]);

    useEffect(() => {
        const fetchAuctions = async () => {
            try {
                const response = await getAllAuctions();
                setAuctions(response);
            } catch (error) {
                console.error("Error fetching auctions", error);
            }
        };

        fetchAuctions();
    }, []);

    return (
        <Container className="py-5">
            <h1 className="mb-4">All Auctions</h1>
            <Row xs={1} md={2} lg={3} className="g-4">
                {auctions.map(auction => (
                    <Col key={auction.id}>
                        <AuctionCard auction={auction} />
                    </Col>
                ))}
            </Row>
        </Container>
    );
};
export default AuctionsListPage;
