import './Favourites.css';
import React, { useEffect, useState } from 'react';
import axios from '../../axiosInstance';
import { jwtDecode } from 'jwt-decode';

const Favourites = () => {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [likedProducts, setLikedProducts] = useState([]);
  const BASE_URL = "http://52.200.252.181:8000";

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

  // ❤️ LIKE / UNLIKE
  const handleLike = async (productId) => {
    if (!accessToken) {
      alert("Please login first");
      return;
    }

    const decoded = jwtDecode(accessToken);
    const userId =decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];

    const isLiked = likedProducts.includes(productId);

    try {
      if (!isLiked) {
        await axios.post(
          '/api/addFavourites',
          { userId, productId },
          { headers: { Authorization: `Bearer ${accessToken}` } }
        );
        setLikedProducts((prev) => [...prev, productId]);
      } else {
        await axios.delete(
          `/deleteFavourites/${productId}?userId=${userId}`,
          { headers: { Authorization: `Bearer ${accessToken}` } }
        );

        setLikedProducts((prev) => prev.filter((x) => x !== productId));

        // 🔥 REMOVE FROM UI ALSO
        setProducts((prev) => prev.filter((p) => p.productId !== productId));
      }
    } catch (error) {
      console.log("Like Error:", error);
    }
  };

  // 📦 FETCH DATA
  useEffect(() => {
  const fetchData = async () => {
    try {
      setLoading(true);

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

      const favData = Array.isArray(favRes.data)
        ? favRes.data
        : favRes.data.data || favRes.data.favourites || [];

      const favIds = favData.map((f) => f.productId);

      const favouriteProducts = productRes.data.filter((p) =>
        favIds.includes(p.productId)
      );

      setProducts(favouriteProducts);
      setLikedProducts(favIds);

    } catch (error) {
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  fetchData();
}, [accessToken]); // ✅ FIX HERE

  return (
    <div className="page">

      {/* HEADER */}
      <div className="top-bar">
        <p>Wishlist ❤️ ({products.length})</p>
      </div>

      {/* PRODUCTS */}
      <div className="product-container">

        {loading ? (
          <div className="loader"></div>

        ) : products.length > 0 ? (

          products.map((p) => (
            <div key={p.productId} className="product-card">

              {/* ❤️ REMOVE FROM FAV */}
              <button
                className="like-btn liked"
                onClick={() => handleLike(p.productId)}
              >
                ❤️
              </button>

              <div className="product-tag">
                {p.shortDescription}
              </div>

              <div className="product-image">
                <img src={ p.imageUrl ? `${BASE_URL}${p.imageUrl}` : "https://dummyimage.com/300x200/ccc/000&text=No+Image" }
                  alt={p.productName}  />
              </div>

              <div className="product-info">
                <h3>{p.productName}</h3>

                <div className="rating">
                  {renderStars(p.reting)}
                  <span>({p.reviewCount || 0})</span>
                </div>

                <div className="price-row">
                  <span className="price">
                    ₹{p.price?.toLocaleString("en-IN")}
                  </span>
                  <button className="cart-btn">🛒</button>
                </div>
              </div>

            </div>
          ))

        ) : (
          <p>No favourite products yet ❤️</p>
        )}

      </div>
    </div>
  );
};

export default Favourites;