export interface IHuntingConfigurationModel {
    pokemonResourceName: string;
    pokemonName: string;
    nature: string;
    natureEffect: string;
    ability: string;
}

export class HuntingConfigurationModel implements IHuntingConfigurationModel { 
    pokemonResourceName = '';
    pokemonName = '';
    nature = '';
    natureEffect = '';
    ability = '';
}