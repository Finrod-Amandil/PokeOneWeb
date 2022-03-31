export interface IItemStatBoostModel {
    itemName: string;
    itemResourceName: string;
    itemEffect: string;
    
    attackBoost: number;
    defenseBoost: number;
    specialAttackBoost: number;
    specialDefenseBoost: number;
    speedBoost: number;
    hitPointsBoost: number;
    
    hasRequiredPokemon: boolean;
    requiredPokemonResourceName: string;
}

export class ItemStatBoostModel implements IItemStatBoostModel {
    itemName = '';
    itemResourceName = '';
    itemEffect = '';
    
    attackBoost = 1;
    defenseBoost = 1;
    specialAttackBoost = 1;
    specialDefenseBoost = 1;
    speedBoost = 1;
    hitPointsBoost = 1;

    hasRequiredPokemon = false;
    requiredPokemonResourceName = '';
}