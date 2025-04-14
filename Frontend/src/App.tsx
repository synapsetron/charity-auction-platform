import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Header from './components/layout/Header';

import HomePage from './pages/HomePage';
import ProductPage from './pages/AuctionsListPage'; // Переіменуй файл якщо хочеш більш читабельно
import BlogPage from './pages/BlogPage';
import ServicesPage from './pages/ServicesPage';
import ContactPage from './pages/ContactPage';

const App = () => {
  return (
    <Router>
      <Header />
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/product" element={<ProductPage />} />
        <Route path="/blog" element={<BlogPage />} />
        <Route path="/services" element={<ServicesPage />} />
        <Route path="/contact" element={<ContactPage />} />
      </Routes>
    </Router>
  );
};

export default App;
