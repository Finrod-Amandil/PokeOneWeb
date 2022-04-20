export interface ILocationListModel {
    resourceName: string;
    name: string;
    sortIndex: number;
    regionResourceName: string;
    regionName: string;
}

export class LocationListModel implements ILocationListModel {
    resourceName = '';
    name = '';
    sortIndex = 0;
    regionResourceName = '';
    regionName = '';
}
