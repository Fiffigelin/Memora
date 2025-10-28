import { Outlet } from "react-router-dom";
import Sidebar from "../../components/sidebar/sidebar";
import { SidebarItemProps } from "../../components/sidebar/sidebar-item";
import { IoGridOutline } from "react-icons/io5";
import { TbVocabulary } from "react-icons/tb";
// import { BiMessageDetail } from "react-icons/bi";

import "./private-layout.scss";
import { useAuthContext } from "../../context/auth-context";

export default function PrivateLayout() {
  const { user } = useAuthContext();
  console.log(user);

  if (!user?.user) {
    return <p>Loading...</p>;
  }

  const navItems: SidebarItemProps[] = [
    { label: "Dashboard", icon: <IoGridOutline />, to: "/dashboard" },
    { label: "Vocabularies", icon: <TbVocabulary />, to: "/vocabulary" },
  ];
  return (
    <div className="private-container">
      <Sidebar title="" sidebarItems={navItems} />
      <section className="content">
        <Outlet context={{ user: user.user }} />
      </section>
    </div>
  );
}
