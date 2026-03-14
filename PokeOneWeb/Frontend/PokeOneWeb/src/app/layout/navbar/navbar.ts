import { Component, computed, inject } from '@angular/core';
import { Region } from '../../models/api/location';
import { LocationDataService } from '../../service/data/location.data.service';
import { NavbarItemComponent } from './navbar-item/navbar-item';
import { SubmenuItem } from './submenu-item';

@Component({
    selector: 'pokeoneweb-navbar',
    imports: [NavbarItemComponent],
    templateUrl: './navbar.html',
    styleUrl: './navbar.scss',
})
export class NavbarComponent {
    private locationDataService = inject(LocationDataService);

    regionSubmenuItems = computed<SubmenuItem[]>(() =>
        this.locationDataService.regions
            .value()
            .filter((r) => this.showRegionInNavigation(r))
            .map((r) => ({
                caption: r.name,
                link: r.resourceName,
                isEventRegion: r.isEventRegion,
            })),
    );

    private showRegionInNavigation(region: Region): boolean {
        if (!region.isReleased) {
            return false;
        }

        if (!region.isEventRegion) {
            return true;
        }

        if (!region.eventEndDate) {
            return true;
        }

        var eventEndYear = new Date(region.eventEndDate).getFullYear();
        var currentYear = new Date().getFullYear();

        return currentYear - eventEndYear <= 1;
    }
}
