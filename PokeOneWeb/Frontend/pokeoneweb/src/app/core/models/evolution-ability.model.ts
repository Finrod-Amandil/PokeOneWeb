export interface IEvolutionAbilityModel {
    relativeStageIndex: number;
    pokemonResourceName: string;
    pokemonSortIndex: number;
    pokemonName: string;
    spriteName: string;
    abilityName: string;
}

export class EvolutionAbilityModel implements IEvolutionAbilityModel {
    relativeStageIndex = 0;
    pokemonResourceName = '';
    pokemonSortIndex = 0;
    pokemonName = '';
    spriteName = '';
    abilityName = '';
}