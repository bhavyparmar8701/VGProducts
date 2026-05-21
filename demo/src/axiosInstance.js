import axios from "axios";

const axiosInstance = axios.create({
  baseURL: "http://52.200.252.181:8000/api",
});

//  REQUEST INTERCEPTOR (add token)
axiosInstance.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("accessToken");

    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
  },
  (error) => Promise.reject(error)
);

// RESPONSE INTERCEPTOR (handle 401)
axiosInstance.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      // remove token
      localStorage.removeItem("accessToken");

      // notify header
      window.dispatchEvent(new Event("authChange"));

      // redirect
      window.location.href = "/login";
    }

    return Promise.reject(error);
  }
);

export default axiosInstance;