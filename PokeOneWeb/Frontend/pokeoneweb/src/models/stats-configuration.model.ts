import { IItemStatBoostModel } from './item-stat-boost.model';
import { INatureModel } from './nature.model';
import { IPokemonNameModel, PokemonNameModel } from './pokemon-name.model';
import { IStatsModel, StatsModel } from './stats.model';

export interface IStatsConfigurationModel {
    pokemon: IPokemonNameModel,
    baseStats: IStatsModel,
    level: number,
    nature: INatureModel | null,
    ev: IStatsModel,
    iv: IStatsModel,
    item: IItemStatBoostModel | null,
}

export class StatsConfigurationModel implements IStatsConfigurationModel {
    pokemon = new PokemonNameModel();
    baseStats = new StatsModel();
    level = 100;
    nature = null;
    ev = new StatsModel();
    iv = new StatsModel();
    item = null;
}