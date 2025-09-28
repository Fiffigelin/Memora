import { NavLink } from "react-router-dom";
import "./sidebar.scss";

export type SidebarItemProps = {
  label: string;
  icon: React.ReactNode;
  to: string;
};

export function SidebarItem({ label, icon, to }: SidebarItemProps) {
  return (
    <li>
      <NavLink to={to} className={({ isActive }) => `nav-link ${isActive ? "active" : ""}`}>
        {icon}
        <span className="links-name">{label}</span>
      </NavLink>
    </li>
  );
}
