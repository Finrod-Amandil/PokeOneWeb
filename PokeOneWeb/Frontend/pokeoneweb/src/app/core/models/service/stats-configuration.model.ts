import { IItemStatBoostPokemonModel } from '../api/item-stat-boost-pokemon.model';
import { INatureModel } from '../api/nature.model';
import { IPokemonVarietyListModel, PokemonVarietyListModel } from '../api/pokemon-variety.model';
import { IStatsModel, StatsModel } from './stats.model';
import { IAbilityModel } from './ability.model';
import { IFieldBoostModel } from './field-boost.model';

export interface IStatsConfigurationModel {
    pokemon: IPokemonVarietyListModel;
    baseStats: IStatsModel;
    level: number;
    nature: INatureModel | null;
    ability: IAbilityModel | null;
    item: IItemStatBoostPokemonModel | null;
    ev: IStatsModel;
    iv: IStatsModel;
    statModifiers: IStatsModel;
    fieldBoosts: IFieldBoostModel[];
}

export class StatsConfigurationModel implements IStatsConfigurationModel {
    pokemon = new PokemonVarietyListModel();
    baseStats = new StatsModel();
    level = 100;
    nature = null;
    ability = null;
    item = null;
    ev = new StatsModel();
    iv = new StatsModel();
    statModifiers = new StatsModel();
    fieldBoosts = [];
}
