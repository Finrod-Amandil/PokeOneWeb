import { ISeasonModel } from './season.model';
import { ITimeModel } from './time.model';

export interface ISpawnModel {
    pokemonFormSortIndex: number;
    locationSortIndex: number;
    pokemonResourceName: string;
    pokemonName: string;
    spriteName: string;

    locationName: string;
    locationResourceName: string;
    regionName: string;
    regionColor: string;
    isEvent: boolean;
    eventName: string;
    eventDateRange: string;
    spawnType: string;
    spawnTypeSortIndex: number;
    spawnTypeColor: string;
    isSyncable: boolean;
    isInfinite: boolean;

    seasons: ISeasonModel[]
    times: ITimeModel[]

    rarityString: string;
    rarityValue: number;
    notes: string;
}

export class SpawnModel implements ISpawnModel {
    pokemonFormSortIndex = 0;
    locationSortIndex = 0;
    pokemonResourceName = '';
    pokemonName = '';
    spriteName = '';

    locationName = '';
    locationResourceName = '';
    regionName = '';
    regionColor = '';
    isEvent = false;
    eventName = '';
    eventDateRange = '';
    spawnType = '';
    spawnTypeSortIndex = 0;
    spawnTypeColor = '';
    isSyncable = false;
    isInfinite = false;

    seasons = [];
    times = [];

    rarityString = '';
    rarityValue = 0;
    notes = '';
}