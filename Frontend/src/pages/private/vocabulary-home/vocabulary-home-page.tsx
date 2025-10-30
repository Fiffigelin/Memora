import CommonButton from "../../../components/common-button/common-button";
import VocabCard from "../../../components/vocab-card/vocab-card";
import LazyList from "../../../components/lazy-list/lazy-list";

import "./vocabulary-home-page.scss";
import { useCallback, useState } from "react";
import { useVocabulary } from "../hooks/use-vocabulary";

export default function VocabularyHome() {
  const { lists, deleteList, refetch } = useVocabulary();
  const [initialized, setInitialized] = useState(false);

  // Kör refetch **bara en gång** när komponenten första gången mountas
  if (!initialized) {
    refetch();
    setInitialized(true);
    console.log(lists);
  }

  const handleDeleteClick = useCallback(
    (id: string) => {
      if (!id) return;
      deleteList(id);
    },
    [deleteList]
  );

  return (
    <div className="vocabulary-home-container">
      <div className="vocabulary-header">
        <h1>Glosor</h1>
        <CommonButton
          title="Skapa ny gloslista"
          variant="default"
          onClick={() => console.log("Skapa ny lista")}
        />
      </div>

      <LazyList
        list={lists}
        increment={20}
        renderItem={(item) => <VocabCard vocabList={item} onDelete={handleDeleteClick} />}
      />
    </div>
  );
}
