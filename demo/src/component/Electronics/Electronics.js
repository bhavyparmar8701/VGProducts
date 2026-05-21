import React, { useState } from 'react';
import './Electronics.css';

// --- Icon Helpers ---
const StarIcon = ({ filled }) => (
  <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24" fill={filled ? "#F59E0B" : "none"} stroke={filled ? "#F59E0B" : "#D4D4D8"} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
    <polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"></polygon>
  </svg>
);

const CartIcon = () => (
  <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
    <circle cx="9" cy="21" r="1"></circle><circle cx="20" cy="21" r="1"></circle>
    <path d="M1 1h4l2.68 13.39a2 2 0 0 0 2 1.61h9.72a2 2 0 0 0 2-1.61L23 6H6"></path>
  </svg>
);

const CpuIcon = () => (
  <svg xmlns="http://www.w3.org/2000/svg" width="48" height="48" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1" strokeLinecap="round" strokeLinejoin="round" style={{ opacity: 0.2 }}>
    <rect x="4" y="4" width="16" height="16" rx="2" ry="2"></rect><rect x="9" y="9" width="6" height="6"></rect><line x1="9" y1="1" x2="9" y2="4"></line><line x1="15" y1="1" x2="15" y2="4"></line><line x1="9" y1="20" x2="9" y2="23"></line><line x1="15" y1="20" x2="15" y2="23"></line><line x1="20" y1="9" x2="23" y2="9"></line><line x1="20" y1="14" x2="23" y2="14"></line><line x1="1" y1="9" x2="4" y2="9"></line><line x1="1" y1="14" x2="4" y2="14"></line>
  </svg>
);

const CheckIcon = () => (
  <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="3" strokeLinecap="round" strokeLinejoin="round">
    <polyline points="20 6 9 17 4 12"></polyline>
  </svg>
);

const ChevronLeft = () => (
  <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
    <polyline points="15 18 9 12 15 6"></polyline>
  </svg>
);

const Electronics = () => {
  // Grid & Filter States
  const [activeCategory, setActiveCategory] = useState('All');
  const [sortBy, setSortBy] = useState('popular');
  const [priceFilter, setPriceFilter] = useState('all');
  const [ratingFilter, setRatingFilter] = useState(0);
  
  // Detail View States
  const [selectedProduct, setSelectedProduct] = useState(null);
  const [selectedColor, setSelectedColor] = useState('');
  const [activeTab, setActiveTab] = useState('description');

  // Expanded Tech Dummy Data
  const products = [
    { 
      id: 1, name: "MacBook Pro M3 Max", price: 319900, originalPrice: 349900, category: "Laptops", rating: 5, reviews: 342, image: "Laptop", brand: "Apple", stock: "In Stock",
      colors: [{ name: 'Space Black', hex: '#2E2E2E' }, { name: 'Silver', hex: '#E3E4E5' }],
      features: ["M3 Max chip for extreme workflows", "Up to 22 hours of battery life", "Liquid Retina XDR display"],
      description: "The most advanced Mac ever built. Powered by the M3 Max chip, it delivers game-changing performance and up to 22 hours of battery life.",
      specs: { "Screen": "16.2-inch Liquid Retina XDR", "Processor": "Apple M3 Max", "RAM": "36GB Unified Memory", "Storage": "1TB SSD" }
    },
    { 
      id: 2, name: "Samsung Galaxy S24 Ultra", price: 129999, originalPrice: 134999, category: "Smartphones", rating: 4, reviews: 856, image: "Smartphone", brand: "Samsung", stock: "In Stock",
      colors: [{ name: 'Titanium Gray', hex: '#5b5b5d' }, { name: 'Titanium Black', hex: '#1C1C1C' }],
      features: ["Galaxy AI built-in", "200MP Main Camera", "Snapdragon 8 Gen 3"],
      description: "Welcome to the era of mobile AI. With Galaxy S24 Ultra in your hands, you can unleash whole new levels of creativity and productivity.",
      specs: { "Display": "6.8-inch Dynamic AMOLED 2X", "Processor": "Snapdragon 8 Gen 3", "Camera": "200MP + 50MP + 12MP + 10MP", "Battery": "5000mAh" }
    },
    { 
      id: 3, name: "Sony WH-1000XM5", price: 29990, originalPrice: 34990, category: "Audio", rating: 5, reviews: 1205, image: "Headphones", brand: "Sony", stock: "In Stock",
      colors: [{ name: 'Black', hex: '#1C1C1C' }, { name: 'Platinum Silver', hex: '#E4E4E7' }, { name: 'Midnight Blue', hex: '#1E3A8A' }],
      features: ["Industry-leading noise cancellation", "Crystal clear hands-free calling", "Up to 30-hour battery life"],
      description: "The WH-1000XM5 headphones rewrite the rules for distraction-free listening. 2 processors control 8 microphones for unprecedented noise cancellation.",
      specs: { "Weight": "Approx. 250g", "Headphone Type": "Closed, dynamic", "Driver Unit": "30mm", "Battery": "Up to 30 hours" }
    },
    { 
      id: 4, name: "Apple Watch Series 9", price: 41900, originalPrice: 44900, category: "Wearables", rating: 4, reviews: 623, image: "Smartwatch", brand: "Apple", stock: "Low Stock",
      colors: [{ name: 'Midnight', hex: '#1E293B' }, { name: 'Starlight', hex: '#F8FAFC' }],
      features: ["S9 SiP for super bright display", "Double tap gesture", "Blood oxygen & ECG apps"],
      description: "Smarter. Brighter. Mightier. The most powerful chip in Apple Watch ever. A magical new way to use your watch without touching the screen.",
      specs: { "Case Size": "45mm or 41mm", "Display": "Always-On Retina display", "Water Resistance": "50 meters" }
    },
    { 
      id: 5, name: "Logitech MX Master 3S", price: 8995, originalPrice: 10995, category: "Accessories", rating: 5, reviews: 412, image: "Mouse", brand: "Logitech", stock: "In Stock",
      colors: [{ name: 'Graphite', hex: '#2A2A2A' }, { name: 'Pale Grey', hex: '#DCDCDC' }],
      features: ["8K DPI any-surface tracking", "Quiet Clicks", "MagSpeed Electromagnetic scrolling"],
      description: "Meet MX Master 3S – an iconic mouse remastered. Feel every moment of your workflow with even more precision, tactility, and performance.",
      specs: { "Sensor": "Darkfield high precision", "DPI": "200 to 8000 dpi", "Battery": "Rechargeable Li-Po (500 mAh)" }
    },
    { 
      id: 6, name: "Dell XPS 15 OLED", price: 189990, originalPrice: 195000, category: "Laptops", rating: 3, reviews: 215, image: "Laptop", brand: "Dell", stock: "In Stock",
      colors: [{ name: 'Platinum Silver', hex: '#E5E4E2' }],
      features: ["13th Gen Intel Core i7", "NVIDIA RTX 4050", "3.5K OLED Touch Display"],
      description: "Immerse yourself in content with bright, color rich panels with high resolution, with more viewing space to keep you productive.",
      specs: { "Screen": "15.6-inch 3.5K OLED", "Processor": "Intel Core i7-13700H", "RAM": "16GB DDR5", "Storage": "512GB NVMe SSD" }
    }
  ];

  const categories = ['All', 'Laptops', 'Smartphones', 'Audio', 'Wearables', 'Accessories'];

  // --- Filtering & Sorting Logic ---
  let filteredProducts = products.filter(p => {
    // 1. Category Filter
    if (activeCategory !== 'All' && p.category !== activeCategory) return false;
    
    // 2. Price Filter
    if (priceFilter === 'under50k' && p.price >= 50000) return false;
    if (priceFilter === '50k-100k' && (p.price < 50000 || p.price > 100000)) return false;
    if (priceFilter === 'over100k' && p.price <= 100000) return false;

    // 3. Rating Filter
    if (p.rating < ratingFilter) return false;

    return true;
  });

  // Sorting
  if (sortBy === 'price-low') {
    filteredProducts.sort((a, b) => a.price - b.price);
  } else if (sortBy === 'price-high') {
    filteredProducts.sort((a, b) => b.price - a.price);
  }

  // --- Handlers ---
  const handleProductClick = (product) => {
    setSelectedProduct(product);
    setSelectedColor(product.colors[0]?.name || '');
    setActiveTab('description');
    window.scrollTo(0, 0); 
  };

  const handleBackToGrid = () => {
    setSelectedProduct(null);
  };

  const handleAddToCart = () => {
    alert(`Added ${selectedProduct.name} (${selectedColor}) to cart!`);
    window.dispatchEvent(new Event("authChange"));
  };

  return (
    <div className="electronics-page">
      <div className="electronics-container">
        
        {!selectedProduct ? (
          /* --- VIEW 1: PRODUCT GRID & FILTERS --- */
          <>
            {/* Page Header */}
            <div className="electronics-header">
              <div className="header-titles">
                <h2>Electronics Hub</h2>
                <p>Showing {filteredProducts.length} results</p>
              </div>
              <div className="sort-control">
                <label>Sort by:</label>
                <select value={sortBy} onChange={(e) => setSortBy(e.target.value)}>
                  <option value="popular">Most Popular</option>
                  <option value="price-low">Price: Low to High</option>
                  <option value="price-high">Price: High to Low</option>
                </select>
              </div>
            </div>

            <div className="electronics-layout">
              {/* Sidebar Filters */}
              <aside className="electronics-sidebar">
                
                {/* Category Filter */}
                <div className="filter-section">
                  <h3>Categories</h3>
                  <ul className="category-list">
                    {categories.map(category => (
                      <li key={category}>
                        <button 
                          className={`category-btn ${activeCategory === category ? 'active' : ''}`}
                          onClick={() => setActiveCategory(category)}
                        >
                          {category}
                        </button>
                      </li>
                    ))}
                  </ul>
                </div>

                {/* Price Filter */}
                <div className="filter-section">
                  <h3>Price Range</h3>
                  <div className="filter-options">
                    <label className="radio-label">
                      <input type="radio" name="price" value="all" checked={priceFilter === 'all'} onChange={() => setPriceFilter('all')} />
                      All Prices
                    </label>
                    <label className="radio-label">
                      <input type="radio" name="price" value="under50k" checked={priceFilter === 'under50k'} onChange={() => setPriceFilter('under50k')} />
                      Under ₹50,000
                    </label>
                    <label className="radio-label">
                      <input type="radio" name="price" value="50k-100k" checked={priceFilter === '50k-100k'} onChange={() => setPriceFilter('50k-100k')} />
                      ₹50,000 - ₹1,00,000
                    </label>
                    <label className="radio-label">
                      <input type="radio" name="price" value="over100k" checked={priceFilter === 'over100k'} onChange={() => setPriceFilter('over100k')} />
                      Over ₹1,00,000
                    </label>
                  </div>
                </div>

                {/* Rating Filter */}
                <div className="filter-section">
                  <h3>Minimum Rating</h3>
                  <div className="filter-options">
                    <label className="radio-label">
                      <input type="radio" name="rating" value={0} checked={ratingFilter === 0} onChange={() => setRatingFilter(0)} />
                      Any Rating
                    </label>
                    <label className="radio-label rating-label">
                      <input type="radio" name="rating" value={4} checked={ratingFilter === 4} onChange={() => setRatingFilter(4)} />
                      4 <StarIcon filled={true} /> & Above
                    </label>
                    <label className="radio-label rating-label">
                      <input type="radio" name="rating" value={5} checked={ratingFilter === 5} onChange={() => setRatingFilter(5)} />
                      5 <StarIcon filled={true} /> Only
                    </label>
                  </div>
                </div>

              </aside>

              {/* Grid */}
              <main className="electronics-grid">
                {filteredProducts.length > 0 ? (
                  filteredProducts.map(product => (
                    <div key={product.id} className="tech-product-card" onClick={() => handleProductClick(product)}>
                      <div className="tech-image-wrapper">
                        <div className="tech-image-placeholder">
                          <CpuIcon />
                          <span className="tech-image-text">{product.image}</span>
                        </div>
                        <span className="tech-category-tag">{product.category}</span>
                      </div>
                      <div className="tech-details">
                        <h3 className="tech-name">{product.name}</h3>
                        <div className="tech-rating">
                          <div className="stars">
                            {[...Array(5)].map((_, i) => (
                              <StarIcon key={i} filled={i < product.rating} />
                            ))}
                          </div>
                          <span className="review-count">({product.reviews})</span>
                        </div>
                        <div className="tech-bottom">
                          <span className="tech-price">₹{product.price.toLocaleString('en-IN')}</span>
                          <button className="add-to-cart-btn" aria-label="Add to cart" onClick={(e) => { e.stopPropagation(); alert('Added to cart'); }}>
                            <CartIcon />
                          </button>
                        </div>
                      </div>
                    </div>
                  ))
                ) : (
                  <div className="no-results">
                    <h3>No products found</h3>
                    <p>Try adjusting your filters to see more results.</p>
                    <button onClick={() => { setActiveCategory('All'); setPriceFilter('all'); setRatingFilter(0); }} className="clear-filters-btn">
                      Clear All Filters
                    </button>
                  </div>
                )}
              </main>
            </div>
          </>
        ) : (
          
          /* --- VIEW 2: PRODUCT DETAIL (Same Page) --- */
          <div className="product-detail-view">
            
            {/* Breadcrumb Navigation */}
            <nav className="breadcrumb">
              <button onClick={handleBackToGrid} className="back-btn">
                <ChevronLeft /> Back to Electronics
              </button>
            </nav>

            {/* Top Section */}
            <div className="product-main-section">
              {/* Image Gallery */}
              <div className="product-gallery">
                <div className="main-image-placeholder">
                  <span className="tech-watermark">{selectedProduct.brand.toUpperCase()}</span>
                  <div className="headphone-shape"><CpuIcon /></div>
                  <span className="gallery-img-text">{selectedProduct.image}</span>
                </div>
              </div>

              {/* Info Section */}
              <div className="product-info">
                <div className="brand-tag">{selectedProduct.brand}</div>
                <h1 className="product-title">{selectedProduct.name}</h1>
                
                <div className="rating-row">
                  <div className="stars">
                    {[...Array(5)].map((_, i) => (
                      <StarIcon key={i} filled={i < selectedProduct.rating} />
                    ))}
                  </div>
                  <span className="review-text">{selectedProduct.rating}.0 ({selectedProduct.reviews} reviews)</span>
                  <span className="stock-status">✓ {selectedProduct.stock}</span>
                </div>

                <div className="price-section">
                  <span className="current-price">₹{selectedProduct.price.toLocaleString('en-IN')}</span>
                  {selectedProduct.originalPrice > selectedProduct.price && (
                    <>
                      <span className="original-price">₹{selectedProduct.originalPrice.toLocaleString('en-IN')}</span>
                      <span className="discount-tag">
                        {Math.round(((selectedProduct.originalPrice - selectedProduct.price) / selectedProduct.originalPrice) * 100)}% OFF
                      </span>
                    </>
                  )}
                </div>

                <div className="features-list">
                  <ul>
                    {selectedProduct.features.map((feature, index) => (
                      <li key={index}>{feature}</li>
                    ))}
                  </ul>
                </div>

                {/* Color Selector */}
                {selectedProduct.colors && selectedProduct.colors.length > 0 && (
                  <div className="variant-section">
                    <h3>Color: <span>{selectedColor}</span></h3>
                    <div className="color-options">
                      {selectedProduct.colors.map(color => (
                        <button
                          key={color.name}
                          className={`color-btn ${selectedColor === color.name ? 'selected' : ''}`}
                          style={{ backgroundColor: color.hex }}
                          onClick={() => setSelectedColor(color.name)}
                          aria-label={`Select ${color.name}`}
                        >
                          {selectedColor === color.name && <CheckIcon />}
                        </button>
                      ))}
                    </div>
                  </div>
                )}

                {/* Actions (Quantity selector removed here) */}
                <div className="action-section">
                  <div className="action-buttons">
                    <button className="btn-add-cart" onClick={handleAddToCart}>
                      Add to Cart
                    </button>
                    <button className="btn-buy-now">
                      Buy Now
                    </button>
                  </div>
                </div>
              </div>
            </div>

            {/* Bottom Tabs */}
            <div className="product-tabs-section">
              <div className="tabs-header">
                <button 
                  className={activeTab === 'description' ? 'active' : ''} 
                  onClick={() => setActiveTab('description')}
                >
                  Description
                </button>
                <button 
                  className={activeTab === 'specs' ? 'active' : ''} 
                  onClick={() => setActiveTab('specs')}
                >
                  Specifications
                </button>
              </div>

              <div className="tabs-content">
                {activeTab === 'description' && (
                  <div className="tab-pane description-pane">
                    <p>{selectedProduct.description}</p>
                  </div>
                )}
                
                {activeTab === 'specs' && (
                  <div className="tab-pane specs-pane">
                    <table className="specs-table">
                      <tbody>
                        {Object.entries(selectedProduct.specs).map(([key, value]) => (
                          <tr key={key}>
                            <td className="spec-key">{key}</td>
                            <td className="spec-value">{value}</td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  </div>
                )}
              </div>
            </div>
            
          </div>
        )}
      </div>
    </div>
  );
};

export default Electronics;