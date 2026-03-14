export interface Spawn {
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
    isRemoved: boolean;
    lowestLevel: number;
    highestLevel: number;

    seasons: Season[];
    timesOfDay: TimeOfDay[];

    rarityString: string;
    rarityValue: number;

    notes: string;
}

export interface TimeOfDay {
    sortIndex: number;
    abbreviation: string;
    name: string;
    color: string;
    times: string;
}

export interface Season {
    sortIndex: number;
    abbreviation: string;
    name: string;
    color: string;
}
