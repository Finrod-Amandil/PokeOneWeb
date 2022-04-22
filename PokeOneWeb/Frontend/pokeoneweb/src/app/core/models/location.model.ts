import { PlacedItemModel } from "./placed-item.model";
import { SpawnModel } from "./spawn.model";

export interface ILocationModel {
    name: string;
    sortIndex: number;
    
    isDiscoverable: boolean;
    notes: string;

    spawns: SpawnModel[];
    placedItems: PlacedItemModel[];
}

export class LocationModel implements ILocationModel {
    name = "";
    sortIndex = 0;
    
    isDiscoverable = false;
    notes = "";

    spawns = [];
    placedItems = [];
}
