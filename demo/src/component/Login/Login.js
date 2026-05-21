import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import axios from "../../axiosInstance";
import "./Login.css";

const Login = () => {
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    email: "",
    password: "",
  });

  const [errors, setError] = useState({});
  const [touched, setTouched] = useState({});
  const [isLoading, setIsLoading] = useState(false);
  const [showPassword, setShowPassword] = useState(false);

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

  // Input Change
  const handleInputChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });

    if (errors[e.target.name]) {
      setError({
        ...errors,
        [e.target.name]: null,
      });
    }
  };

  // Blur
  const handleBlur = (e) => {
    setTouched({
      ...touched,
      [e.target.name]: true,
    });
  };

  // Validation
  const validate = () => {
    let newError = {};

    if (!formData.email) {
      newError.email = "Email is required";
    }

    if (!formData.password) {
      newError.password = "Password is required";
    }

    return newError;
  };

  // Submit
  const handleSubmit = async (e) => {
    e.preventDefault();

    const validationErrors = validate();
    setError(validationErrors);
    setTouched({ email: true, password: true });

    if (Object.keys(validationErrors).length === 0) 
    {
      setIsLoading(true);

      try 
      {
        const response = await axios.post("http://52.200.252.181:8000/api/login",
          {
            Email: formData.email,
            Password: formData.password,
          }
        );

        const accessToken = response.data.accessToken ; 

        if (!accessToken ) 
        {
          console.error("Token Missing : ", response.data);
          alert("Login failed: Token not received");
          return;
        }
        localStorage.setItem("accessToken", accessToken);
        window.dispatchEvent(new Event("authChange"));
        navigate("/dashboard");

      } 
      catch (error) 
      {
        console.error("Login Error:", error);

        if (error.response?.data?.message) 
        {
          alert(error.response.data.message);
        } 
        else 
        {
          alert("Server error. Please try again.");
        }
      } 
      finally 
      {
        setIsLoading(false);
      }
    }
  };

  return (
    <div className="login-container">
      <div className="login-card">
        <div className="login-header">
          <h2>Welcome Back</h2>
          <p>Please enter your details to sign in.</p>
        </div>

        <form className="login-form" onSubmit={handleSubmit}>
          
          {/* Email */}
          <div className="input-group">
            <label>Email</label>
            <input
              type="email"
              name="email"
              placeholder="Enter Email"
              value={formData.email}
              onChange={handleInputChange}
              onBlur={handleBlur}
              className={
                touched.email && errors.email ? "input-error" : ""
              }
            />
            {touched.email && errors.email && (
              <span className="error-text">{errors.email}</span>
            )}
          </div>

          {/* Password */}
          <div className="input-group">
            <div className="password-wrapper">
              <input
                type={showPassword ? "text" : "password"}
                name="password"
                placeholder="Enter Password"
                value={formData.password}
                onChange={handleInputChange}
                onBlur={handleBlur}
                className={
                  touched.password && errors.password ? "input-error" : ""
                }
              />

              <span
                className="eye-icon"
                onClick={() => setShowPassword(!showPassword)}
              >
                {showPassword ? <EyeOpen /> : < EyeClose/>}
              </span>
            </div>

            {touched.password && errors.password && (
              <span className="error-text">{errors.password}</span>
            )}
          </div>

          {/* Forgot */}
          <div className="form-actions">
            <Link to="/forgotpassword" className="forgot-password">
              Forgot Password?
            </Link>
          </div>

          {/* Button */}
          <button
            type="submit"
            className="login-button"
            disabled={isLoading}
          >
            {isLoading ? "Signing In..." : "Sign In"}
          </button>

          {/* Footer */}
          <div className="form-footer">
            <span>
              Don't have an account?{" "}
              <Link to="/register">Register</Link>
            </span>
          </div>

        </form>
      </div>
    </div>
  );
};

export default Login;