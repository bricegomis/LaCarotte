import { Carotte } from './Carotte';


export interface Profile {
  id: string;
  login: string;
  scoreTotal: number;
  scoreWeek: number;
  scoreDay: number;
  carottesIds: string[];
  carottes: Carotte[];
  dateLastConnection: string;
  dateCreated: string;
}
