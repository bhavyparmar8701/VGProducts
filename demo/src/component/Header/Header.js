import React, { useState, useEffect } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { jwtDecode } from "jwt-decode";
import './Header.css';
import axios from '../../axiosInstance';


const Icon = ({ name, className }) => {
  switch (name) {

    case 'heart':
      return (
        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
          <path d="M20.8 4.6a5.5 5.5 0 0 0-7.8 0L12 5.7l-1-1.1a5.5 5.5 0 0 0-7.8 7.8l1 1L12 21l7.8-7.6 1-1a5.5 5.5 0 0 0 0-7.8z"/>
        </svg>
      );

    // 🛒 CART (BETTER ICON)
    case 'shopping-bag':
      return (
        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
          <circle cx="9" cy="21" r="1"/>
          <circle cx="20" cy="21" r="1"/>
          <path d="M1 1h4l2.6 13.4a2 2 0 0 0 2 1.6h9.7a2 2 0 0 0 2-1.6L23 6H6"/>
        </svg>
      );

    // 👤 USER (FIXED PROPORTION)
    case 'user':
      return (
        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.2">
          <circle cx="12" cy="8" r="4"/>
          <path d="M4 20c2-4 6-6 8-6s6 2 8 6"/>
        </svg>
      );

    default:
      return null;
  }
};
  const EyeOpen = () => (
    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
      <path d="M2 12s4-7 10-7 10 7 10 7-4 7-10 7-10-7-10-7z" />
      <circle cx="12" cy="12" r="3" />
    </svg>
  );

  const EyeClose = () => (
    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
      <path d="M17.94 17.94A10.94 10.94 0 0 1 12 19c-6 0-10-7-10-7a18.7 18.7 0 0 1 5.06-5.94" />
      <path d="M1 1l22 22" />
      <path d="M9.88 9.88A3 3 0 0 0 14.12 14.12" />
      <path d="M14.12 9.88A3 3 0 0 1 9.88 14.12" />
      <path d="M22 12s-4-7-10-7c-1.2 0-2.3.2-3.3.6" />
    </svg>
  );

const Header = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(!!localStorage.getItem("accessToken"));
  const [showUserMenu, setShowUserMenu] = useState(false);
  const [user, setUser] = useState(null);
  const [showProfilePopup, setShowProfilePopup] = useState(false);
  const [showchangePasswordPopup, setShowchangePasswordPopup] = useState(false);
  const [showOldPassword, setShowOldPassword] = useState(false);
  const [showNewPassword, setShowNewPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);
  const [cartCount, setCartCount] = useState(0);

  const[profileData, setProfileData] = useState({
    firstName: "",
    lastName: "",
    phoneNumber: ""
  });

  const [passwordData, setPasswordData] = useState({
    oldPassword: "",
    newPassword: "",
    confirmPassword: ""
  });

  

  const navigate = useNavigate();
  const location = useLocation();

  const getUserId = () => {
      const token = localStorage.getItem("accessToken");
      if (!token) return null;
  
      const decoded = jwtDecode(token);
      return decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
    };
  

  useEffect(() => {
    const checkLoginAndFetch = async () => {
      const token = localStorage.getItem("accessToken");
      const userId = getUserId();

      if (!token || !userId) {
        setCartCount(0);
        setUser(null);
        setIsLoggedIn(false);
        return;
      }

      try {
        // ✅ WAIT for both APIs
        await fetchCartCount();
        await fetchUser();

        setIsLoggedIn(true);
      } catch (err) {
        console.error(err);
      }
    };

    const handleCartUpdate = (e) => {
      setCartCount(e.detail);
    };

    const handleAuthChange = () => {
      checkLoginAndFetch();
    };

    checkLoginAndFetch();

    window.addEventListener("cartUpdated", handleCartUpdate);
    window.addEventListener("authChange", handleAuthChange);

    return () => {
      window.removeEventListener("cartUpdated", handleCartUpdate);
      window.removeEventListener("authChange", handleAuthChange);
    };
  }, []);

  const handleLogout = () => {
    localStorage.clear();
    setUser(null);
    setShowUserMenu(false);
    window.dispatchEvent(new Event("authChange"));
    navigate('/login');
  };


    const fetchUser = async () => {
      try {
        const token = localStorage.getItem("accessToken");
        const userId = getUserId();

        if (!token || !userId)
        {
          setUser(null);
          return;
        }

        const resGetUser = await axios.get(`/GetuserById/${userId}`, {
          headers: {
            Authorization: `Bearer ${token}`
          }
        });

        const data =  resGetUser.data;
        setUser(data.data || data);

      } catch (err) {
        console.error("Error:", err);
        setUser(null);
      }
    };


  const handleOpenProfile = async () => {
    try{
      const token = localStorage.getItem("accessToken");
      
      const userId = getUserId();

        const resGetUserById = await axios.get(`/GetuserById/${userId}`, {
          headers: {
            Authorization: `Bearer ${token}`
          }
        });

       const data = resGetUserById.data.data || resGetUserById.data;

        setProfileData({
          firstName: data.firstName || "",
          lastName: data.lastName || "",
          phoneNumber: data.phoneNumber || ""
        });

        setShowProfilePopup(true);
    }
    catch(error)
    {
      console.log(error);
    }
  };

  const handleProfileChange = (e) => {
    setProfileData({
      ...profileData,
      [e.target.name]: e.target.value
    });
  };

  const handleProfileUpdate = async (e) =>{
    e.preventDefault();

    try{
      const token = localStorage.getItem("accessToken");
      const userId = getUserId();

       const resUpdateUser = await axios.put(`/UpdateUser/${userId}`,{
        firstName:profileData.firstName,
        lastName: profileData.lastName,
        phoneNumber: profileData.phoneNumber
      } , {
        headers: { Authorization: `Bearer ${token}`}
      });


      alert(resUpdateUser.data);
      setShowProfilePopup(false);
      window.dispatchEvent(new Event("authChange"));
    }
    catch(error)
    {
      console.log(error);
      if(error.response && error.response.data)
      {
        alert(error.response.data);
      }
      else
      {
        alert("Something Went Wrong");
      }
      setShowProfilePopup(false);
    }
  };
  
  const handleChangePassword = async (e) =>{
    e.preventDefault();
    try
    {
      if (!passwordData.oldPassword || !passwordData.newPassword || !passwordData.confirmPassword) {
        alert("All fields are required");
        return;
      }

      if (passwordData.newPassword !== passwordData.confirmPassword) {
        alert("Passwords do not match");
        return;
      }

      const token = localStorage.getItem("accessToken");
      const userId = getUserId();
      const resChangePassword = await axios.post(
        `/ChangePassword/${userId}`,
        {
          oldPassword: passwordData.oldPassword,
          newPassword: passwordData.newPassword,
          confirmPassword: passwordData.confirmPassword
        },
        {
          headers: { Authorization: `Bearer ${token}` }
        }
      );

      alert(resChangePassword.data.message || resChangePassword.data);
      setPasswordData({
        oldPassword: "",
        newPassword: "",
        confirmPassword: ""
      });

      setShowchangePasswordPopup(false);
    }
    catch(error)
    {
      console.log(error);
      if(error.response && error.response.data)
      {
        alert(error.response.data);
      }
      else
      {
        alert("Something Went Wrong");
      }
      setPasswordData();
      setShowchangePasswordPopup(false);
    }
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;

    setPasswordData((prev) => ({
      ...prev,
      [name]: value
    }));
  };

  const fetchCartCount = async () => {
    try {
      const token = localStorage.getItem("accessToken");
      const userId = getUserId();

       if (!token || !userId) return;

     const res = await axios.get(`/GetAllCartItem/${userId}`,
        {
          headers: { Authorization: `Bearer ${token}` }
        }
      );
      const totalQty = res.data.items.reduce(
        (sum, item) => sum + item.quantity,
        0
      );

      setCartCount(totalQty);
      

    } catch (error) {
      console.error(error);
    }
  };

  

 return (
    <>
      <header className="shopwave-header">

        <div className="top-bars">
          <span>Free shipping on orders above ₹499</span>
          <span className="limited-time-badge">Limited Time</span>
        </div>

        <div className="main-header">

          <div className="logo" onClick={() => navigate('/dashboard')}>
            <span className="logo-shop">VG</span>
            <span className="logo-wave">Products</span>
          </div>

          

          <div className="right-section">

            <div className="header-links">
              <span className={location.pathname === "/dashboard" ? "active-link" : ""} onClick={() => navigate("/dashboard")} >
                Home
              </span>

              <span className={location.pathname.startsWith("/products") ? "active-link" : ""} onClick={() => navigate("/products")} >
                Products
              </span>
            </div>

            <div className="user-actions">
              {!isLoggedIn ? (
                <button className="sign-in-btn" onClick={() => navigate("/login")}>
                  Sign in
                </button>
              ) : (
                <>
                 

                  <button className={`icon-btn ${location.pathname.startsWith("/favourites") ? "icon-active" : ""}`} onClick={() => navigate("/favourites")} >
                    <Icon name="heart" />
                  </button>

                  <button className={`icon-btn cart-icon-container ${ location.pathname.startsWith("/CartItems") ? "icon-active" : "" }`} onClick={() => navigate("/CartItems")} >
                    <Icon name="shopping-bag" /> {cartCount > 0 && ( <span className="cart-badge">{cartCount}</span> )}
                  </button>

                  <div className="icon-btn user-icon-container">
                    <div onClick={() => setShowUserMenu(!showUserMenu)}>
                      <Icon name="user" />
                    </div>

                    {showUserMenu && (
                      <div className="user-dropdown">
                        {!user ? (
                          <p>Loading...</p>
                        ) : (
                          <>
                            <p className="user-welcome">
                              Welcome, {user.firstName} {user.lastName} 👋
                            </p>

                            <p className="user-info">{user.email}</p>

                            <p className="user-info">
                              {user.phoneNumber || "No Phone Number"}
                            </p>

                            <hr />

                            <button className="dropdown-btn" onClick={handleOpenProfile}>
                              Update Details
                            </button>

                            <button className="dropdown-btn" onClick={() => setShowchangePasswordPopup(true)}>
                              Change Password
                            </button>

                            <button className="dropdown-btn logout-btn" onClick={handleLogout}>
                              Logout
                            </button>
                          </>
                        )}
                      </div>
                    )}
                  </div>
                </>
              )}
            </div>

          </div>
        </div>
      </header>

      {/* 🔥 ADD POPUP HERE */}
      {showProfilePopup && (
        <div className="popup-overlay">
          <div className="popup-container">

            <h2>Update Profile</h2>

            <form onSubmit={handleProfileUpdate}>

              <input name="firstName" placeholder="First Name" value={profileData.firstName} onChange={handleProfileChange} required />

              <input name="lastName" placeholder="Last Name" value={profileData.lastName} onChange={handleProfileChange} required />

              <input name="phoneNumber" placeholder="Phone Number" value={profileData.phoneNumber} onChange={handleProfileChange} required />

              <div className="popup-buttons">
                <button type="submit">Update</button>

                <button type="button" onClick={() => setShowProfilePopup(false)} > Cancel </button>
              </div>

            </form>
          </div>
        </div>
      )}

      {/* 🔥 ADD POPUP HERE */}
      {showchangePasswordPopup  && (
        <div className="popup-overlay">
          <div className="popup-container">

            <h2>Change Password</h2>

            <form onSubmit={handleChangePassword}>
               <div className="password-wrapper">
                <input type={showOldPassword ? "text" : "password"} name="oldPassword" placeholder="Old Password" value={passwordData.oldPassword} onChange={handleInputChange} required />
                <span onClick={() => setShowOldPassword(!showOldPassword)} className="eye-icon"> {showOldPassword ? <EyeClose /> : <EyeOpen />} </span>
              </div>

              <div className="password-wrapper">
                <input type={showNewPassword ? "text" : "password"} name="newPassword" placeholder="New Password" value={passwordData.newPassword} onChange={handleInputChange} required />
                <span onClick={() => setShowNewPassword(!showNewPassword)} className="eye-icon"> {showNewPassword ? <EyeClose /> : <EyeOpen />} </span>
              </div>

              <div className="password-wrapper">
                <input type={showConfirmPassword ? "text" : "password"} name="confirmPassword" placeholder="Confirm Password" value={passwordData.confirmPassword} onChange={handleInputChange} required />
                <span onClick={() => setShowConfirmPassword(!showConfirmPassword)} className="eye-icon"> {showConfirmPassword ? <EyeClose /> : <EyeOpen />} </span>
              </div>

              <div className="popup-buttons">
                <button type="submit">Update</button>
                <button type="button" onClick={() => setShowchangePasswordPopup(false)} > Cancel </button>
              </div>

            </form>
          </div>
        </div>
      )}

    </>
  );
};


export default Header;
