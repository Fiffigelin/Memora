import { useOutletContext } from "react-router-dom";
import { UserProfileDto } from "../../../api/client";
import CommonButton from "../../../components/common-button/common-button";
import VocabCard from "../../../components/vocab-card/vocab-card";
import LazyList from "../../../components/lazy-list/lazy-list";

import "./vocabulary-home-page.scss";
import { useState } from "react";

type DashboardContext = { user: UserProfileDto };

export default function VocabularyHome() {
  const { user } = useOutletContext<DashboardContext>();
  const [vocabLists, setList] = useState(user.vocabularyLists || []);

  // TODO: Byt ut mot den riktiga listan

  // const testLists = Array(40)
  //   .fill(null)
  //   .flatMap(() => user?.vocabularyLists || []);

  const handleDeleteClick = (id: string) => {
    setList((prev) => prev.filter((list) => list.id !== id));
  };

  console.log(vocabLists[0]);

  return (
    <div className="vocabulary-home-container">
      <div className="vocabulary-header">
        <h1>Glosor</h1>
        <VocabCard vocabList={vocabLists[0]} onDelete={handleDeleteClick} />
        <CommonButton
          title="Skapa ny gloslista"
          variant="default"
          onClick={() => console.log("Skapa ny lista")}
        />
      </div>

      <LazyList
        list={vocabLists}
        increment={20}
        renderItem={(item) => <VocabCard vocabList={item} onDelete={handleDeleteClick} />}
      />
    </div>
  );
}
