import { useOutletContext } from "react-router-dom";
import { UserProfileDto } from "../../../api/client";

import "./dashboard-home-page.scss";

type DashboardContext = { user: UserProfileDto };

export default function DashboardHome() {
  const { user } = useOutletContext<DashboardContext>();
  console.log(user);

  return (
    <div className="dashboard">
      <p>Dashboard!</p>
      <p>Welcome, {user!.username}!</p>
      {user?.vocabularyLists?.map((list) => {
        return (
          <div>
            <p>{list.title}</p>
            <p>{list.language}</p>
            {list.vocabularies?.map((vocab) => {
              return (
                <div>
                  <p>
                    {vocab.word} - {vocab.translation}
                  </p>
                </div>
              );
            })}
          </div>
        );
      })}
    </div>
  );
}
