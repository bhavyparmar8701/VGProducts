import React, { useEffect, useState, useCallback  } from "react";
import axios from "../../axiosInstance";
import { jwtDecode } from "jwt-decode";
import "./Address.css";
import { useNavigate, useLocation, userLocation } from "react-router-dom";

const Address = () => {
  const [countries, setCountries] = useState([]);
  const [states, setStates] = useState([]);
  const [cities, setCities] = useState([]);
  const [addresses, setAddresses] = useState([]);
  const [editId, setEditId] = useState(null);
  const [showPopup, setShowPopup] = useState(false);
  const [isEditLoading, setIsEditLoading] = useState(false);
  const [showAddPopup, setShowAddPopup] = useState(false);
  const [loading, setLoading] = useState(false);
  const [selectedAddressId, setSelectedAddressId] = useState(null);
  const location =useLocation();
  const cartItems = location.state?.cartItems || [];
  const BASE_URL = "http://52.200.252.181:8000";
  const navigate = useNavigate();

  const getUserId = () => {
    const token = localStorage.getItem("accessToken");
    if (!token) return null;

    const decoded = jwtDecode(token);
    return decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
  };

  
  const [addFormData, setAddFormData] = useState({
    countryId: "",
    stateId: "",
    cityId: "",
    addressLine1: "",
    addressLine2: "",
    landmark: "",
    pincode: "",
    saveAs: "",
  });

  const [editFormData, setEditFormData] = useState({
    countryId: "",
    stateId: "",
    cityId: "",
    addressLine1: "",
    addressLine2: "",
    landmark: "",
    pincode: "",
    saveAs: "",
  });

  const handleAddChange = (e) => {
    setAddFormData({ ...addFormData, [e.target.name]: e.target.value });
  };

  const handleEditChange = (e) => {
  setEditFormData({ ...editFormData, [e.target.name]: e.target.value }); };

  // ✅ Load Countries
  useEffect(() => {
    const token = localStorage.getItem("accessToken");

    axios.get("/getCountry", {
      headers: { Authorization: `Bearer ${token}` }
    })
      .then(res => {
        const data = res.data?.data || res.data || [];
        setCountries(Array.isArray(data) ? data : []);
      })
      .catch(err => console.log(err));
  }, []);

  // ✅ Load States
  useEffect(() => {
    if (addFormData.countryId) {

      axios.get(`/getStateById/${addFormData.countryId}`)
        .then(res => {
          let data = res.data?.data || res.data;

          if (data?.message) data = [data.message];

          setStates(Array.isArray(data) ? data : []);
        })
        .catch(err => console.log(err));

      // 🔥 Reset state + city
      setAddFormData(prev => ({
        ...prev,
        stateId: "",
        cityId: ""
      }));
      setCities([]);
    }
  }, [addFormData.countryId]);

  // ✅ Load Cities
  useEffect(() => {
    if (addFormData.stateId) {

      axios.get(`/getCityById/${addFormData.stateId}`)
        .then(res => {
          let data = res.data?.data || res.data;

          if (data?.message) data = [data.message];

          setCities(Array.isArray(data) ? data : []);
        })
        .catch(err => console.log(err));

      // 🔥 Reset city
      setAddFormData(prev => ({
        ...prev,
        cityId: ""
      }));
    }
  }, [addFormData.stateId]);

  // ✅ Handle Change


  // ✅ Submit
  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const token = localStorage.getItem("accessToken");
      const userId = getUserId();
      await axios.post("/addaddress",
      {
          UserId: userId,
          LandMark: addFormData.landmark,
          AddressLine1: addFormData.addressLine1,
          AddressLine2: addFormData.addressLine2,
          CountryId: addFormData.countryId,
          StateId: addFormData.stateId,
          CityId: addFormData.cityId,
          Pincode: addFormData.pincode,
          SaveAs: addFormData.saveAs
      }, {
        headers: { Authorization: `Bearer ${token}` }
      });
      
      
      alert("Address Saved");
      

      // 🔥 Reset form
      setAddFormData({
        countryId: "",
        stateId: "",
        cityId: "",
        addressLine1: "",
        addressLine2: "",
        landmark: "",
        pincode: "",
        saveAs: "",
      });

      setStates([]);
      setCities([]);

    } catch (err) {
        console.log("SAVE ERROR FULL:", err); 
        console.log("SAVE ERROR RESPONSE:", err.response);
      }
  };
  
  const fetchAddresses = useCallback(async () => {
    try {
      const token = localStorage.getItem("accessToken");
      const userId = getUserId();

      if (!token || !userId) return;

      const res = await axios.get("/getAddress", {
        params: { userId },
        headers: { Authorization: `Bearer ${token}` }
      });

      const data = res.data.data || res.data ;
      setAddresses(Array.isArray(data) ? data : []);


    } catch (error) {
      console.error("Error fetching addresses:", error);
      setAddresses([]);
    }
  }, []);

  useEffect(() => {
    fetchAddresses();
  }, [fetchAddresses]);

    const handleDelete = async (addressId) => {
      try {
        const token = localStorage.getItem("accessToken");
        const userId = getUserId();

        await axios.delete(`/deleteAddress/${addressId}/${userId}`, {
          headers: { Authorization: `Bearer ${token}` }
        });

        alert("Address Deleted ");
        window.location.reload();

      } catch (err) {
        console.log("DELETE ERROR:", err);
      }
    };

  const handleEdit = async (addr) => {
    const token = localStorage.getItem("accessToken");
    const userId = getUserId();

    try {
      setEditId(addr.addressId);
      

      const res = await axios.get(
        `/getAddressById/${addr.addressId}/${userId}`,
        { headers: { Authorization: `Bearer ${token}` } }
      );

      const data = res.data.message;

      // ✅ Load states first
      const stateRes = await axios.get(`/getStateById/${data.countryId}`);
      const statesData = Array.isArray(stateRes.data.message)
        ? stateRes.data.message
        : [stateRes.data.message];

      setStates(statesData);

      // ✅ Load cities
      const cityRes = await axios.get(`/getCityById/${data.stateId}`);
      const citiesData = Array.isArray(cityRes.data.message)
        ? cityRes.data.message
        : [cityRes.data.message];

      setCities(citiesData);

      // ✅ NOW set addFormData AFTER dropdowns ready
      setEditFormData({
        countryId: data.countryId || "",
        stateId: data.stateId || "",
        cityId: data.cityId || "",
        addressLine1: data.addressLine1 || "",
        addressLine2: data.addressLine2 || "",
        landmark: data.landMark || "",
        pincode: data.pincode || "",
        saveAs: data.saveAs || "",
      });

      setShowPopup(true);
      setIsEditLoading(true);
      setIsEditLoading(false);

    } catch (err) {
      console.log(err);
      setIsEditLoading(false);
    }
  };

    const handleSubmitAfterUpdate = async(e) => {
    
      e.preventDefault();

      try
      {
        const token = localStorage.getItem("accessToken");
        const userId = getUserId();

        if(editId)
        {
          await axios.put(`/updateAddress/${editId}/${userId}`,{
            LandMark: addFormData.landmark,
            AddressLine1: addFormData.addressLine1,
            AddressLine2: addFormData.addressLine2,
            CountryId: addFormData.countryId,
            StateId: addFormData.stateId,
            CityId: addFormData.cityId,
            Pincode: addFormData.pincode,
            SaveAs: addFormData.saveAs
          },{
            headers: {Authorization: `Bearer ${token}`}
          });
          alert("Address Updated");
        }
        else
        {
          await axios.post("/addaddress", {
            LandMark: editFormData.landmark,
            AddressLine1: editFormData.addressLine1,
            AddressLine2: editFormData.addressLine2,
            CountryId: editFormData.countryId,
            StateId: editFormData.stateId,
            CityId: editFormData.cityId,
            Pincode: editFormData.pincode,
            SaveAs: editFormData.saveAs
          }, {
            headers: { Authorization: `Bearer ${token}` }
          });

          alert("Saved");
        }
        
        fetchAddresses();
        // RESET
        setEditId(null);
        setShowPopup(false);

        setAddFormData({
          countryId: "",
          stateId: "",
          cityId: "",
          addressLine1: "",
          addressLine2: "",
          landmark: "",
          pincode: "",
          saveAs: "",
        });

        fetchAddresses();
      }
      catch(error)
      {
        console.log(error);
      }
    };

    const handleProceedToPayment = () => {

      const selectedAddress = addresses.find(
        (addr) => addr.addressId === selectedAddressId
      );

      const totalAmount = cartItems.reduce(
        (sum, item) =>
          sum + item.quantity * (item.price || 0),
        0
      );

      console.log("Selected Address:", selectedAddress);
      console.log("Cart Items:", cartItems);
      console.log("Total Amount:", totalAmount);

      navigate("/Payments", {
        state: {
          address: selectedAddress,
          cart: cartItems,
          total: totalAmount,
        },
      });
    };


  return (
    <div className="address-page">

      {/* =======================================================
          MAIN LAYOUT
      ======================================================= */}
      <div className="checkout-layout">

        {/* =======================================================
            LEFT SIDE : ORDER SUMMARY
        ======================================================= */}
        <div className="address-left">

          <h2>Order Summary</h2>

          {cartItems.length === 0 ? (

            <p>No items</p>

          ) : (

            <table className="order-summary-table">

              {/* ================= HEADER ================= */}
              <thead>

                <tr>

                  <th>Image</th>
                  <th>Product</th>
                  <th>Price</th>
                  <th>Qty</th>
                  <th>Subtotal</th>

                </tr>

              </thead>

              {/* ================= BODY ================= */}
              <tbody>

                {cartItems.map((item) => (

                  <tr key={item.id}>

                    {/* IMAGE */}
                    <td>

                      <div className="table-product-image">

                        <img
                          src={
                            item.imageUrl
                              ? `${BASE_URL}${item.imageUrl}`
                              : "https://dummyimage.com/300x200/ccc/000&text=No+Image"
                          }
                          alt={item.productName}
                        />

                      </div>

                    </td>

                    {/* PRODUCT NAME */}
                    <td className="table-product-name">
                      {item.productName}
                    </td>

                    {/* PRICE */}
                    <td className="price-cell">
                      ₹ {item.price}
                    </td>

                    {/* QTY */}
                    <td className="qty-cell">
                      {item.quantity}
                    </td>

                    {/* SUBTOTAL */}
                    <td className="table-subtotal">
                      ₹ {item.subTotal}
                    </td>

                  </tr>

                ))}

                {/* ================= TOTAL ROW ================= */}
                <tr className="grand-total-row">

                  <td colSpan="4" className="grand-total-label">
                    Total Amount
                  </td>

                  <td className="grand-total-price">
                    ₹
                    {cartItems.reduce(
                      (total, item) =>
                        total + (item.quantity * (item.price || 0)),
                      0
                    )}
                  </td>

                </tr>

              </tbody>

            </table>

          )}

        </div>
        {/* =======================================================
            RIGHT SIDE : ADDRESS SECTION
        ======================================================= */}
        <div className="address-right-container">

          {/* =======================================================
              HEADER
          ======================================================= */}
          <div className="right-header">

            <h2>Select Addresses</h2>

            <button
              className="add-address-btn"
              onClick={() => setShowAddPopup(true)}
            >
              ➕ Add Address
            </button>

          </div>

          {/* =======================================================
              ADDRESS GRID
          ======================================================= */}
          <div className="right">

            {loading ? (

              <div className="loader-container">

                <div className="spinner"></div>

              </div>

            ) : addresses.length === 0 ? (

              <p className="empty-text">
                No addresses found
              </p>

            ) : (

              addresses.map((addr) => (

                <div
                  key={addr.addressId}
                  className="address-card-new"
                >

                  {/* ===== RADIO BUTTON ===== */}
                  <div className="radio-select">

                    <input
                      type="radio"
                      name="selectedAddress"
                      value={addr.addressId}
                      checked={
                        selectedAddressId ===
                        addr.addressId
                      }
                      onChange={() =>
                        setSelectedAddressId(
                          addr.addressId
                        )
                      }
                    />

                  </div>

                  {/* ===== ADDRESS TAG ===== */}
                  <div className="card-top">

                    <span className="address-tag">

                      {addr.saveAs || "Address"}

                    </span>

                  </div>

                  {/* ===== ADDRESS BODY ===== */}
                  <div className="card-body">

                    <p className="landmark">

                      📍 {addr.landMark},
                      {" "}
                      {addr.addressLine1},
                      {" "}
                      {addr.addressLine2},
                      {" "}
                      {addr.cityName},
                      {" "}
                      {addr.stateName},
                      {" "}
                      {addr.countryName}
                      {" "}
                      - {addr.pincode}

                    </p>

                  </div>

                  {/* ===== ACTION BUTTONS ===== */}
                  <div className="card-actions-new">

                    {/* EDIT */}
                    <button
                      onClick={() => handleEdit(addr)}
                      className="btn-edit"
                    >
                      ✏️ Edit
                    </button>

                    {/* DELETE */}
                    <button
                      onClick={() =>
                        handleDelete(addr.addressId)
                      }
                      className="btn-delete"
                    >
                      🗑 Delete
                    </button>

                  </div>

                </div>
              ))
            )}

          </div>

          {/* =======================================================
              PAYMENT BUTTON
          ======================================================= */}
          <div className="payment-btn-container">
            <button className="proceed-btn" disabled={!selectedAddressId} onClick={handleProceedToPayment} > 🚀 Proceed to Payment </button>
          </div>

        </div>
      </div>

      {/* =======================================================
          ADD ADDRESS POPUP
      ======================================================= */}
      {showAddPopup && (

        <div className="popup-overlay">

          <div className="popup-container">

            <h2>Add Address</h2>

            <form
              onSubmit={handleSubmit}
              className="popup-form"
            >

              {/* LANDMARK */}
              <input
                name="landmark"
                placeholder="Landmark"
                value={addFormData.landmark}
                onChange={handleAddChange}
                required
              />

              {/* ADDRESS LINE 1 */}
              <input
                name="addressLine1"
                placeholder="Address Line 1"
                value={addFormData.addressLine1}
                onChange={handleAddChange}
                required
              />

              {/* ADDRESS LINE 2 */}
              <input
                name="addressLine2"
                placeholder="Address Line 2"
                value={addFormData.addressLine2}
                onChange={handleAddChange}
                required
              />

              {/* COUNTRY */}
              <select
                name="countryId"
                value={addFormData.countryId}
                onChange={handleAddChange}
                required
              >

                <option value="">
                  Select Country
                </option>

                {countries.map((c) => (

                  <option
                    key={c.countryId}
                    value={c.countryId}
                  >
                    {c.countryName}
                  </option>

                ))}

              </select>

              {/* STATE */}
              <select
                name="stateId"
                value={addFormData.stateId}
                onChange={handleAddChange}
                required
              >

                <option value="">
                  Select State
                </option>

                {states.map((s) => (

                  <option
                    key={s.stateId}
                    value={s.stateId}
                  >
                    {s.stateName}
                  </option>

                ))}

              </select>

              {/* CITY */}
              <select
                name="cityId"
                value={addFormData.cityId}
                onChange={handleAddChange}
                required
              >

                <option value="">
                  Select City
                </option>

                {cities.map((c) => (

                  <option
                    key={c.cityId}
                    value={c.cityId}
                  >
                    {c.cityName}
                  </option>

                ))}

              </select>

              {/* PINCODE */}
              <input
                name="pincode"
                placeholder="Pincode"
                value={addFormData.pincode}
                onChange={handleAddChange}
                required
              />

              {/* SAVE AS */}
              <input
                name="saveAs"
                placeholder="Home / Work"
                value={addFormData.saveAs}
                onChange={handleAddChange}
                required
              />

              {/* BUTTONS */}
              <div className="popup-buttons">

                <button type="submit">
                  Save
                </button>

                <button
                  type="button"
                  onClick={() =>
                    setShowAddPopup(false)
                  }
                >
                  Cancel
                </button>

              </div>

            </form>

          </div>

        </div>
      )}

      {/* =======================================================
          EDIT ADDRESS POPUP
      ======================================================= */}
      {showPopup && (

        <div className="popup-overlay">

          <div className="popup-container">

            <h2>Edit Address</h2>

            <form
              onSubmit={handleSubmitAfterUpdate}
              className="popup-form"
            >

              <input
                name="landmark"
                value={editFormData.landmark}
                onChange={handleEditChange}
                required
              />

              <input
                name="addressLine1"
                value={editFormData.addressLine1}
                onChange={handleEditChange}
                required
              />

              <input
                name="addressLine2"
                value={editFormData.addressLine2}
                onChange={handleEditChange}
                required
              />

              <input
                name="pincode"
                value={editFormData.pincode}
                onChange={handleEditChange}
                required
              />

              <input
                name="saveAs"
                value={editFormData.saveAs}
                onChange={handleEditChange}
              />

              {/* BUTTONS */}
              <div className="popup-buttons">

                <button type="submit">
                  Update
                </button>

                <button
                  type="button"
                  onClick={() =>
                    setShowPopup(false)
                  }
                >
                  Cancel
                </button>

              </div>

            </form>

          </div>

        </div>
      )}

    </div>
  );
};

export default Address;