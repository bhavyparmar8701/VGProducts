import { BrowserRouter, Routes, Route } from "react-router-dom";
import Header from "./component/Header/Header";
import Dashboard from "./component/Dashboard/Dashboard";
import Login from "./component/Login/Login";
import Register from "./component/Register/Register";
import ForgotPassword from "./component/ForgotPassword/ForgotPassword";
import Footer from "./component/Footer/Footer";
import Electronics from "./component/Electronics/Electronics";
import Favourites from "./component/Favourites/Favourites";
import Products from "./component/Products/Products";
import Address from "./component/Address/Address";
import CartItems from "./component/CartItems/CartItems";
import Payments from "./component/Payments/Payments";

function App() {
  return (
    <BrowserRouter>
      <Header />

      <Routes>
        <Route path="/" element={<Dashboard />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/forgotpassword" element={<ForgotPassword />} />
        <Route path="/Electronics" element={<Electronics />} />
        <Route path="/favourites" element={<Favourites />} />
        <Route path="/Products" element={<Products/>}/>
        <Route path="/Address" element={<Address />} />
        <Route path="/CartItems" element={<CartItems />} />
        <Route path="/Payments" element={<Payments />} />
      </Routes>

      <Footer />
    </BrowserRouter>
  );
}

export default App;