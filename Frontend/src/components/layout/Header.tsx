import { useEffect, useState } from "react";
import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import Button from "react-bootstrap/Button";
import logo from '../../assets/icons/hammer.png';
import { Link } from "react-router-dom";
import { FaSearch } from "react-icons/fa";

function Header() {
  const [isScrolled, setIsScrolled] = useState(false);

  useEffect(() => {
    const onScroll = () => {
      setIsScrolled(window.scrollY > 10);
    };

    window.addEventListener("scroll", onScroll);
    return () => window.removeEventListener("scroll", onScroll);
  }, []);

  return (
    <Navbar
      expand="lg"
      sticky="top"
      style={{
        transition: "background-color 0.3s, color 0.3s",
        backgroundColor: isScrolled ? "#fff" : "#11413a",
        color: isScrolled ? "#000" : "#fff"
      }}
      variant={isScrolled ? "light" : "dark"}
      className={isScrolled ? "shadow-sm" : ""}
    >
      <Container fluid className="px-4">
        <Navbar.Brand
          as={Link}
          to="/"
          className="d-flex align-items-center gap-2"
        >
          <img
            src={logo}
            alt="Logo"
            style={{ height: "32px" }}
          />
          <span className="fw-bold" style={{ color: isScrolled ? "#000" : "#fff" }}>
            ДоброBid
          </span>
        </Navbar.Brand>

        <Navbar.Toggle aria-controls="main-navbar" />

        <Navbar.Collapse id="main-navbar" className="justify-content-between">
          <Nav className="gap-3">
            {["Home", "Product", "Blog", "About", "Services", "Contact"].map((text) => (
              <Nav.Link
                as={Link}
                to={`/${text.toLowerCase()}`}
                key={text}
                className={isScrolled ? "text-dark" : "text-white"}
              >
                {text}
              </Nav.Link>
            ))}
          </Nav>

          <div className="d-flex align-items-center gap-3">
            <FaSearch style={{ color: isScrolled ? "black" : "white", cursor: "pointer" }} />

            <Nav.Link
              as={Link}
              to="/login"
              className={isScrolled ? "text-dark" : "text-white"}
            >
              Sign in
            </Nav.Link>

            <Button as="div" variant={isScrolled ? "dark" : "light"} className="rounded-pill px-4">
              <Link
                to="/register"
                className={`text-decoration-none d-block px-3 py-1 ${isScrolled ? "text-white" : "text-dark"}`}
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
