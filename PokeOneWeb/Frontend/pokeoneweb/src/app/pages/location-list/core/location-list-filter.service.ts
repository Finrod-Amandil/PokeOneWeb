import { Injectable } from '@angular/core';
import { ILocationListModel } from 'src/app/core/models/location-list.model';
import { LocationListFilterModel } from './location-list-filter.model';

@Injectable({
    providedIn: 'root'
})
export class LocationListFilterService {
    public async applyFilter(filter: LocationListFilterModel, allModels: ILocationListModel[]): Promise<ILocationListModel[]> {
        return allModels.filter((i) => this.isIncluded(i, filter));
    }

    private isIncluded(i: ILocationListModel, filter: LocationListFilterModel): boolean {
        return (
            this.filterSearchTerm(i, filter)
        );
    }

    private filterSearchTerm(i: ILocationListModel, filter: LocationListFilterModel): boolean {
        if (!filter.searchTerm) {
            return true;
        }

        const searchTerm = filter.searchTerm.toLowerCase();

        return (
            i.name.toLowerCase().includes(searchTerm)
        );
    }
}
