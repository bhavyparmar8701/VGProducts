import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "../../axiosInstance";
import { jwtDecode } from "jwt-decode";
import "./CartItems.css";

const Cart = () => {
  const navigate = useNavigate();
  const [cart, setCart] = useState(null);
  const [loading, setLoading] = useState(true);
  const [cartItems, setCartItems] = useState([]);
  const BASE_URL = "http://52.200.252.181:8000";

  // ✅ Get UserId
  const getUserId = () => {
    const token = localStorage.getItem("accessToken");
    if (!token) return null;

    const decoded = jwtDecode(token);
    return decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
  };

  const fetchCart = async () => {
    try {
      setLoading(true); 

      const token = localStorage.getItem("accessToken");
      const userId = getUserId();

      if (!token || !userId) return;

      const res = await axios.get(`/GetAllCartItem/${userId}`, {
        headers: { Authorization: `Bearer ${token}` }
      });

      console.log("API Data:", res.data); 
      setCartItems(res.data.items);

      const totalQty = (res.data.items || []).reduce(
        (sum, item) => sum + item.quantity,
        0
      );

      window.dispatchEvent(new CustomEvent("cartUpdated", { detail: totalQty }));
      
    } catch (err) {
      console.error("Cart error:", err);
    } finally {
      setLoading(false); 
    }
  };

  // ➖ Decrease
  const handleDecreaseCart = async (id) => {
    try {
      const token = localStorage.getItem("accessToken");
      const userId = getUserId();

      await axios.delete(`/DeleteCartItem/${id}/${userId}`, {
        headers: { Authorization: `Bearer ${token}` }
      });

      fetchCart(); 
    } catch (error) {
      console.error(error);
    }
  };

  // ➕ Increase
  const handleIncreaseCart = async (id) => {
    try {
      const token = localStorage.getItem("accessToken");
      const userId = getUserId();

      await axios.post(`/AddByIdCartItem/${id}/${userId}`, {}, {
        headers: { Authorization: `Bearer ${token}` }
      });

      fetchCart(); 
    } catch (error) {
      console.error(error);
    }
  };

  // 🗑 Clear cart
  const clearCart = async () => {
    try {
      const token = localStorage.getItem("accessToken");
      const userId = getUserId();

      await axios.delete(`/DeleteAllCartItem/${userId}`, {
        headers: { Authorization: `Bearer ${token}` }
      });

      fetchCart(); 
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    fetchCart();
  }, []);

  // ✅ Total (Calculated safely from cartItems array)
  const grandTotal = cartItems.reduce((sum, item) => sum + (item.subTotal || 0), 0);

  

  return (
    <div className="cart-page">

      <div className="cart-header">
        <h2 className="cart-heading">🛒 Your Cart</h2>

        {cartItems.length > 0 && (
          <button
            className="remove-all-btn"
            onClick={clearCart}
          >
            🗑 Remove All
          </button>
        )}
      </div>

      {/* 🔥 LOADER */}
      {loading ? (

        <div className="loader-container">
          <div className="spinner"></div>
        </div>

      ) : cartItems.length === 0 ? (

        /* 🔥 EMPTY CART */
        <div className="empty-cart-container">

          <div className="empty-cart-card">

            <h2>No Cart Items</h2>

            <p>
              Looks like you have not added
              anything to your cart yet.
            </p>

            <button className="shop-now-btn" onClick={() => navigate("/products")} > 🛍 Shop Now </button>

          </div>

          {/* 🔥 ORDER SUMMARY EMPTY */}
          <div className="cart-right">

            <h3>Order Summary</h3>

            <p>Total Items: 0</p>

            <h2 className="total-amount">₹0</h2>

            <button
              className="add-product-btn"
              onClick={() => navigate("/products")}
            >
              ➕ Add More Products
            </button>

            <button
              className="checkout-btn disabled-btn"
              disabled
            >
              🚀 Proceed to Checkout
            </button>

          </div>

        </div>

      ) : (

        /* 🔥 CART DATA */
        <div className="cart-wrapper">

          {/* LEFT */}
          <div className="cart-left">

            {cartItems.map((item) => (

              <div
                className="cart-item-card"
                key={item.id}
              >

                <div className="item-image">
                  <img
                    src={
                      item.imageUrl
                        ? `${BASE_URL}${item.imageUrl}`
                        : "https://dummyimage.com/300x200/ccc/000&text=No+Image"
                    }
                    alt={item.productName}
                  />
                </div>

                <div className="item-details">

                  <h3>{item.productName}</h3>

                  <p className="price">
                    ₹{item.price}
                  </p>

                  <div className="qty-box">

                    <button
                      onClick={() =>
                        handleDecreaseCart(item.id)
                      }
                    >
                      −
                    </button>

                    <span>{item.quantity}</span>

                    <button
                      onClick={() =>
                        handleIncreaseCart(item.id)
                      }
                    >
                      +
                    </button>

                  </div>

                </div>

                <div className="item-total">
                  ₹{item.subTotal}
                </div>

              </div>

            ))}

          </div>

          {/* RIGHT */}
          <div className="cart-right">

            <h3>Order Summary</h3>

            <p>
              Total Items:{" "}
              {cartItems.reduce(
                (t, i) => t + i.quantity,
                0
              )}
            </p>

            <h2 className="total-amount">
              ₹{grandTotal}
            </h2>

            <button
              className="add-product-btn"
              onClick={() => navigate("/products")}
            >
              ➕ Add More Products
            </button>

            <button
              className="checkout-btn"
              onClick={() =>
                navigate("/address", {
                  state: { cartItems },
                })
              }
            >
              🚀 Proceed to Checkout
            </button>

          </div>

        </div>

      )}
    </div>
  );
};

export default Cart;