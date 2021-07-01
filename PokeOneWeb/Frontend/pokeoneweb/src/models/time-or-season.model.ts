export interface ITimeOrSeasonModel {
    sortIndex: number;
    abbreviation: string;
    name: string;
    color: string;
}

export class TimeOrSeasonModel implements ITimeOrSeasonModel {
    sortIndex = 0;
    abbreviation = '';
    name = '';
    color = '';
}