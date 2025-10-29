import { useEffect, useRef, useState } from "react";
import { useOutletContext } from "react-router-dom";
import { UserProfileDto } from "../../../api/client";
import CommonButton from "../../../components/common-button/common-button";
import VocabCard from "../../../components/vocab-card/vocab-card";

import "./vocabulary-home-page.scss";

type DashboardContext = { user: UserProfileDto };
const LAZY_INCREMENT = 20;

export default function VocabularyHome() {
  const { user } = useOutletContext<DashboardContext>();
  const [visibleCount, setVisibleCount] = useState(LAZY_INCREMENT);
  const [loading, setLoading] = useState(false);

  const testLists = Array(40)
    .fill(null)
    .flatMap(() => user?.vocabularyLists || []);

  const contentRef = useRef<HTMLDivElement>(null);
  const lastItemRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    if (!lastItemRef.current || visibleCount >= testLists.length) return;

    const observer = new IntersectionObserver(
      (entries) => {
        if (entries[0].isIntersecting && visibleCount < testLists.length) {
          setLoading(true);
          setTimeout(() => {
            setVisibleCount((prev) => Math.min(prev + LAZY_INCREMENT, testLists.length));
            setLoading(false);
          }, 200);
        }
      },
      {
        root: contentRef.current,
        threshold: 0.75,
      }
    );

    observer.observe(lastItemRef.current);

    return () => observer.disconnect();
  }, [visibleCount, testLists.length]);

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

      <div className="vocabulary-content" ref={contentRef}>
        <div className="vocabulary-list">
          {testLists.slice(0, visibleCount).map((item, index) => {
            const isLastItem = index === visibleCount - 1;
            return (
              <div className="item-wrapper" key={index} ref={isLastItem ? lastItemRef : null}>
                <VocabCard
                  key={item.id}
                  id={item.id!}
                  createdAt={item.createdAt}
                  language={item.language!}
                  title={item.title!}
                  amountOfWords={item.vocabularies ? item.vocabularies.length : 0}
                />
              </div>
            );
          })}
        </div>
        {/* Ändra loading-indikatorn till något snyggare */}
        {loading && <div className="loading-indicator">Laddar...</div>}
      </div>
    </div>
  );
}
