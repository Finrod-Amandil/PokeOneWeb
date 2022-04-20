export interface IRegionListModel {
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

export class RegionListModel implements IRegionListModel {
    name = '';
    resourceName = '';
    isEventRegion = false;
    eventName = '';
    eventStartDate = '';
    eventEndDate = '';
    color = '';
    description = '';
    isReleased = false;
    isMainRegion = false;
    isSideRegion = false;
}
