import { Rank } from './rank.enum';

export interface NamiMember {
  id: number;
  firstname: string;
  lastname: string;
  rank: Rank;
}
