import { ITimeOrSeasonModel } from './time-or-season.model';

export interface ISeasonModel extends ITimeOrSeasonModel { }

export class SeasonModel implements ISeasonModel {
    sortIndex = 0;
    abbreviation = '';
    name = '';
    color = '';
}