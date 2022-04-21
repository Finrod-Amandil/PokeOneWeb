import { Component, OnInit } from '@angular/core';
import { IRegionListModel } from '../../models/region-list.model';
import { RegionService } from '../../services/api/region.service';
import { SubmenuItemModel } from '../navbar-item/core/submenu-item.model';

@Component({
    selector: 'pokeone-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
    constructor(private regionService: RegionService) {}

    public regionSubmenuItems : SubmenuItemModel[] = [];

    ngOnInit(): void {
        this.regionService.getAll().subscribe((response) => {
            const regions = response as IRegionListModel[];

            regions.forEach(r => {
                if (r.isReleased) {
                    const submenuItem = new SubmenuItemModel();
                    submenuItem.caption = r.name;
                    submenuItem.link = r.resourceName;
                    submenuItem.isEventRegion = r.isEventRegion;

                    this.regionSubmenuItems.push(submenuItem)
                }
            })
        })
    }
}
