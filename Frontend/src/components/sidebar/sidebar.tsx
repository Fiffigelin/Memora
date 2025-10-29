import { useEffect, useState } from "react";
import { FiLogOut, FiMenu } from "react-icons/fi";
import { TiChevronRight } from "react-icons/ti";
import { SidebarItem, SidebarItemProps } from "./sidebar-item";

import "./sidebar.scss";

export type NavItem = {
  title: string;
  sidebarItems?: SidebarItemProps[];
};

export default function Sidebar({ title, sidebarItems }: NavItem) {
  const [collapsed, setCollapsed] = useState<boolean>(window.innerWidth < 768);

  useEffect(() => {
    const handleResize = () => {
      setCollapsed(window.innerWidth < 768);
    };

    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, []);

  const handleToggle = () => {
    setCollapsed((prev) => !prev);
  };

  const handleLogout = () => {
    console.log("Logging out...");
  };

  return (
    <aside className={`sidebar ${collapsed ? "collapsed" : ""}`}>
      <div className="logo-content">
        {!collapsed && (
          <div className="logo">
            <TiChevronRight />
            <span className="logo-name">{title}</span>
          </div>
        )}
        <button id="btn" aria-label="Toggle menu" className="toggle-btn" onClick={handleToggle}>
          <FiMenu />
        </button>
      </div>

      <div className={`sidebar-content ${collapsed ? "collapsed" : ""}`}>
        <ul className="nav-list">
          {sidebarItems?.map(({ label, icon, to }, index) => (
            <SidebarItem key={label || index} label={label} icon={icon} to={to} />
          ))}
        </ul>

        <div className="logout" onClick={handleLogout}>
          <SidebarItem key={"logout"} label={"Log out"} icon={<FiLogOut />} to={""} />
        </div>
      </div>
    </aside>
  );
}
