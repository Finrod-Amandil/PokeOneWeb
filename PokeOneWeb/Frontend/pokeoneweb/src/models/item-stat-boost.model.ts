export interface IItemStatBoostModel {
    itemName: string;
    itemResourceName: string;
    itemEffect: string;
    
    atkBoost: number;
    defBoost: number;
    spaBoost: number;
    spdBoost: number;
    speBoost: number;
    hpBoost: number;

    hasRequiredPokemon: boolean;
    requiredPokemonName: string;
    requiredPokemonResourceName: string;
}

export class ItemStatBoostModel implements IItemStatBoostModel {
    itemName = '';
    itemResourceName = '';
    itemEffect = '';
    
    atkBoost = 1;
    defBoost = 1;
    spaBoost = 1;
    spdBoost = 1;
    speBoost = 1;
    hpBoost = 1;

    hasRequiredPokemon = false;
    requiredPokemonName = '';
    requiredPokemonResourceName = '';
}