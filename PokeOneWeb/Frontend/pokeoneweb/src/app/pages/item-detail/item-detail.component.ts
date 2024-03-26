import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { WEBSITE_NAME } from 'src/app/core/constants/string.constants';
import { IItemModel } from 'src/app/core/models/api/item.model';
import { ItemService } from 'src/app/core/services/api/item.service';
import { ItemDetailModel } from './core/item-detail.model';

@Component({
    selector: 'pokeone-item-detail',
    templateUrl: './item-detail.component.html',
    styleUrls: ['./item-detail.component.scss']
})
export class ItemDetailComponent implements OnInit {
    public model: ItemDetailModel = new ItemDetailModel();

    constructor(private route: ActivatedRoute, private itemService: ItemService, private titleService: Title) {}

    ngOnInit(): void {
        this.route.data.subscribe((result) => {
            this.model.itemName = result['resourceName'];

            this.titleService.setTitle(`${this.model.itemName} - ${WEBSITE_NAME}`);

            this.itemService.getByName(this.model.itemName).subscribe((result) => {
                this.model.item = result as IItemModel;

                this.titleService.setTitle(`${this.model.item.name} - ${WEBSITE_NAME}`);
            });
        });
    }

    public getAvailabilityClass(isAvailable: boolean): string {
        return isAvailable ? 'availability-obtainable' : 'availability-unobtainable';
    }
}
