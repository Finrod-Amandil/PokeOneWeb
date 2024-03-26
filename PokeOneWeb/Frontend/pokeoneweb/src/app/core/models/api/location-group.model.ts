import { LocationModel } from './location.model';

export interface ILocationGroupListModel {
    name: string;
    resourceName: string;

    sortIndex: number;
    regionResourceName: string;
    regionName: string;
}

export interface ILocationGroupModel extends ILocationGroupListModel {
    isEventRegion: boolean;
    eventName: string;
    eventStartDate: string;
    eventEndDate: string;

    previousLocationGroupResourceName: string;
    previousLocationGroupName: string;
    nextLocationGroupResourceName: string;
    nextLocationGroupName: string;

    locations: LocationModel[];
}

export class LocationGroupListModel implements ILocationGroupListModel {
    name = '';
    resourceName = '';

    sortIndex = 0;
    regionResourceName = '';
    regionName = '';
}

export class LocationGroupModel extends LocationGroupListModel implements ILocationGroupModel {
    isEventRegion = false;
    eventName = '';
    eventStartDate = '';
    eventEndDate = '';

    previousLocationGroupResourceName = '';
    previousLocationGroupName = '';
    nextLocationGroupResourceName = '';
    nextLocationGroupName = '';

    locations = [];
}
