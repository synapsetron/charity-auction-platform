import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import Button from "react-bootstrap/Button";
import CustomLink from "../common/СustomLink";
import { Link } from "react-router-dom";
import { FaSearch } from "react-icons/fa"; // для іконки пошуку

function Header() {
  return (
    <Navbar
      expand="lg"
      style={{ backgroundColor: "#11413a" }}
      variant="dark"
      sticky="top"
    >
      <Container fluid className="px-4">
        {/* Logo + Site Name */}
        <Navbar.Brand
          as={Link}
          to="/"
          className="d-flex align-items-center gap-2"
        >
          <img
            src="/logo.png" // встав сюди свій логотип або змінюй на placeholder
            alt="Logo"
            style={{ height: "32px" }}
          />
          <span className="fw-bold">ДоброBid </span>
        </Navbar.Brand>

        {/* Mobile Toggle */}
        <Navbar.Toggle aria-controls="main-navbar" />

        {/* Navigation Links */}
        <Navbar.Collapse id="main-navbar" className="justify-content-between">
          <Nav className="gap-3">
            <Nav.Link as={Link} to="/">
              Home
            </Nav.Link>
            <Nav.Link as={Link} to="/product">
              Product
            </Nav.Link>
            <Nav.Link as={Link} to="/blog">
              Blog
            </Nav.Link>
            <Nav.Link as={Link} to="/about">
              About
            </Nav.Link>
            <Nav.Link as={Link} to="/services">
              Services
            </Nav.Link>
            <Nav.Link as={Link} to="/contact">
              Contact
            </Nav.Link>
          </Nav>

          {/* Right side: Search + Auth */}
          <div className="d-flex align-items-center gap-3">
            <FaSearch style={{ color: "white", cursor: "pointer" }} />

            <Nav.Link as={Link} to="/login" className="text-white">
              Sign in
            </Nav.Link>

            <Button as="div" variant="light" className="rounded-pill px-4">
              <Link
                to="/register"
                className="text-decoration-none text-dark d-block px-3 py-1"
              >
                Join
              </Link>
            </Button>
          </div>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default Header;
