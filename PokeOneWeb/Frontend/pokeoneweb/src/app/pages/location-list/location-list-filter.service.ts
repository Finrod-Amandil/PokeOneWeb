import { Injectable } from '@angular/core';
import { LocationGroupBase } from '../../models/api/location';

@Injectable({
    providedIn: 'root',
})
export class LocationListFilterService {
    filter(allModels: LocationGroupBase[], filter: LocationListFilter): LocationGroupBase[] {
        return allModels.filter((i) => this.isIncluded(i, filter));
    }

    private isIncluded(i: LocationGroupBase, filter: LocationListFilter): boolean {
        return this.filterSearchTerm(i, filter);
    }

    private filterSearchTerm(i: LocationGroupBase, filter: LocationListFilter): boolean {
        if (!filter.searchTerm) {
            return true;
        }

        const searchTerm = filter.searchTerm.toLowerCase();

        return i.name.toLowerCase().includes(searchTerm);
    }
}

export interface LocationListFilter {
    searchTerm: string;
}
