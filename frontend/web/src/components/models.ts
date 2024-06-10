export interface DoItem {
  id?: string | null;
  title?: string;
  desc?: string | null;
  dateCreated?: string | null;
  dateUpdated?: string | null;
  points?: number;
  isFinished?: boolean | null;
  profileId?: string | null;
  dateFinished?: string | null;
  image?: string | null;
  images?: string[] | null;
  tags?: string[] | null;
  schedule?: string;
  historyBonus?: number | null;
}

export interface CarrotItem {
  id?: string | null;
  title?: string | null;
  desc?: string | null;
  dateCreated?: string | null;
  dateUpdated?: string | null;
  points?: number | null;
  profileId?: string | null;
  image?: string | null;
  tags?: string[] | null;
  history?: [] | null;
}

export interface Profile {
  id: string;
  login: string;
  scoreTotal: number;
  scoreWeek: number;
  doItemsIds: string[];
  doItems: DoItem[];
  dateLastConnection: string;
  dateCreated: string;
}
