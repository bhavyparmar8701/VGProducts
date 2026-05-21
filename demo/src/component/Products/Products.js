import './Products.css';
import React, {  useEffect, useState } from 'react';
import axios from '../../axiosInstance';
import { useNavigate , useLocation } from 'react-router-dom';
import  {jwtDecode}  from 'jwt-decode';

const ProductList = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const subCategoryId = location.state?.subCategoryId;
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [likedProducts, setLikedProducts] = useState([]);
  const [categories, setCategories] = useState([]);
  const [subCategories, setSubCategories] = useState([]);
  const [openCategory, setOpenCategory] = useState(null); // for dropdown
  const [selectedSubCategory, setSelectedSubCategory] = useState(null);

  const BASE_URL = "http://52.200.252.181:8000";
  

  // ✅ FIXED STATE
  const [selectedProduct, setSelectedProduct] = useState(null);

  // FILTERS
  const [priceRange, setPriceRange] = useState("All");
  const [searchTerm, setSearchTerm] = useState("");
  const [reting, setReting] = useState(0);
  const [sort, setSort] = useState("popular");
  const [reviews, setReviews] = useState([]);
  
  
  const accessToken = localStorage.getItem("accessToken");

  // ⭐ STARS
  const renderStars = (value) => {
    const ratingValue = Math.round(parseFloat(value) || 0);

    return (
      <div className="stars">
        {[1, 2, 3, 4, 5].map((i) => (
          <span key={i} className={i <= ratingValue ? "star filled" : "star"}>
            ★
          </span>
        ))}
      </div>
    );
  };

  const getUserId = () => {
    const token = localStorage.getItem("accessToken");
      if (!token) return null;

        const decoded = jwtDecode(token);
        return decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
  };

  // ❤️ LIKE
  const handleLike = async(productId) => {
    
    const accessToken = localStorage.getItem("accessToken");

    if (!accessToken) {
    alert("Please login first");
    return;
    }

    const UserId = getUserId();

    const isLiked = likedProducts.includes(productId);
    try
    {
      if(!isLiked)
      {
        const ref = await axios.post(`/addFavourites`,
          {
            userId: UserId,
            productId: productId
          },
          {
            headers:{
              Authorization: `Bearer ${accessToken}`,
            },
          }
        );
        console.log("ref : ",ref);
        setLikedProducts((prev) => prev.includes(productId) ? prev : [...prev, productId]);
      }
      else
      {
        await axios.delete(`/deleteFavourites/${productId}?userId=${UserId}`,
          {
            headers:{
              Authorization: `Bearer ${accessToken}`,
            },
          }
        );
        setLikedProducts((prev) => prev.filter((x) => x !== productId));
      }
    }
    catch(error)
    {
      console.log("Link Error: ",error);
    }
  };


  //Fetch Reviews
  const fetchReviews = async (productId) => {
    try {
      const res = await axios.get(
        "/getallproductreview"
      );


      // ✅ FILTER HERE
      const filtered = res.data.filter((r) => {
        const reviewId = (r.productId || r.ProductId || "").toString().toLowerCase();
        const selectedId = (productId || "").toString().toLowerCase();

        return reviewId === selectedId;
      });

      setReviews(filtered);

    } catch (error) {
      console.log(error);
      setReviews([]);
    }
  };
  
  // 📦 FETCH
  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);

        const accessToken = localStorage.getItem("accessToken");

        const config = accessToken
          ? { headers: { Authorization: `Bearer ${accessToken}` } }
          : {};

        let userId = null;

        if (accessToken) {
          const decoded = jwtDecode(accessToken);
          userId =
            decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
        }

        const [productRes, favRes] = await Promise.all([
          axios.get("/getallproduct", config),

          accessToken
            ? axios.get(
                `/viewFavourites?userId=${userId}`,
                config
              )
            : Promise.resolve({ data: [] })
        ]);

        // ✅ FIXED FILTER LOGIC
        let filteredProducts = productRes.data;

        if (subCategoryId) {
          filteredProducts = productRes.data.filter(
            (p) =>
              p.subCategoryId?.toLowerCase() ===
              subCategoryId.toLowerCase()
          );
        }

        setProducts(filteredProducts);

        const favData = Array.isArray(favRes.data)
          ? favRes.data
          : favRes.data.data || favRes.data.favourites || [];

        const favIds = favData.map((f) => f.productId);

        setLikedProducts(favIds);

      } catch (error) {
        console.error("Fetch Error:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();   // ✅ ALWAYS CALL

  }, [subCategoryId]);

  const categoryFetchData = async () => {
    try {
      const res = await axios.get("/getallcategory");
      setCategories(res.data);
    } catch (err) {
      console.error(err);
    }
  };

  useEffect(() => {
    categoryFetchData();
  }, []);

  const fetchSubCategories = async () => {
    try {
      const res = await axios.get("/getallsubcategory");
      setSubCategories(Array.isArray(res.data) ? res.data : []);
    } catch (err) {
      console.error(err);
    }
  };

  useEffect(() => {
    fetchSubCategories();
  }, []);



  // FILTER
  let filteredProducts = products.filter((p) => {

    const searchMatch =
      p.productName.toLowerCase().includes(searchTerm.toLowerCase()) ||
      (p.shortDescription || "").toLowerCase().includes(searchTerm.toLowerCase());

    const subCategoryMatch =
      selectedSubCategory === null ||
      String(p.subCategoryId) === String(selectedSubCategory);

    // const filteredSubCategories = subCategories.filter((s) =>
    //   category === null
    //     ? true
    //     : String(s.categoryId) === String(category)
    // );

    let priceMatch = true;
    if (priceRange === "under50") priceMatch = p.price < 50000;
    if (priceRange === "50to1") priceMatch = p.price >= 50000 && p.price <= 100000;
    if (priceRange === "above1") priceMatch = p.price > 100000;

    const ratingValue = Number(p.reting) || 0;
    const selectedRating = Number(reting) || 0;

    const ratingMatch = selectedRating === 0
        ? true
        : ratingValue >= (selectedRating - 1) && ratingValue < selectedRating;

    return searchMatch &&  subCategoryMatch && priceMatch && ratingMatch;
  });

  // SORT
  if (sort === "low") filteredProducts.sort((a, b) => a.price - b.price);
  if (sort === "high") filteredProducts.sort((a, b) => b.price - a.price);

  const handleAddToCart = async (product) => {
     
    try{
      const token = localStorage.getItem("accessToken");
      const userId = getUserId();

      console.log("Selected Product:", product);

      if(!token || !userId)
      {
        alert("Please login first!");
        navigate("/Login");
        return;
      }
      
      await axios.post("/AddCartItem",
      {
        productId: product.productId,
        userId: userId
      } ,
      {
        headers: { Authorization: `Bearer ${token}` }
      });

      navigate("/CartItems", { state: { product: product } });
      alert("Add to Cart Successfully");
      
    }
    catch(error)
    {
      console.error(error.response?.data || error.message);
      alert(error.response?.data?.message || "Something went wrong");
    }
  };

  return (
    <div className="page">

      {/* HEADER */}
      <div className="top-bar">
        <p>Showing {filteredProducts.length} results</p>

        <input
              type="text"
              placeholder="Search products..."
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              className="search-input"
            />

        <div>
          <span>Sort by: </span>
          <select value={sort} onChange={(e) => setSort(e.target.value)}>
            <option value="popular">Most Popular</option>
            <option value="low">Price Low → High</option>
            <option value="high">Price High → Low</option>
          </select>
        </div>
      </div>

      <div className="layout">

        {/* SIDEBAR */}
        <div className="sidebar">

          

          <h3>Categories</h3>

          <div
            className={`category ${selectedSubCategory === null ? "active" : ""}`}
            onClick={() => setSelectedSubCategory(null)}
          >
            All
          </div>

          {categories.map((c) => {

            const subs = subCategories.filter(
              (s) => String(s.categoryId) === String(c.categoryId)
            );

            return (
              <div key={c.categoryId}>

                {/* CATEGORY CLICK → TOGGLE DROPDOWN */}
                <div
                  className="category"
                  onClick={() =>
                    setOpenCategory(
                      openCategory === c.categoryId ? null : c.categoryId
                    )
                  }
                >
                  {c.categoryName}
                </div>

                {/* SUBCATEGORY DROPDOWN */}
                {openCategory === c.categoryId && (
                  <div style={{ paddingLeft: "15px" }}>
                    
                    {subs.map((s) => (
                      <div
                        key={s.subCategoryId}
                        className={`subcategory ${ String(selectedSubCategory) === String(s.subCategoryId)
                            ? "active"
                            : ""
                        }`}
                        onClick={() => setSelectedSubCategory(s.subCategoryId)}
                      >
                        {s.subCategoryName}
                      </div>
                    ))}

                  </div>
                )}

              </div>
            );
          })}

          <h3>Price Range</h3>
          <label>
            <input type="radio" checked={priceRange === "All"} onChange={() => setPriceRange("All")} /> 
            All
          </label>

          <label>
            <input type="radio" checked={priceRange === "under50"} onChange={() => setPriceRange("under50")} /> 
            Under ₹50,000
          </label>

          <label>
            <input type="radio" checked={priceRange === "50to1"} onChange={() => setPriceRange("50to1")} /> 
            ₹50,000 - ₹1,00,000
          </label>

          <label>
            <input type="radio" checked={priceRange === "above1"} onChange={() => setPriceRange("above1")} /> 
            Above ₹1,00,000
          </label>

          <h3>Minimum Rating</h3>
          {[0,1,2,3,4,5].map((r) => (
            <label key={r}>
              <input
                type="radio"
                checked={reting === r}
                onChange={() => setReting(r)}
              />
              {r === 0 ? "Any Rating" : `${r} ★ & Above`}
            </label>
          ))}

        </div>

        {/* PRODUCTS */}
        <div className="product-container">

          {loading ? (
            <div className="loader"></div>

          ) : filteredProducts.length > 0 ? (

            filteredProducts.map((p) => (
              <div 
                key={p.productId} 
                className="product-card"
                onClick={() => {
                  setSelectedProduct(p);
                  fetchReviews(p.productId);
                  setSelectedSubCategory(null);
                  setOpenCategory(null);

                }}
              >

                {/* LIKE */}
                {accessToken && (
                  <button
                    className={`like-btn ${likedProducts.includes(p.productId) ? "liked" : ""}`}
                    onClick={(e) => {
                      e.stopPropagation();
                      handleLike(p.productId);
                    }}
                  >
                    {likedProducts.includes(p.productId) ? "❤️" : "🤍"}
                  </button>
                )}

                {/* TAG */}
                <div className="product-tag">
                  {p.shortDescription}
                </div>

                {/* IMAGE */}
                <div className="product-image">
                  <img src={ p.imageUrl ? `${BASE_URL}${p.imageUrl}` : "https://dummyimage.com/300x200/ccc/000&text=No+Image" }
                    alt={p.productName}
                  />
                </div>

                {/* INFO */}
                <div className="product-info">
                  <h3>{p.productName}</h3>

                  <div className="rating">
                    {renderStars(p.reting)}
                    <span className="review-count">
                      ({p.reviewCount || 0})
                    </span>
                  </div>

                  <div className="price-row">
                    <span className="price">
                      ₹{p.price?.toLocaleString("en-IN")}
                    </span>
                    <button className="cart-btn" onClick={(e) => { e.stopPropagation(); handleAddToCart(p); }} >🛒</button>
                  </div>
                </div>

              </div>
            ))

          ) : (
            <p>No Products Found</p>
          )}

        </div>

        {/* ✅ MODAL */}
        {selectedProduct && (
          <div className="modal-overlay" onClick={() => {setSelectedProduct(null); setReviews([]);} }>

            <div className="modal-content" onClick={(e) => e.stopPropagation()}>

              <button className="close-btn" onClick={() => setSelectedProduct(null)}>
                ✖
              </button>

              <div className="modal-body">
                 <img src={ selectedProduct.imageUrl ? `${BASE_URL}${selectedProduct.imageUrl}` : "https://dummyimage.com/300x200/ccc/000&text=No+Image" }
                    alt={selectedProduct.productName}
                  />

                <div>
                  <h2>{selectedProduct.productName}</h2>
                  <h3>₹{selectedProduct.price}</h3>
                  <p>{selectedProduct.description || "No description available"}</p>
                  <div className="rating-row">
                  {renderStars(selectedProduct.reting || 0)}
                  <span className="rating-number">
                    ({selectedProduct.reting || 0})
                  </span>
                </div>

                  <button className="cart-btn" onClick={(e) => { e.stopPropagation(); handleAddToCart(selectedProduct); }} >🛒</button>
                </div>
              </div>

              <div className="reviews-section">
                  <h3>Reviews</h3>

                  {reviews.length === 0 ? (
                    <p>No reviews available</p>
                  ) : (
                    reviews.map((review, index) => (
                      <div key={index} className="review-card">

                        {/* ✅ USER NAME */}
                        <h4>
                          {review.firstName} {review.lastName}
                        </h4>

                        {/* ⭐ STARS */}
                        <div className="rating-row">
                          {renderStars(review.rating)}
                        </div>

                        {/* 💬 COMMENT */}
                        <p>{review.comment}</p>

                      </div>
                    ))
                  )}
                </div>

            </div>
          </div>
        )}

      </div>
    </div>
  );
};

export default ProductList;