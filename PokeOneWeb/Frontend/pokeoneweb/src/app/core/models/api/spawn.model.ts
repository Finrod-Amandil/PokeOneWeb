import { ISeasonModel } from './season.model';
import { ITimeOfDayModel } from './time-of-day.model';

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
    eventStartDate: string;
    eventEndDate: string;
    spawnType: string;
    spawnTypeSortIndex: number;
    spawnTypeColor: string;
    isSyncable: boolean;
    isInfinite: boolean;
    lowestLevel: number;
    highestLevel: number;

    seasons: ISeasonModel[];
    timesOfDay: ITimeOfDayModel[];

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
    eventStartDate = '';
    eventEndDate = '';
    spawnType = '';
    spawnTypeSortIndex = 0;
    spawnTypeColor = '';
    isSyncable = false;
    isInfinite = false;
    lowestLevel = 0;
    highestLevel = 0;

    seasons = [];
    timesOfDay = [];

    rarityString = '';
    rarityValue = 0;

    notes = '';
}
