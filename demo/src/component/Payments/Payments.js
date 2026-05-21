import './Payments.css';
import { useLocation } from "react-router-dom";

const Payments = () => {
  const location = useLocation();

  const { address, cart, total } = location.state || {};
   console.log("Address:", address);
  console.log("Cart:", cart);
  console.log("Total:", total);


  return (
    <div>
      <h2>Payment Page</h2>

      <h3>Selected Address</h3>
      <p>{address?.fullName}</p>
      <p>{address?.mobileNumber}</p>
      <p>{address?.addressLine}</p>

      <h3>Total Amount: ₹{total}</h3>
    </div>
  );
};


export default Payments;
