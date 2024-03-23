import { Injectable } from '@angular/core';
import { ILocationGroupListModel } from 'src/app/core/models/api/location-group.model';
import { LocationListColumn } from './location-list-column.enum';

@Injectable({
    providedIn: 'root'
})
export class LocationListSortService {
    public sort(models: ILocationGroupListModel[], sortColumn: LocationListColumn, sortDirection: number) {
        switch (sortColumn) {
            case LocationListColumn.Name:
                return this.sortName(models, sortDirection);
            case LocationListColumn.SortIndex:
                return this.sortIndex(models, sortDirection);
            default:
                return models;
        }
    }

    private sortName(models: ILocationGroupListModel[], sortDirection: number): ILocationGroupListModel[] {
        return models.slice().sort((n1, n2) => {
            if (n1.name > n2.name) {
                return sortDirection * 1;
            }

            if (n1.name < n2.name) {
                return sortDirection * -1;
            }

            return 0;
        });
    }

    private sortIndex(models: ILocationGroupListModel[], sortDirection: number): ILocationGroupListModel[] {
        return models.slice().sort((n1, n2) => {
            if (n1.sortIndex > n2.sortIndex) {
                return sortDirection * 1;
            }

            if (n1.sortIndex < n2.sortIndex) {
                return sortDirection * -1;
            }

            return 0;
        });
    }
}
