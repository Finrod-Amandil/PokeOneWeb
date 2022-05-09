import { Injectable } from '@angular/core';
import { IRegionListModel } from 'src/app/core/models/region.model';

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

            // Sort events by date
            if (n1.isEventRegion && n2.isEventRegion) {
                if (!n1.eventStartDate && !!n2.eventStartDate) {
                    return -1;
                }
                if (!!n1.eventStartDate && !n2.eventStartDate) {
                    return 1;
                }

                const event1StartDate = new Date(n1.eventStartDate);
                const event2StartDate = new Date(n2.eventStartDate);
                if (event1StartDate && event2StartDate && event1StartDate > event2StartDate) {
                    return -1;
                }
                if (event1StartDate && event2StartDate && event1StartDate < event2StartDate) {
                    return 1;
                }
            }
            return 0;
        });
    }
}
