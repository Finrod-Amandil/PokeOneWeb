import { Injectable } from '@angular/core';
import { ILocationListModel } from 'src/app/core/models/location-list.model';
import { LocationListColumn } from './location-list-column.enum';

@Injectable({
    providedIn: 'root'
})
export class LocationListSortService {
    public sort(models: ILocationListModel[], sortColumn: LocationListColumn, sortDirection: number) {
        switch (sortColumn) {
            case LocationListColumn.Name:
                return this.sortName(models, sortDirection);
            default:
                return models;
        }
    }

    private sortName(models: ILocationListModel[], sortDirection: number): ILocationListModel[] {
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
}
