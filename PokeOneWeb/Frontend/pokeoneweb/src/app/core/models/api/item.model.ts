import { IPlacedItemModel } from './placed-item.model';

export interface IItemListModel {
    name: string;
    resourceName: string;
    spriteName: string;

    sortIndex: number;
    description: string;
    effect: string;
    isAvailable: boolean;
    bagCategoryName: string;
    bagCategorySortIndex: number;
}

export interface IItemModel extends IItemListModel {
    placedItems: IPlacedItemModel[];
}

export class ItemListModel implements IItemListModel {
    name = '';
    resourceName = '';
    spriteName = '';

    sortIndex = 0;
    description = '';
    effect = '';
    isAvailable = false;
    bagCategoryName = '';
    bagCategorySortIndex = 0;
}

export class ItemModel extends ItemListModel implements IItemModel {
    placedItems = [];
}
