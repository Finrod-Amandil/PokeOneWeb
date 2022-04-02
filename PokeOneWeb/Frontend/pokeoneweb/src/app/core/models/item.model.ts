import { IPlacedItemModel } from './placed-item.model';

export interface IItemModel {
    resourceName: string;
    sortIndex: number;
    name: string;
    description: string;
    effect: string;
    isAvailable: boolean;
    spriteName: string;
    bagCategoryName: string;
    bagCategorySortIndex: number;
    placedItems: IPlacedItemModel[];
}

export class ItemModel implements IItemModel {
    resourceName = '';
    sortIndex = 0;
    name = '';
    description = '';
    effect = '';
    isAvailable = false;
    spriteName = '';
    bagCategoryName = '';
    bagCategorySortIndex = 0;
    placedItems = [];
}
