import { Container, Row, Col, Card, Button, Badge } from "react-bootstrap";
import { FaPenNib, FaUserAlt, FaCalendarAlt, FaTags } from "react-icons/fa";
import { useState } from "react";
import { useTranslation } from "react-i18next";

const blogPosts = Array.from({ length: 12 }).map((_, i) => ({
  id: i + 1,
  title: `Inspiring Mission #${i + 1}`,
  excerpt: `Discover the story behind our ${i % 2 === 0 ? "veterans" : "volunteers"} who are using digital auctions to drive real impact in their communities...`,
  author: `Admin`,
  date: `2025-05-${(i % 28) + 1}`,
  category: i % 2 === 0 ? "Fundraising" : "Awareness",
  image: `https://picsum.photos/seed/blog${i + 1}/600/300`,
}));

const BlogPage = () => {
  const [visiblePosts, setVisiblePosts] = useState(6);
  const { t } = useTranslation();

  return (
    <Container className="py-5">
      <h2 className="text-center mb-5">
        <FaPenNib className="me-2" /> {t("blogpage.title")}
      </h2>

      <Row className="gy-4">
        {blogPosts.slice(0, visiblePosts).map((post) => (
          <Col md={6} lg={4} key={post.id}>
            <Card className="shadow-sm h-100">
              <Card.Img variant="top" src={post.image} />
              <Card.Body>
                <Badge pill bg="success" className="mb-2">
                  <FaTags className="me-1" /> {post.category}
                </Badge>
                <Card.Title>{post.title}</Card.Title>
                <Card.Text>{post.excerpt}</Card.Text>
                <div className="d-flex justify-content-between text-muted small">
                  <span>
                    <FaUserAlt className="me-1" /> {post.author}
                  </span>
                  <span>
                    <FaCalendarAlt className="me-1" /> {post.date}
                  </span>
                </div>
              </Card.Body>
              <Card.Footer className="bg-white border-0 text-end">
                <Button variant="outline-success" size="sm">
                  {t("blogpage.read_more")}
                </Button>
              </Card.Footer>
            </Card>
          </Col>
        ))}
      </Row>

      {visiblePosts < blogPosts.length && (
        <div className="text-center mt-5">
          <Button
            onClick={() => setVisiblePosts((prev) => prev + 3)}
            variant="success"
            size="lg"
          >
            {t("blogpage.load_more")}
          </Button>
        </div>
      )}
    </Container>
  );
};

export default BlogPage;
