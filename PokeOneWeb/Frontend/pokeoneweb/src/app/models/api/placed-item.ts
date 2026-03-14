export interface PlacedItem {
    itemName: string;
    itemResourceName: string;
    itemSpriteName: string;

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
    isRemoved: boolean;
    quantity: number;

    notes: string;

    screenshot: string;
}
