import { IPokemonNameModel, PokemonNameModel } from "./pokemon-name.model";

export interface IBasicPokemonModel extends IPokemonNameModel {
    type1: string;
    type2: string;

    atk: number;
    spa: number;
    def: number;
    spd: number;
    spe: number;
    hp: number;
    statTotal: number;

    primaryAbility: string;
    primaryAbilityEffect: string;
    secondaryAbility: string;
    secondaryAbilityEffect: string;
    hiddenAbility: string;
    hiddenAbilityEffect: string;

    pvpTier: string;
    pvpTierSortIndex: number;

    generation: number;
    isFullyEvolved: boolean;
    isMega: boolean;

    smogonUrl: string;
    bulbapediaUrl: string;
    pokeOneCommunityUrl: string;
    pokemonShowDownUrl: string;
    serebiiUrl: string;
    pokemonDbUrl: string;

    notes: string;
}

export class BasicPokemonModel extends PokemonNameModel implements IBasicPokemonModel {
    type1 = '';
    type2 = '';

    atk = 0;
    spa = 0;
    def = 0;
    spd = 0;
    spe = 0;
    hp = 0;
    statTotal = 0;

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

    smogonUrl = '';
    bulbapediaUrl = '';
    pokeOneCommunityUrl = '';
    pokemonShowDownUrl = '';
    serebiiUrl = '';
    pokemonDbUrl = '';

    notes = '';
}