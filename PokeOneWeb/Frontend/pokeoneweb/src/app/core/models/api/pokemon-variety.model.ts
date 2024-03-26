import { IAttackEffectivityModel } from './attack-effectivity.model';
import { IEvolutionAbilityModel } from './evolution-ability.model';
import { IEvolutionModel } from './evolution.model';
import { ILearnableMoveModel } from './learnable-move.model';
import { IPokemonFormModel } from './pokemon-form.model';
import { IPokemonVarietyUrlModel } from './pokemon-variety-url.model';
import { ISpawnModel } from './spawn.model';

export interface IPokemonVarietyNameModel {
    name: string;
    resourceName: string;
}

export interface IPokemonVarietyListModel extends IPokemonVarietyNameModel {
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

    urls: IPokemonVarietyUrlModel[];
}

export interface IPokemonVarietyModel extends IPokemonVarietyListModel {
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

    forms: IPokemonFormModel[];
    evolutions: IEvolutionModel[];
    primaryEvolutionAbilities: IEvolutionAbilityModel[];
    secondaryEvolutionAbilities: IEvolutionAbilityModel[];
    hiddenEvolutionAbilities: IEvolutionAbilityModel[];
    defenseAttackEffectivities: IAttackEffectivityModel[];
    spawns: ISpawnModel[];
    learnableMoves: ILearnableMoveModel[];
}

export class PokemonVarietyNameModel implements IPokemonVarietyNameModel {
    name = '';
    resourceName = '';
}

export class PokemonVarietyListModel extends PokemonVarietyNameModel implements IPokemonVarietyListModel {
    sortIndex = 0;
    pokedexNumber = 0;
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
    availabilityDescription = '';

    pvpTier = '';
    pvpTierSortIndex = 0;

    generation = 0;
    isFullyEvolved = false;
    isMega = false;

    urls = [];
}

export class PokemonVarietyModel extends PokemonVarietyListModel implements IPokemonVarietyModel {
    previousPokemonResourceName = '';
    previousPokemonSpriteName = '';
    previousPokemonName = '';
    nextPokemonResourceName = '';
    nextPokemonSpriteName = '';
    nextPokemonName = '';

    catchRate = 0;
    hasGender = true;
    maleRatio = 50;
    femaleRatio = 50;
    eggCycles = 0;
    height = 0;
    weight = 0;
    expYield = 0;

    attackEv = 0;
    specialAttackEv = 0;
    defenseEv = 0;
    specialDefenseEv = 0;
    speedEv = 0;
    hitPointsEv = 0;

    primaryAbilityAttackBoost = 1;
    primaryAbilitySpecialAttackBoost = 1;
    primaryAbilityDefenseBoost = 1;
    primaryAbilitySpecialDefenseBoost = 1;
    primaryAbilitySpeedBoost = 1;
    primaryAbilityBoostConditions = '';

    secondaryAbilityAttackBoost = 1;
    secondaryAbilitySpecialAttackBoost = 1;
    secondaryAbilityDefenseBoost = 1;
    secondaryAbilitySpecialDefenseBoost = 1;
    secondaryAbilitySpeedBoost = 1;
    secondaryAbilityBoostConditions = '';

    hiddenAbilityAttackBoost = 1;
    hiddenAbilitySpecialAttackBoost = 1;
    hiddenAbilityDefenseBoost = 1;
    hiddenAbilitySpecialDefenseBoost = 1;
    hiddenAbilitySpeedBoost = 1;
    hiddenAbilityBoostConditions = '';

    notes = '';

    forms = [];
    evolutions = [];
    primaryEvolutionAbilities = [];
    secondaryEvolutionAbilities = [];
    hiddenEvolutionAbilities = [];
    defenseAttackEffectivities = [];
    spawns = [];
    learnableMoves = [];
}
