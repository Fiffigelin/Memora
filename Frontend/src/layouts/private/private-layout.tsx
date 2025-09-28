import { Outlet } from "react-router-dom";
import Sidebar from "../../components/sidebar/sidebar";
import { SidebarItemProps } from "../../components/sidebar/sidebar-item";
import { IoGridOutline } from "react-icons/io5";
import { TbVocabulary } from "react-icons/tb";
// import { BiMessageDetail } from "react-icons/bi";

import "./private-layout.scss";

export default function PrivateLayout() {
  const navItems: SidebarItemProps[] = [
    { label: "Dashboard", icon: <IoGridOutline />, to: "/dashboard" },
    { label: "Vocabularies", icon: <TbVocabulary />, to: "/vocabulary" },
  ];
  return (
    <div className="private-container">
      <Sidebar title="" sidebarItems={navItems} />
      <section className="content">
        <Outlet />
      </section>
    </div>
  );
}
