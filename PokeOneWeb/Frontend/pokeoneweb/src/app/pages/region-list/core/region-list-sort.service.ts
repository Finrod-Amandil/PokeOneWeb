import { Injectable } from '@angular/core';
import { IRegionListModel } from 'src/app/core/models/region-list.model';

@Injectable({
    providedIn: 'root'
})
export class RegionListSortService {
    public sort(models: IRegionListModel[]) {
        return models.slice().sort((n1, n2) => {
            if (n1.isMainRegion && (n2.isSideRegion || n2.isEventRegion)) {
                return -1;
            }
            if (n1.isSideRegion && n2.isEventRegion) {
                return -1;
            }
            if (n2.isMainRegion && (n1.isSideRegion || n1.isEventRegion)) {
                return 1;
            }
            if (n2.isSideRegion && n1.isEventRegion) {
                return 1;
            }
            return 0;
        });
    }
}
