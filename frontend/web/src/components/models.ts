export interface Carotte {
  id?: string | null;
  title?: string;
  desc?: string | null;
  dateCreated?: string | null;
  dateUpdated?: string | null;
  points?: number;
  isReward?: boolean | null;
  profileId?: string | null;
  dateFinished?: string | null;
  image?: string | null;
  tags?: string[] | null;
  history?: string[] | null;
  historyBonus?: number | null;
}

export interface Profile {
  id: string;
  login: string;
  scoreTotal: number;
  scoreWeek: number;
  carottesIds: string[];
  carottes: Carotte[];
  dateLastConnection: string;
  dateCreated: string;
}
