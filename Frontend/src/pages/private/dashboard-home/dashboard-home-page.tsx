import { useAuthContext } from "../../../context/auth-context";
import "./dashboard-home-page.scss";

export default function DashboardHome() {
  const { user } = useAuthContext();

  return (
    <div className="dashboard">
      <p>Dashboard!</p>
      <p>Welcome, {user?.user?.username}!</p>
    </div>
  );
}
