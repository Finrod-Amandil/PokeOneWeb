import { max, min, schema } from '@angular/forms/signals';
import { SELECT_OPTION_NONE } from '../../constants/string.constants';
import {
    defaultItemStatBoostForPokemon,
    ItemStatBoostForPokemon,
} from '../../models/api/item-stat-boost-pokemon';
import { defaultNature, Nature } from '../../models/api/nature';
import { defaultPokemonVariety, PokemonVariety } from '../../models/api/pokemon';

export interface StatConfiguration {
    pokemon: PokemonVariety;
    baseStats: Stats;
    level: number;
    nature: Nature;
    ability: AbilityStatBoost;
    item: ItemStatBoostForPokemon;
    ev: Stats;
    iv: Stats;
    statModifiers: Stats;
    fieldBoosts: FieldBoost[];
}

export interface Stats {
    attack: number;
    specialAttack: number;
    defense: number;
    specialDefense: number;
    speed: number;
    hitPoints: number;
}

export interface AbilityStatBoost {
    name: string;
    effect: string;

    attackBoost: number;
    specialAttackBoost: number;
    defenseBoost: number;
    specialDefenseBoost: number;
    speedBoost: number;
    hitPointsBoost: number;
    boostConditions: string;
}

export interface FieldBoost {
    name: string;
    attackBoost: number;
    specialAttackBoost: number;
    defenseBoost: number;
    specialDefenseBoost: number;
    speedBoost: number;
    hitPointsBoost: number;
    active: boolean;
}

export class EffectiveStats {
    baseStatsExtent = defaultStats;
    baseStatsSubtotal = defaultStats;

    ivExtent = defaultStats;
    ivSubTotal = defaultStats;

    evExtent = defaultStats;
    evSubTotal = defaultStats;

    natureExtent = defaultStats;
    natureSubTotal = defaultStats;

    abilityExtent = defaultStats;
    abilitySubTotal = defaultStats;

    itemExtent = defaultStats;
    itemSubTotal = defaultStats;

    statModifierExtent = defaultStats;
    statModifierSubTotal = defaultStats;

    fieldBoostsExtent = defaultStats;
    fieldBoostsSubTotal = defaultStats;

    total = defaultStats;
}

export const defaultStats: Stats = {
    attack: 0,
    specialAttack: 0,
    defense: 0,
    specialDefense: 0,
    speed: 0,
    hitPoints: 0,
};

export const defaultAbilityStatBoost: AbilityStatBoost = {
    name: SELECT_OPTION_NONE,
    effect: '',

    attackBoost: 1,
    specialAttackBoost: 1,
    defenseBoost: 1,
    specialDefenseBoost: 1,
    speedBoost: 1,
    hitPointsBoost: 1,
    boostConditions: '',
};

export const defaultStatConfiguration: StatConfiguration = {
    pokemon: defaultPokemonVariety,
    baseStats: defaultStats,
    level: 100,
    nature: defaultNature,
    ability: defaultAbilityStatBoost,
    item: defaultItemStatBoostForPokemon,
    ev: defaultStats,
    iv: defaultStats,
    statModifiers: defaultStats,
    fieldBoosts: [],
};

export const statConfigurationSchema = schema<StatConfiguration>((rootPath) => {
    min(rootPath.level, 0);
    max(rootPath.level, 999);

    min(rootPath.ev.attack, 0);
    min(rootPath.ev.specialAttack, 0);
    min(rootPath.ev.defense, 0);
    min(rootPath.ev.specialDefense, 0);
    min(rootPath.ev.speed, 0);
    min(rootPath.ev.hitPoints, 0);

    max(rootPath.ev.attack, 252);
    max(rootPath.ev.specialAttack, 252);
    max(rootPath.ev.defense, 252);
    max(rootPath.ev.specialDefense, 252);
    max(rootPath.ev.speed, 252);
    max(rootPath.ev.hitPoints, 252);

    min(rootPath.iv.attack, 0);
    min(rootPath.iv.specialAttack, 0);
    min(rootPath.iv.defense, 0);
    min(rootPath.iv.specialDefense, 0);
    min(rootPath.iv.speed, 0);
    min(rootPath.iv.hitPoints, 0);

    max(rootPath.iv.attack, 31);
    max(rootPath.iv.specialAttack, 31);
    max(rootPath.iv.defense, 31);
    max(rootPath.iv.specialDefense, 31);
    max(rootPath.iv.speed, 31);
    max(rootPath.iv.hitPoints, 31);
});
