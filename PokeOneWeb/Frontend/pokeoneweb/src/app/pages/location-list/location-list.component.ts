import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'pokeone-location-list',
  templateUrl: './location-list.component.html',
  styleUrls: ['./location-list.component.scss']
})

export class LocationListComponent implements OnInit {
  ngOnInit(): void {}
  /*
    public model: ItemListModel = new ItemListModel();

    public column = ItemListColumn;

    private timeOut: any;
    private timeOutDuration = 500;

    constructor(
        private titleService: Title,
        private itemService: ItemService,
        private filterService: ItemListFilterService,
        private sortService: ItemListSortService,
        private router: Router
    ) {}

    ngOnInit(): void {
        this.titleService.setTitle(`Items - ${WEBSITE_NAME}`);

        this.itemService.getAll().subscribe((response) => {
            this.model.itemModels = response as IItemListModel[];

            this.model.displayedItemModels = this.sortService.sort(this.model.itemModels, ItemListColumn.Name, 1);

            this.model.bagCategories = [...new Set(this.model.itemModels.map((i) => i.bagCategoryName))];
            this.model.bagCategories = this.model.bagCategories.sort((a, b) => (a > b ? 1 : -1));
        });
    }

    public async onFilterChangedDelayed() {
        clearTimeout(this.timeOut);
        this.timeOut = setTimeout(() => {
            this.onFilterChanged();
        }, this.timeOutDuration);
    }

    public async onFilterChanged() {
        const filtered = await this.filterService.applyFilter(this.model.filter, this.model.itemModels);

        this.model.displayedItemModels = this.sortService.sort(
            filtered,
            this.model.sortColumn,
            this.model.sortDirection
        );
    }

    public trackById(index: number, item: IItemListModel): number {
        return item.sortIndex;
    }

    public sort(sortColumn: ItemListColumn, sortDirection: number) {
        this.model.sortColumn = sortColumn;
        this.model.sortDirection = sortDirection;

        this.model.displayedItemModels = this.sortService.sort(
            this.model.displayedItemModels,
            sortColumn,
            sortDirection
        );
    }

    public getSortButtonClass(sortColumn: ItemListColumn, sortDirection: number): string {
        if (this.model.sortColumn === sortColumn && this.model.sortDirection === sortDirection) {
            return 'sorted';
        }
        return 'unsorted';
    }

    public getAvailabilityClass(isAvailable: boolean): string {
        return isAvailable ? 'availability-obtainable' : 'availability-unobtainable';
    }

    public navigateToDetailPage(itemResourceName: string) {
        this.router.navigate([itemResourceName]);
    }*/
}