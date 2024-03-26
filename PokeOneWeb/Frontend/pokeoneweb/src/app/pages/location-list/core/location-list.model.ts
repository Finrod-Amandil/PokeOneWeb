import { ILocationGroupListModel } from 'src/app/core/models/api/location-group.model';
import { LocationListColumn } from './location-list-column.enum';
import { LocationListFilterModel } from './location-list-filter.model';

export class LocationListPageModel {
    public locationModels: ILocationGroupListModel[] = [];
    public displayedLocationModels: ILocationGroupListModel[] = [];

    public sortColumn: LocationListColumn = LocationListColumn.Name;
    public sortDirection = 1;

    public regionName = '';

    public filter: LocationListFilterModel = new LocationListFilterModel();
}
