import { useLocation } from "react-router-dom";

const PaymentSuccessPage = () => {
  const location = useLocation();
  const params = new URLSearchParams(location.search);
  const status = params.get("status");

  return (
    <div className="container mt-5 text-center">
      <h2 className="text-success">🎉 Payment Successful</h2>
      <p>Thank you for your donation! Status: <strong>{status ?? "paid"}</strong></p>
    </div>
  );
};

export default PaymentSuccessPage;
