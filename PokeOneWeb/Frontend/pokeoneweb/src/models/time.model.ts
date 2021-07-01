import { ITimeOrSeasonModel } from './time-or-season.model';

export interface ITimeModel extends ITimeOrSeasonModel {
    times: string;
}

export class TimeModel implements ITimeModel {
    sortIndex = 0;
    abbreviation = '';
    name = '';
    color = '';
    times = '';
}