import { AttackEffectivity } from './attack-effectivity';
import { Spawn } from './spawn';

export interface PokemonVarietyName {
    name: string;
    resourceName: string;
}

export interface PokemonVarietyBase extends PokemonVarietyName {
    sortIndex: number;
    pokedexNumber: number;
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
    availabilityDescription: string;

    pvpTier: string;
    pvpTierSortIndex: number;

    generation: number;
    isFullyEvolved: boolean;
    isMega: boolean;

    urls: PokemonVarietyUrl[];
}

export interface PokemonVariety extends PokemonVarietyBase {
    previousPokemonResourceName: string;
    previousPokemonSpriteName: string;
    previousPokemonName: string;
    nextPokemonResourceName: string;
    nextPokemonSpriteName: string;
    nextPokemonName: string;

    catchRate: number;
    hasGender: boolean;
    maleRatio: number;
    femaleRatio: number;
    eggCycles: number;
    height: number;
    weight: number;
    expYield: number;

    attackEv: number;
    specialAttackEv: number;
    defenseEv: number;
    specialDefenseEv: number;
    speedEv: number;
    hitPointsEv: number;

    primaryAbilityAttackBoost: number;
    primaryAbilitySpecialAttackBoost: number;
    primaryAbilityDefenseBoost: number;
    primaryAbilitySpecialDefenseBoost: number;
    primaryAbilitySpeedBoost: number;
    primaryAbilityBoostConditions: string;

    secondaryAbilityAttackBoost: number;
    secondaryAbilitySpecialAttackBoost: number;
    secondaryAbilityDefenseBoost: number;
    secondaryAbilitySpecialDefenseBoost: number;
    secondaryAbilitySpeedBoost: number;
    secondaryAbilityBoostConditions: string;

    hiddenAbilityAttackBoost: number;
    hiddenAbilitySpecialAttackBoost: number;
    hiddenAbilityDefenseBoost: number;
    hiddenAbilitySpecialDefenseBoost: number;
    hiddenAbilitySpeedBoost: number;
    hiddenAbilityBoostConditions: string;

    notes: string;

    forms: PokemonForm[];
    evolutions: Evolution[];
    primaryEvolutionAbilities: EvolutionAbility[];
    secondaryEvolutionAbilities: EvolutionAbility[];
    hiddenEvolutionAbilities: EvolutionAbility[];
    defenseAttackEffectivities: AttackEffectivity[];
    spawns: Spawn[];
    learnableMoves: LearnableMove[];
}

export interface PokemonForm {
    name: string;
    sortIndex: number;
    spriteName: string;
    availability: string;
}

export interface Evolution {
    baseName: string;
    baseResourceName: string;
    baseSpriteName: string;
    basePrimaryElementalType: string;
    baseSecondaryElementalType: string;
    baseSortIndex: number;
    baseStage: number;

    evolvedName: string;
    evolvedResourceName: string;
    evolvedSpriteName: string;
    evolvedPrimaryElementalType: string;
    evolvedSecondaryElementalType: string;
    evolvedSortIndex: number;
    evolvedStage: number;

    evolutionTrigger: string;
    isReversible: boolean;
    isAvailable: boolean;
}

export interface EvolutionAbility {
    relativeStageIndex: number;
    pokemonResourceName: string;
    pokemonSortIndex: number;
    pokemonName: string;
    spriteName: string;
    abilityName: string;
}

export interface LearnableMove {
    isAvailable: boolean;
    moveName: string;
    elementalType: string;
    damageClass: string;
    attackPower: number;
    effectivePower: number;
    hasStab: boolean;
    accuracy: number;
    powerPoints: number;
    priority: number;
    effectDescription: string;

    learnMethods: LearnMethod[];
}

export interface LearnMethod {
    isAvailable: boolean;
    availability: string;
    availabilityDescription: string;
    learnMethodName: string;
    description: string;
    sortIndex: number;
}

export interface PokemonVarietyUrl {
    name: string;
    url: string;
}

export const defaultPokemonVariety: PokemonVariety = {
    name: '',
    resourceName: '',

    sortIndex: 0,
    pokedexNumber: 0,
    spriteName: '',

    primaryElementalType: '',
    secondaryElementalType: '',

    attack: 0,
    specialAttack: 0,
    defense: 0,
    specialDefense: 0,
    speed: 0,
    hitPoints: 0,
    statTotal: 0,
    bulk: 0,

    primaryAbility: '',
    primaryAbilityEffect: '',
    secondaryAbility: '',
    secondaryAbilityEffect: '',
    hiddenAbility: '',
    hiddenAbilityEffect: '',

    availability: '',
    availabilityDescription: '',

    pvpTier: '',
    pvpTierSortIndex: 0,

    generation: 0,
    isFullyEvolved: false,
    isMega: false,

    urls: [],

    previousPokemonResourceName: '',
    previousPokemonSpriteName: '',
    previousPokemonName: '',
    nextPokemonResourceName: '',
    nextPokemonSpriteName: '',
    nextPokemonName: '',

    catchRate: 0,
    hasGender: false,
    maleRatio: 0,
    femaleRatio: 0,
    eggCycles: 0,
    height: 0,
    weight: 0,
    expYield: 0,

    attackEv: 0,
    specialAttackEv: 0,
    defenseEv: 0,
    specialDefenseEv: 0,
    speedEv: 0,
    hitPointsEv: 0,

    primaryAbilityAttackBoost: 0,
    primaryAbilitySpecialAttackBoost: 0,
    primaryAbilityDefenseBoost: 0,
    primaryAbilitySpecialDefenseBoost: 0,
    primaryAbilitySpeedBoost: 0,
    primaryAbilityBoostConditions: '',

    secondaryAbilityAttackBoost: 0,
    secondaryAbilitySpecialAttackBoost: 0,
    secondaryAbilityDefenseBoost: 0,
    secondaryAbilitySpecialDefenseBoost: 0,
    secondaryAbilitySpeedBoost: 0,
    secondaryAbilityBoostConditions: '',

    hiddenAbilityAttackBoost: 0,
    hiddenAbilitySpecialAttackBoost: 0,
    hiddenAbilityDefenseBoost: 0,
    hiddenAbilitySpecialDefenseBoost: 0,
    hiddenAbilitySpeedBoost: 0,
    hiddenAbilityBoostConditions: '',

    notes: '',

    forms: [],
    evolutions: [],
    primaryEvolutionAbilities: [],
    secondaryEvolutionAbilities: [],
    hiddenEvolutionAbilities: [],
    defenseAttackEffectivities: [],
    spawns: [],
    learnableMoves: [],
};
