import { IItemStatBoostModel } from './item-stat-boost.model';
import { INatureModel } from './nature.model';
import { IBasicPokemonVarietyModel, BasicPokemonVarietyModel } from './basic-pokemon-variety.model';
import { IStatsModel, StatsModel } from './stats.model';

export interface IStatsConfigurationModel {
    pokemon: IBasicPokemonVarietyModel,
    baseStats: IStatsModel,
    level: number,
    nature: INatureModel | null,
    ev: IStatsModel,
    iv: IStatsModel,
    item: IItemStatBoostModel | null,
}

export class StatsConfigurationModel implements IStatsConfigurationModel {
    pokemon = new BasicPokemonVarietyModel();
    baseStats = new StatsModel();
    level = 100;
    nature = null;
    ev = new StatsModel();
    iv = new StatsModel();
    item = null;
}