import CommonButton from "../../../../components/common-button/common-button";
import VocabCard from "../../../../components/vocab-card/vocab-card";
import LazyList from "../../../../components/lazy-list/lazy-list";

import "./vocabulary-home-page.scss";
import { useCallback, useState } from "react";
import { useVocabulary } from "../../hooks/use-vocabulary";
import { useNavigate } from "react-router-dom";

export default function VocabularyHome() {
  const { lists, fetchListById, deleteList, refetch } = useVocabulary();
  const [initialized, setInitialized] = useState(false);
  const navigate = useNavigate();

  // Kör refetch **bara en gång** när komponenten första gången mountas
  if (!initialized) {
    refetch();
    setInitialized(true);
  }

  const copies = Array.from({ length: 3 }).flatMap(() => lists);

  const handleCreate = useCallback(() => {
    navigate("/vocabulary/new");
  }, [navigate]);

  const handleUpdate = useCallback(
    async (listid: string) => {
      const list = await fetchListById(listid);
      navigate("/vocabulary/" + listid, { state: { list } });
    },
    [fetchListById, navigate]
  );

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
        <CommonButton title="Skapa ny gloslista" variant="default" onClick={handleCreate} />
      </div>

      <LazyList
        list={copies}
        increment={20}
        renderItem={(item) => (
          <VocabCard vocabList={item} onUpdate={handleUpdate} onDelete={handleDeleteClick} />
        )}
      />
    </div>
  );
}
