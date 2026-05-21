import React, { useEffect, useState, useCallback } from "react";
import axios from "../../axiosInstance";
import { useNavigate } from "react-router-dom";
import "./Dashboard.css";

const Dashboard = () => {

  const [categories, setCategories] = useState([]);
  const [subCategories, setSubCategories] = useState([]);
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(true);
  const BASE_URL = "http://52.200.252.181:8000";
  
  const getUser = () =>
  {
    const accessToken = localStorage.getItem("accessToken");

    // ✅ Make token OPTIONAL
    return accessToken
      ? { headers: { Authorization: `Bearer ${accessToken}` } }
      : {};
  }

  const categoryFetchData = useCallback(async () =>
  {
    try {
    setIsLoading(true);

    const userAccess = getUser();

    const catRes = await axios.get("http://52.200.252.181:8000/api/getallcategory", userAccess)
  
    setCategories(catRes.data);
    }
    catch (error) 
    {
      console.error("API Error:", error.response?.data || error.message);
    } 
    finally
    {
      setIsLoading(false);
    }
  } ,[]);

  const subcategoryFetchData = useCallback(async () =>
  {
    try {
      setIsLoading(true);

      const userAccess = getUser();
      
      const subRes = await axios.get("http://52.200.252.181:8000/api/getallsubcategory", userAccess)
      
      setSubCategories(subRes.data);
    }
    catch (error) 
    {
      console.error("API Error:", error.response?.data || error.message);
    } 
    finally
    {
      setIsLoading(false);
    }
  },[]);

  useEffect(() => {
    categoryFetchData();
    subcategoryFetchData();
  }, []);

  // ✅ Get subcategories for a category
  const getSubCategories = (categoryId) => {
    return subCategories
      .filter((sub) => sub.categoryId === categoryId)
      .slice(0, 4); // show max 4
  };

  // ✅ Handle subcategory click
  const handleSubClick = (subId) => {
    navigate("/Products", {
      state: { subCategoryId: subId },
    });
  };

  // ✅ Loading state
  if (isLoading) {
  return (
    <div className="loading-container">
      <div className="ring-loader"></div>
    </div>
  );
}

  return (
    <div className="category-container">
      {categories.map((cat) => {
        const subList = getSubCategories(cat.categoryId);

        return (
          <div key={cat.categoryId} className="category-card">
            
            {/* Category Title */}
            <h2 className="category-title">
              {cat.categoryName} | Top picks
            </h2>

            {/* 🔥 Subcategory Grid */}
            <div className="subcategory-grid">
              {subList.map((sub) => (
                <div key={sub.subCategoryId} className="subcategory-item" onClick={() => handleSubClick(sub.subCategoryId)}>
                  <img src={ sub.imageUrl ? `${BASE_URL}${sub.imageUrl}` : "https://dummyimage.com/300x200/ccc/000&text=No+Image" }
                    alt={sub.subCategoryName}
                  />
                  <span>{sub.subCategoryName}</span>
                </div>
              ))}
            </div>

            {/* Footer */}
            <span
              className="see-more"
              onClick={() => navigate(`/category/${cat.categoryId}`)}
            >
              See more
            </span>
          </div>
        );
      })}
    </div>
  );
};

export default Dashboard;