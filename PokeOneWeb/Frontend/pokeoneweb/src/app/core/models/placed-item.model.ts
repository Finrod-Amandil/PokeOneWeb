export interface IPlacedItemModel {
	itemResourceName: string;
	itemName: string;
	regionName: string;
	regionColor: string;
	locationName: string;
	locationResourceName: string;
	locationSortIndex: number;
	sortIndex: number;
	index: number;
	placementDescription: string;
	isHidden: boolean;
	isConfirmed: boolean;
	quantity: number;
	notes: string;
	screenshot: string;
}

export class PlacedItemModel implements IPlacedItemModel {
	itemResourceName = '';
	itemName = '';
	regionName = '';
	regionColor = '';
	locationName = '';
	locationResourceName = '';
	locationSortIndex = 0;
	sortIndex = 0;
	index = 0;
	placementDescription = '';
	isHidden = false;
	isConfirmed = true;
	quantity = 1;
	notes = '';
	screenshot = '';
}
