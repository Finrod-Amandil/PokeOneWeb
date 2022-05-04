import { LocationModel } from "./location.model";

export interface ILocationGroupModel {
    resourceName: string;
    name: string;

    regionResourceName: string;
    regionName: string;
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

export class LocationGroupModel implements ILocationGroupModel {
    resourceName = "";
    name = "";
    
    regionResourceName = "";
    regionName = "";
    isEventRegion = false;
    eventName = "";
    eventStartDate = "";
    eventEndDate = "";

    previousLocationGroupResourceName = "";
    previousLocationGroupName = "";
    nextLocationGroupResourceName = "";
    nextLocationGroupName = "";

    locations = [];
}
