import { IStatsModel, StatsModel } from './stats.model';

export interface IEffectiveStatsModel {
	baseStatsExtent: IStatsModel;
	baseStatsSubtotal: IStatsModel;

	ivExtent: IStatsModel;
	ivSubTotal: IStatsModel;

	evExtent: IStatsModel;
	evSubTotal: IStatsModel;

	natureExtent: IStatsModel;
	natureSubTotal: IStatsModel;

	abilityExtent: IStatsModel;
	abilitySubTotal: IStatsModel;

	itemExtent: IStatsModel;
	itemSubTotal: IStatsModel;

	statModifierExtent: IStatsModel;
	statModifierSubTotal: IStatsModel;

	fieldBoostsExtent: IStatsModel;
	fieldBoostsSubTotal: IStatsModel;

	total: IStatsModel;
}

export class EffecticeStatsModel implements IEffectiveStatsModel {
	baseStatsExtent = new StatsModel();
	baseStatsSubtotal = new StatsModel();

	ivExtent = new StatsModel();
	ivSubTotal = new StatsModel();

	evExtent = new StatsModel();
	evSubTotal = new StatsModel();

	natureExtent = new StatsModel();
	natureSubTotal = new StatsModel();

	abilityExtent = new StatsModel();
	abilitySubTotal = new StatsModel();

	itemExtent = new StatsModel();
	itemSubTotal = new StatsModel();

	statModifierExtent = new StatsModel();
	statModifierSubTotal = new StatsModel();

	fieldBoostsExtent = new StatsModel();
	fieldBoostsSubTotal = new StatsModel();

	total = new StatsModel();
}
