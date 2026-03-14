import { PlacedItem } from './placed-item';
import { Spawn } from './spawn';

export interface Region {
    name: string;
    resourceName: string;
    isEventRegion: boolean;
    eventName: string;
    eventStartDate: string;
    eventEndDate: string;
    color: string;
    description: string;
    isReleased: boolean;
    isMainRegion: boolean;
    isSideRegion: boolean;
}

export interface LocationGroupBase {
    name: string;
    resourceName: string;

    sortIndex: number;
    regionResourceName: string;
    regionName: string;
}

export interface LocationGroup extends LocationGroupBase {
    isEventRegion: boolean;
    eventName: string;
    eventStartDate: string;
    eventEndDate: string;

    previousLocationGroupResourceName: string;
    previousLocationGroupName: string;
    nextLocationGroupResourceName: string;
    nextLocationGroupName: string;

    locations: Location[];
}

export interface Location {
    name: string;
    sortIndex: number;
    isDiscoverable: boolean;

    notes: string;

    spawns: Spawn[];
    placedItems: PlacedItem[];
}

export const defaultLocationGroup: LocationGroup = {
    name: '',
    resourceName: '',
    sortIndex: 0,
    regionResourceName: '',
    regionName: '',
    isEventRegion: false,
    eventName: '',
    eventStartDate: '',
    eventEndDate: '',
    previousLocationGroupResourceName: '',
    previousLocationGroupName: '',
    nextLocationGroupResourceName: '',
    nextLocationGroupName: '',

    locations: [],
};
