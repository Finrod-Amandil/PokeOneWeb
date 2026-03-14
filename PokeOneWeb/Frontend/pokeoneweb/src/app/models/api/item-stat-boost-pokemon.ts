import { SELECT_OPTION_NONE } from '../../constants/string.constants';

export interface ItemStatBoostForPokemon {
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

export const defaultItemStatBoostForPokemon: ItemStatBoostForPokemon = {
    itemName: SELECT_OPTION_NONE,
    itemResourceName: '',
    itemEffect: '',

    attackBoost: 1,
    defenseBoost: 1,
    specialAttackBoost: 1,
    specialDefenseBoost: 1,
    speedBoost: 1,
    hitPointsBoost: 1,

    hasRequiredPokemon: false,
    requiredPokemonResourceName: '',
};
