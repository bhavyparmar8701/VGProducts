import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import './ForgotPassword.css';

const ForgotPassword = () => {
  const navigate = useNavigate();
  
  const [formData, setFormData] = useState({
    email: "",
  });

  const [errors, setErrors] = useState({});
  const [touched, setTouched] = useState({});
  const [isLoading, setIsLoading] = useState(false);

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
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
    if (!formData.email.trim()) {
      newErrors.email = "Email Address is required";
    } else if (!/^\S+@\S+\.\S+$/.test(formData.email)) {
      newErrors.email = "Invalid email format";
    }
    return newErrors;
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    
    const validationErrors = validate();
    setErrors(validationErrors);
    setTouched({ email: true });

    if (Object.keys(validationErrors).length === 0) {
      setIsLoading(true);
      
      // Simulating an API call
      setTimeout(() => {
        alert("Reset link sent successfully to your email!");
        setIsLoading(false);
        navigate("/login");
      }, 800);
    }
  };
    
  return (
    <div className="forgot-password-container">
      <div className="forgot-password-card">
        <div className="forgot-password-header">
          <h2>Reset Password</h2>
          <p>Enter your email and we will send you a reset link.</p>
        </div>

        <form className="forgot-password-form" onSubmit={handleSubmit}>
          <div className="input-group">
            <label>Email Address</label>
            <input
              type="email"
              name="email"
              placeholder="Enter Your Email"
              value={formData.email}
              onChange={handleChange}
              onBlur={handleBlur}
              className={touched.email && errors.email ? "input-error" : ""}
            />
            {touched.email && errors.email && (
              <span className="error-text">{errors.email}</span>
            )}
          </div>

          <button type="submit" className="forgot-password-button" disabled={isLoading}>
            {isLoading ? "Sending..." : "Send Reset Link"}
          </button>

          <div className="form-footer">
            <Link to="/login" className="back-to-login">Back to Login</Link>
          </div>
        </form>
      </div>
    </div>
  );
};

export default ForgotPassword;