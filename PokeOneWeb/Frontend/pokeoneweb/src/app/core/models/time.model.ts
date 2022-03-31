export interface ITimeOfDayModel {
    sortIndex: number;
    abbreviation: string;
    name: string;
    color: string;
    times: string;
}

export class TimeOfDayModel implements ITimeOfDayModel {
    sortIndex = 0;
    abbreviation = '';
    name = '';
    color = '';
    times = '';
}
