import { PlacedItem } from './placed-item';

export interface ItemBase {
    name: string;
    resourceName: string;
    spriteName: string;

    sortIndex: number;
    description: string;
    effect: string;
    availability: string;
    availabilityDescription: string;
    bagCategoryName: string;
    bagCategorySortIndex: number;
}

export interface Item extends ItemBase {
    placedItems: PlacedItem[];
}

export const defaultItem: Item = {
    name: '',
    resourceName: '',
    spriteName: '',
    sortIndex: 0,
    description: '',
    effect: '',
    availability: 'Unobtainable',
    availabilityDescription: '',
    bagCategoryName: '',
    bagCategorySortIndex: 0,
    placedItems: [],
};
