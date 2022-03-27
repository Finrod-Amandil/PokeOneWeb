export interface IItemListModel {
	resourceName: string;
	sortIndex: number;
	name: string;
	description: string;
	effect: string;
	isAvailable: boolean;
	spriteName: string;
	bagCategoryName: string;
	bagCategorySortIndex: number;
}

export class ItemListModel implements IItemListModel {
	resourceName = '';
	sortIndex = 0;
	name = '';
	description = '';
	effect = '';
	isAvailable = false;
	spriteName = '';
	bagCategoryName = '';
	bagCategorySortIndex = 0;
}
