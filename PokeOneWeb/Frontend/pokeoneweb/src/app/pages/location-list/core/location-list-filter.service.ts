import { Injectable } from '@angular/core';
import { ILocationGroupListModel } from 'src/app/core/models/api/location-group.model';
import { LocationListFilterModel } from './location-list-filter.model';

@Injectable({
    providedIn: 'root'
})
export class LocationListFilterService {
    public applyFilter(
        filter: LocationListFilterModel,
        allModels: ILocationGroupListModel[]
    ): ILocationGroupListModel[] {
        return allModels.filter((i) => this.isIncluded(i, filter));
    }

    private isIncluded(i: ILocationGroupListModel, filter: LocationListFilterModel): boolean {
        return this.filterSearchTerm(i, filter);
    }

    private filterSearchTerm(i: ILocationGroupListModel, filter: LocationListFilterModel): boolean {
        if (!filter.searchTerm) {
            return true;
        }

        const searchTerm = filter.searchTerm.toLowerCase();

        return i.name.toLowerCase().includes(searchTerm);
    }
}
