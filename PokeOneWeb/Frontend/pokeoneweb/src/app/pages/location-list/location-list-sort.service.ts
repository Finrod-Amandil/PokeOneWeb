import { Injectable } from '@angular/core';
import { LocationGroupBase } from '../../models/api/location';
import { SortDirection } from '../../models/sort-direction.enum';
import { LocationListColumn } from './location-list-column.enum';

@Injectable({
    providedIn: 'root',
})
export class LocationListSortService {
    sort(
        models: LocationGroupBase[],
        sortColumn: LocationListColumn,
        sortDirection: SortDirection,
    ) {
        switch (sortColumn) {
            case LocationListColumn.Name:
                return this.sortName(models, sortDirection);
            case LocationListColumn.SortIndex:
                return this.sortIndex(models, sortDirection);
            default:
                return models;
        }
    }

    private sortName(
        models: LocationGroupBase[],
        sortDirection: SortDirection,
    ): LocationGroupBase[] {
        return models.slice().sort((n1, n2) => {
            if (n1.name > n2.name) {
                return sortDirection == SortDirection.Ascending ? 1 : -1;
            }

            if (n1.name < n2.name) {
                return sortDirection == SortDirection.Ascending ? -1 : 1;
            }

            return 0;
        });
    }

    private sortIndex(
        models: LocationGroupBase[],
        sortDirection: SortDirection,
    ): LocationGroupBase[] {
        return models.slice().sort((n1, n2) => {
            if (n1.sortIndex > n2.sortIndex) {
                return sortDirection == SortDirection.Ascending ? 1 : -1;
            }

            if (n1.sortIndex < n2.sortIndex) {
                return sortDirection == SortDirection.Ascending ? -1 : 1;
            }

            return 0;
        });
    }
}
