import { LocationModel } from "./location.model";

export interface ILocationGroupModel {
    locationGroupResourceName: string;
    locationGroupName: string;

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

    locationModel: LocationModel[];
}

export class LocationGroupModel implements ILocationGroupModel {
    locationGroupResourceName = "";
    locationGroupName = "";
    
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

    locationModel = [];
}
