import { IItemStatBoostModel } from './item-stat-boost.model';
import { INatureModel } from './nature.model';
import { IBasicPokemonVarietyModel, BasicPokemonVarietyModel } from './basic-pokemon-variety.model';
import { IStatsModel, StatsModel } from './stats.model';
import { IAbilityModel } from './ability.model';
import { IFieldBoostModel } from './field-boost.model';

export interface IStatsConfigurationModel {
	pokemon: IBasicPokemonVarietyModel;
	baseStats: IStatsModel;
	level: number;
	nature: INatureModel | null;
	ability: IAbilityModel | null;
	item: IItemStatBoostModel | null;
	ev: IStatsModel;
	iv: IStatsModel;
	statModifiers: IStatsModel;
	fieldBoosts: IFieldBoostModel[];
}

export class StatsConfigurationModel implements IStatsConfigurationModel {
	pokemon = new BasicPokemonVarietyModel();
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
