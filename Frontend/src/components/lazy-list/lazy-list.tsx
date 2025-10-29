import React, { useEffect, useRef, useState } from "react";

import "./lazy-list.scss";

type ListItemProps<T> = {
  list: T[];
  increment: number;
  renderItem: (item: T, index: number) => React.ReactNode;
};
export default function LazyList<T>({ list, increment, renderItem }: ListItemProps<T>) {
  const [loading, setLoading] = useState(false);
  const [visibleCount, setVisibleCount] = useState(increment);
  const contentRef = useRef<HTMLDivElement>(null);
  const lastItemRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    if (!lastItemRef.current || visibleCount >= list.length) return;

    const observer = new IntersectionObserver(
      (entries) => {
        if (entries[0].isIntersecting && visibleCount < list.length) {
          setLoading(true);
          setTimeout(() => {
            setVisibleCount((prev) => Math.min(prev + increment, list.length));
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
  }, [visibleCount, list.length, increment]);

  return (
    <div className="lazy-list-container" ref={contentRef}>
      <div className="lazy-list-grid">
        {list.slice(0, visibleCount).map((item, index) => {
          const isLastItem = index === visibleCount - 1;
          return (
            <div className="item-wrapper" key={index} ref={isLastItem ? lastItemRef : null}>
              {renderItem(item, index)}
            </div>
          );
        })}
      </div>
      {/* Snygga till laddaren */}
      {loading && <div className="loading-indicator">Laddar...</div>}
    </div>
  );
}
