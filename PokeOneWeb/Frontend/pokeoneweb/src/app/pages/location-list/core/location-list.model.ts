import { ILocationListModel } from 'src/app/core/models/location-list.model';
import { LocationListColumn } from './location-list-column.enum';
import { LocationListFilterModel } from './location-list-filter.model';

export class LocationListModel {
    public locationModels: ILocationListModel[] = [];
    public displayedLocationModels: ILocationListModel[] = [];

    public sortColumn: LocationListColumn = LocationListColumn.Name;
    public sortDirection = 1;

    public regionName: string = "";

    public filter: LocationListFilterModel = new LocationListFilterModel();
}