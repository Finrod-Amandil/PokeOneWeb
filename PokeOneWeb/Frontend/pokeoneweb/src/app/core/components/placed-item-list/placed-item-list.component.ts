import { Component, Input, OnInit } from '@angular/core';
import { IItemModel, ItemModel } from '../../models/item.model';
import { PlacedItemListColumn } from './core/placed-item-list-column.enum';
import { PlacedItemListSortService } from './core/placed-item-list-sort.service';
import { PlacedItemListModel } from './core/placed-item-list.model';

@Component({
  selector: 'app-placed-item-list',
  templateUrl: './placed-item-list.component.html',
  styleUrls: ['./placed-item-list.component.scss']
})
export class PlacedItemListComponent implements OnInit {
  @Input() item : IItemModel = new ItemModel;

  public model : PlacedItemListModel = new PlacedItemListModel();
  public PlacedItemListColumn = PlacedItemListColumn;

  constructor(
    private sortService: PlacedItemListSortService
  ) { }

  ngOnInit(): void {
    this.model.item = this.item;
    this.applyInitialSorting();
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

  private applyInitialSorting() {
    if (!this.model.item) return;

    this.model.item.placedItems = this.sortService.sortPlacedItems(
        this.model.item.placedItems,
        PlacedItemListColumn.Location,
        1
    );
  }
}
