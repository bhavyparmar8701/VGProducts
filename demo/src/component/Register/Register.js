import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import './Register.css';

const Register = () => {
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    phoneNumber: "",
    email: "",
    password: "",
  });

  const [errors, setErrors] = useState({});
  const [touched, setTouched] = useState({});
  const [isLoading, setIsLoading] = useState(false);

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
    // Clear specific error when user starts typing
    if (errors[e.target.name]) {
      setErrors({
        ...errors,
        [e.target.name]: null
      });
    }
  };

  const handleBlur = (e) => {
    setTouched({
      ...touched,
      [e.target.name]: true
    });
  };

  const validate = () => {
    let newErrors = {};

    if (!formData.firstName.trim()) {
      newErrors.firstName = "First Name is required";
    }
    if (!formData.lastName.trim()) {
      newErrors.lastName = "Last Name is required";
    }
    if (!formData.phoneNumber.trim()) {
      newErrors.phoneNumber = "Phone Number is required";
    } else if (!/^[0-9]{10}$/.test(formData.phoneNumber)) {
      newErrors.phoneNumber = "Enter a valid 10-digit phone number";
    }
    if (!formData.email.trim()) {
      newErrors.email = "Email is required";
    } else if (!/^\S+@\S+\.\S+$/.test(formData.email)) {
      newErrors.email = "Invalid email format";
    }
    if (!formData.password.trim()) {
      newErrors.password = "Password is required";
    } else if (formData.password.length < 6) {
      newErrors.password = "Password must be at least 6 characters";
    }

    return newErrors;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const validationErrors = validate();
    setErrors(validationErrors);

    // Mark all fields as touched
    setTouched({
      firstName: true,
      lastName: true,
      phoneNumber: true,
      email: true,
      password: true
    });

    if (Object.keys(validationErrors).length === 0) {
      setIsLoading(true);
      try {
        const response = await axios.post("http://52.200.252.181:8000/api/register", {
          FirstName: formData.firstName,
          LastName: formData.lastName,
          PhoneNumber: formData.phoneNumber,
          Email: formData.email,
          Password: formData.password
        });
        
        alert(response.data.message);
        navigate("/login");
      } catch (error) {
        console.log("BACKEND:", error.response?.data);
        if (error.response?.data?.message) {
          alert(error.response.data.message);
        } else {
          alert("Something went wrong!");
        }
      } finally {
        setIsLoading(false);
      }
    }
  };

  return (
    <div className="register-container">
      <div className="register-card">
        <div className="register-header">
          <h2>Create Account</h2>
          <p>Join ShopWave to get the best deals.</p>
        </div>

        <form className="register-form" onSubmit={handleSubmit}>
          
          <div className="form-row">
            <div className="input-group">
              <label>First Name</label>
              <input
                type="text"
                name="firstName"
                placeholder="First Name"
                value={formData.firstName}
                onChange={handleChange}
                onBlur={handleBlur}
                className={touched.firstName && errors.firstName ? "input-error" : ""}
              />
              {touched.firstName && errors.firstName && (
                <span className="error-text">{errors.firstName}</span>
              )}
            </div>

            <div className="input-group">
              <label>Last Name</label>
              <input
                type="text"
                name="lastName"
                placeholder="Last Name"
                value={formData.lastName}
                onChange={handleChange}
                onBlur={handleBlur}
                className={touched.lastName && errors.lastName ? "input-error" : ""}
              />
              {touched.lastName && errors.lastName && (
                <span className="error-text">{errors.lastName}</span>
              )}
            </div>
          </div>

          <div className="input-group">
            <label>Phone Number</label>
            <input
              type="tel"
              name="phoneNumber"
              placeholder="Enter Phone Number"
              value={formData.phoneNumber}
              onChange={handleChange}
              onBlur={handleBlur}
              className={touched.phoneNumber && errors.phoneNumber ? "input-error" : ""}
            />
            {touched.phoneNumber && errors.phoneNumber && (
              <span className="error-text">{errors.phoneNumber}</span>
            )}
          </div>

          <div className="input-group">
            <label>Email</label>
            <input
              type="email"
              name="email"
              placeholder="Enter Email"
              value={formData.email}
              onChange={handleChange}
              onBlur={handleBlur}
              className={touched.email && errors.email ? "input-error" : ""}
            />
            {touched.email && errors.email && (
              <span className="error-text">{errors.email}</span>
            )}
          </div>

          <div className="input-group">
            <label>Password</label>
            <input
              type="password"
              name="password"
              placeholder="Create a Password"
              value={formData.password}
              onChange={handleChange}
              onBlur={handleBlur}
              className={touched.password && errors.password ? "input-error" : ""}
            />
            {touched.password && errors.password && (
              <span className="error-text">{errors.password}</span>
            )}
          </div>

          <button type="submit" className="register-button" disabled={isLoading}>
            {isLoading ? "Registering..." : "Register"}
          </button>

          <div className="form-footer">
            <span>
              Already have an account? <Link to="/login">Login</Link>
            </span>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Register;