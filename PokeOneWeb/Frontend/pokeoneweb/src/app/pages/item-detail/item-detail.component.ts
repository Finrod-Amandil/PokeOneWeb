import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { WEBSITE_NAME } from 'src/app/core/constants/string.constants';
import { IItemModel } from 'src/app/core/models/item.model';
import { ItemService } from 'src/app/core/services/api/item.service';
import { ItemDetailSortService } from './core/item-detail-sort.service';
import { ItemDetailModel } from './core/item-detail.model';
import { PlacedItemListColumn } from './core/placed-item-list-column.enum';

@Component({
	selector: 'pokeone-item-detail',
	templateUrl: './item-detail.component.html',
	styleUrls: ['./item-detail.component.scss']
})
export class ItemDetailComponent implements OnInit {
	public model: ItemDetailModel = new ItemDetailModel();

	public placedItemsColumn = PlacedItemListColumn;

	constructor(
		private route: ActivatedRoute,
		private itemService: ItemService,
		private sortService: ItemDetailSortService,
		private titleService: Title
	) {}

	ngOnInit(): void {
		this.route.data.subscribe((result) => {
			this.model.itemName = result['resourceName'];

			this.titleService.setTitle(`${this.model.itemName} - ${WEBSITE_NAME}`);

			this.itemService.getByName(this.model.itemName).subscribe((result) => {
				this.model.item = result as IItemModel;

				this.titleService.setTitle(`${this.model.item.name} - ${WEBSITE_NAME}`);
				this.applyInitialSorting();
			});
		});
	}

	public sortPlacedItems(sortColumn: PlacedItemListColumn, sortDirection: number) {
		if (!this.model.item) return;

		this.model.placedItemsSortedByColumn = sortColumn;
		this.model.placedItemsSortDirection = sortDirection;

		this.model.item.placedItems = this.sortService.sortPlacedItems(
			this.model.item.placedItems,
			sortColumn,
			sortDirection
		);
	}

	public getPlacedItemSortButtonClass(sortColumn: PlacedItemListColumn, sortDirection: number): string {
		if (
			this.model.placedItemsSortedByColumn === sortColumn &&
			this.model.placedItemsSortDirection === sortDirection
		) {
			return 'sorted';
		}
		return 'unsorted';
	}

	public getAvailabilityClass(isAvailable: boolean): string {
		return isAvailable ? 'availability-obtainable' : 'availability-unobtainable';
	}

	private applyInitialSorting() {
		if (!this.model.item) return;

		this.model.item.placedItems = this.sortService.sortPlacedItems(
			this.model.item.placedItems,
			PlacedItemListColumn.Location,
			1
		);
	}
}
