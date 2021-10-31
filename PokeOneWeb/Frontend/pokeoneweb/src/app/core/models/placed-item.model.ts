export interface IPlacedItemModel{
    itemResourceName: string;
    itemName: string;
    regionName: string;
    locationName: string;
    locationSortIndex: number;
    sortIndex: number;
    index: number;
    placementDescription: string;
    isHidden: boolean;
    isConfirmed: boolean;
    quantity: number;
    screenshot: string;
}

export class PlacedItemModel implements IPlacedItemModel {
    itemResourceName = '';
    itemName = '';
    regionName = '';
    locationName = '';
    locationSortIndex = 0;
    sortIndex = 0;
    index = 0;
    placementDescription = '';
    isHidden = false;
    isConfirmed = true;
    quantity = 1;
    screenshot = '';
}