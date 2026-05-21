import React from 'react';
import { Link } from 'react-router-dom';
import './Footer.css';

// SVG Icon helper
const FooterIcon = ({ name, className }) => {
  switch (name) {
    case 'facebook':
      return (
        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" className={className}>
          <path d="M18 2h-3a5 5 0 0 0-5 5v3H7v4h3v8h4v-8h3l1-4h-4V7a1 1 0 0 1 1-1h3z"></path>
        </svg>
      );
    case 'twitter':
      return (
        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" className={className}>
          <path d="M23 3a10.9 10.9 0 0 1-3.14 1.53 4.48 4.48 0 0 0-7.86 3v1A10.66 10.66 0 0 1 3 4s-4 9 5 13a11.64 11.64 0 0 1-7 2c9 5 20 0 20-11.5a4.5 4.5 0 0 0-.08-.83A7.72 7.72 0 0 0 23 3z"></path>
        </svg>
      );
    case 'instagram':
      return (
        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" className={className}>
          <rect x="2" y="2" width="20" height="20" rx="5" ry="5"></rect>
          <path d="M16 11.37A4 4 0 1 1 12.63 8 4 4 0 0 1 16 11.37z"></path>
          <line x1="17.5" y1="6.5" x2="17.51" y2="6.5"></line>
        </svg>
      );
    case 'mail':
      return (
        <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" className={className}>
          <path d="M4 4h16c1.1 0 2 .9 2 2v12c0 1.1-.9 2-2 2H4c-1.1 0-2-.9-2-2V6c0-1.1.9-2 2-2z"></path>
          <polyline points="22,6 12,13 2,6"></polyline>
        </svg>
      );
    default:
      return null;
  }
};

const Footer = () => {
  return (
    <footer className="shopwave-footer">
      <div className="footer-top">
        <div className="footer-container grid-layout">

          {/* Company Info */}
          <div className="footer-col company-info">
            <Link to="/" className="footer-logo">
              <span className="logo-shop">VG</span>
              <span className="logo-wave">Products</span>
            </Link>

            <p className="company-desc">
              Your one-stop destination for the latest electronics, fashion, and home essentials.
            </p>

            {/* ✅ FIXED SOCIAL LINKS */}
            <div className="social-links">
              <a href="https://facebook.com" target="_blank" rel="noopener noreferrer" aria-label="Facebook" className="social-icon">
                <FooterIcon name="facebook" />
              </a>

              <a href="https://twitter.com" target="_blank" rel="noopener noreferrer" aria-label="Twitter" className="social-icon">
                <FooterIcon name="twitter" />
              </a>

              <a href="https://instagram.com" target="_blank" rel="noopener noreferrer" aria-label="Instagram" className="social-icon">
                <FooterIcon name="instagram" />
              </a>
            </div>
          </div>

          {/* Quick Links */}
          <div className="footer-col">
            <h3>Quick Links</h3>
            <ul className="footer-links">
              <li><Link to="/">Home</Link></li>
              <li><Link to="/about">About Us</Link></li>
              <li><Link to="/contact">Contact Us</Link></li>
              <li><Link to="/faq">FAQs</Link></li>
              <li><Link to="/terms">Terms & Conditions</Link></li>
            </ul>
          </div>

          {/* ✅ FIXED CATEGORY LINKS */}
          <div className="footer-col">
            <h3>Categories</h3>
            <ul className="footer-links">
              <li><Link to="/electronics">Electronics</Link></li>
              <li><Link to="/fashion">Fashion</Link></li>
              <li><Link to="/home-kitchen">Home & Kitchen</Link></li>
              <li><Link to="/beauty">Beauty & Personal Care</Link></li>
              <li><Link to="/sports">Sports & Outdoors</Link></li>
            </ul>
          </div>

          {/* Newsletter */}
          <div className="footer-col newsletter-col">
            <h3>Stay Updated</h3>
            <p>Subscribe to our newsletter to get updates.</p>

            <form className="newsletter-form" onSubmit={(e) => e.preventDefault()}>
              <div className="input-wrapper">
                <FooterIcon name="mail" className="mail-icon" />
                <input type="email" placeholder="Enter your email" required />
              </div>

              <button type="submit" className="subscribe-btn">
                Subscribe
              </button>
            </form>
          </div>

        </div>
      </div>

      {/* Bottom */}
      <div className="footer-bottom">
        <div className="footer-container bottom-bar-flex">
          <p>
            &copy; {new Date().getFullYear()} ShopWave. All rights reserved.
          </p>

          <div className="payment-methods">
            <span>Visa</span>
            <span>Mastercard</span>
            <span>PayPal</span>
            <span>UPI</span>
          </div>
        </div>
      </div>
    </footer>
  );
};

export default Footer;