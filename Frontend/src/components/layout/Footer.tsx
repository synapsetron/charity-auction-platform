// src/components/layout/Footer.tsx

import { Container, Row, Col } from 'react-bootstrap';
import { FaPhone, FaEnvelope, FaMapMarkerAlt, FaYoutube, FaInstagram, FaTwitter, FaLinkedin } from 'react-icons/fa';
import { Link } from 'react-router-dom';
import logo from '../../assets/icons/hammer.png';
const Footer = () => {
  return (
    <footer style={{ backgroundColor: '#11413a', color: 'white', paddingTop: '40px', paddingBottom: '40px' }}>
      <Container>
        <Row className="gy-4">
          {/* Logo + description */}
          <Col md={3}>
            <div className="mb-3 d-flex align-items-center gap-2">
              <img src={logo} alt="Logo" height="32" />
              <h5 className="mb-0 fw-bold">ДоброBid</h5>
            </div>
            <p>Created with the collaboration of over 60 of the world’s best Nuron Artists.</p>
            <hr style={{ borderColor: 'rgba(255,255,255,0.2)' }} />
            <h6>Get The Latest Nuron Updates</h6>
          </Col>

          {/* Auction Categories */}
          <Col md={2}>
            <h6 className="fw-bold mb-3">Auction Categories</h6>
            <ul className="list-unstyled">
              <li><Link to="/auctions/ending" className="text-white text-decoration-none">Ending Now</Link></li>
              <li><Link to="/auctions/vehicles" className="text-white text-decoration-none">Vehicles</Link></li>
              <li><Link to="/auctions/watches" className="text-white text-decoration-none">Watches</Link></li>
              <li><Link to="/auctions/electronics" className="text-white text-decoration-none">Electronics</Link></li>
            </ul>
          </Col>

          {/* About Us */}
          <Col md={2}>
            <h6 className="fw-bold mb-3">About Us</h6>
            <ul className="list-unstyled">
              <li><Link to="/about" className="text-white text-decoration-none">About Sbidu</Link></li>
              <li><Link to="/help" className="text-white text-decoration-none">Help</Link></li>
              <li><Link to="/affiliates" className="text-white text-decoration-none">Affiliates</Link></li>
              <li><Link to="/jobs" className="text-white text-decoration-none">Jobs</Link></li>
            </ul>
          </Col>

          {/* Help */}
          <Col md={2}>
            <h6 className="fw-bold mb-3">We are Here to Help</h6>
            <ul className="list-unstyled">
              <li><Link to="/account" className="text-white text-decoration-none">Your Account</Link></li>
              <li><Link to="/security" className="text-white text-decoration-none">Safe and Secure</Link></li>
              <li><Link to="/shipping" className="text-white text-decoration-none">Shipping Info</Link></li>
              <li><Link to="/contact" className="text-white text-decoration-none">Contact Us</Link></li>
            </ul>
          </Col>

          {/* Contacts + Socials */}
          <Col md={3}>
            <h6 className="fw-bold mb-3">Follow Us</h6>
            <p><FaPhone className="me-2" /> (646) 968-0608</p>
            <p><FaEnvelope className="me-2" /> help@engotheme.com</p>
            <p><FaMapMarkerAlt className="me-2" /> 1201 Broadway Suite</p>
            <div className="d-flex gap-3 mt-3">
              <a href="https://youtube.com" target="_blank" rel="noopener noreferrer" className="text-white"><FaYoutube /></a>
              <a href="https://instagram.com" target="_blank" rel="noopener noreferrer" className="text-white"><FaInstagram /></a>
              <a href="https://twitter.com" target="_blank" rel="noopener noreferrer" className="text-white"><FaTwitter /></a>
              <a href="https://linkedin.com" target="_blank" rel="noopener noreferrer" className="text-white"><FaLinkedin /></a>
            </div>
          </Col>
        </Row>
      </Container>
    </footer>
  );
};

export default Footer;
