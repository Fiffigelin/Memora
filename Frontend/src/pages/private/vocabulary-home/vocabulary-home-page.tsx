import CommonButton from "../../../components/common-button/common-button";
import VocabCard from "../../../components/vocab-card/vocab-card";

import "./vocabulary-home-page.scss";
import { useOutletContext } from "react-router-dom";
import { UserProfileDto } from "../../../api/client";

type DashboardContext = { user: UserProfileDto };

export default function VocabularyHome() {
  const { user } = useOutletContext<DashboardContext>();

  const list1 = user?.vocabularyLists || [];
  const list2 = user?.vocabularyLists || [];
  const list3 = user?.vocabularyLists || [];

  const testLists = [...list1, ...list2, ...list3];

  return (
    <div className="vocabulary-home-container">
      <div className="vocabulary-header">
        <h1>Glosor</h1>
        <CommonButton
          title="Skapa ny gloslista"
          variant="default"
          onClick={() => {
            console.log("Skapa ny lista");
          }}
        />
      </div>
      <div className="vocabulary-content">
        <div className="vocabulary-list">
          {/* {user?.vocabularyLists?.map((item) => ( */}
          {testLists.map((item) => (
            <VocabCard
              key={item.id}
              id={item.id!}
              createdAt={item.createdAt}
              language={item.language!}
              title={item.title!}
              amountOfWords={item.vocabularies ? item.vocabularies.length : 0}
            />
          ))}
        </div>
      </div>
    </div>
  );
}
