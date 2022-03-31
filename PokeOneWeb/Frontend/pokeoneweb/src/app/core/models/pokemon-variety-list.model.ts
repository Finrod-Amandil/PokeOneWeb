import { IPokemonVarietyUrlModel } from './pokemon-variety-url.model';

export interface IPokemonVarietyListModel {
    resourceName: string;
    sortIndex: number;
    pokedexNumber: number;
    name: string;
    spriteName: string;

    primaryElementalType: string;
    secondaryElementalType: string;

    attack: number;
    specialAttack: number;
    defense: number;
    specialDefense: number;
    speed: number;
    hitPoints: number;
    statTotal: number;
    bulk: number;

    primaryAbility: string;
    primaryAbilityEffect: string;
    secondaryAbility: string;
    secondaryAbilityEffect: string;
    hiddenAbility: string;
    hiddenAbilityEffect: string;

    availability: string;
    pvpTier: string;
    pvpTierSortIndex: number;
    generation: number;
    isFullyEvolved: boolean;
    isMega: boolean;

    urls: IPokemonVarietyUrlModel[];

    notes: string;
}

export class PokemonVarietyListModel implements IPokemonVarietyListModel {
    resourceName = '';
    sortIndex = 0;
    pokedexNumber = 0;
    name = '';
    spriteName = '';

    primaryElementalType = '';
    secondaryElementalType = '';

    attack = 0;
    specialAttack = 0;
    defense = 0;
    specialDefense = 0;
    speed = 0;
    hitPoints = 0;
    statTotal = 0;
    bulk = 0;

    primaryAbility = '';
    primaryAbilityEffect = '';
    secondaryAbility = '';
    secondaryAbilityEffect = '';
    hiddenAbility = '';
    hiddenAbilityEffect = '';

    availability = '';
    pvpTier = '';
    pvpTierSortIndex = 0;
    generation = 0;
    isFullyEvolved = false;
    isMega = false;

    urls = [];

    notes = '';
}
