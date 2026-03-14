import { Component, computed, inject } from '@angular/core';
import { Router } from '@angular/router';
import { PageHeaderComponent } from '../../layout/page-header/page-header';
import { Region } from '../../models/api/location';
import { LocationDataService } from '../../service/data/location.data.service';
import { RegionListSortService } from './region-list-sort.service';

@Component({
    selector: 'pokeoneweb-region-list',
    imports: [PageHeaderComponent],
    templateUrl: './region-list.html',
    styleUrl: './region-list.scss',
})
export class RegionListPage {
    private locationDataService = inject(LocationDataService);
    private sortService = inject(RegionListSortService);
    private router = inject(Router);

    regions = computed(() => this.sortService.sort(this.locationDataService.regions.value()));

    navigateToLocationPage(region: Region) {
        if (region.isReleased) {
            this.router.navigate([region.resourceName]);
        }
    }

    getRegionType(region: Region): string {
        if (region.isMainRegion) {
            return 'Main Region';
        }
        if (region.isSideRegion) {
            return 'Side Region';
        }
        if (region.isEventRegion) {
            return 'Event Region';
        }
        return '';
    }
}
