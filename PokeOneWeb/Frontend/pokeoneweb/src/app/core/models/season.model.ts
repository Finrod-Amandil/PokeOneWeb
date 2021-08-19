export interface ISeasonModel { 
    sortIndex: number;
    abbreviation: string;
    name: string;
    color: string;
}

export class SeasonModel implements ISeasonModel {
    sortIndex = 0;
    abbreviation = '';
    name = '';
    color = '';
}