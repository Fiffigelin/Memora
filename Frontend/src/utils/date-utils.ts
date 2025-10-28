export type DateFormat = "DAY_MONTH_YEAR" | "DAY_MONTH_YEAR_TIME";

export function formatDate(date: string | Date, format: DateFormat = "DAY_MONTH_YEAR"): string {
  if (!date) return "";

  const d = date instanceof Date ? date : new Date(date);

  switch (format) {
    case "DAY_MONTH_YEAR":
      return d.toLocaleDateString("sv-SE", {
        day: "numeric",
        month: "long",
        year: "numeric",
      });
    case "DAY_MONTH_YEAR_TIME":
      return d.toLocaleString("sv-SE", {
        day: "numeric",
        month: "long",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit",
      });
  }
}
