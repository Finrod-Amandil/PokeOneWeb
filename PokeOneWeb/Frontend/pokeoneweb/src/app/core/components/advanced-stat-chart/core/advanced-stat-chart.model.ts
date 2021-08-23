import { IEffectiveStatsModel, EffecticeStatsModel } from "src/app/core/models/effective-stats.model";
import { IStatsConfigurationModel, StatsConfigurationModel } from "src/app/core/models/stats-configuration.model";
import { BarChartGroupModel } from "./bar-chart-group.model";

export class AdvancedStatChartModel {
    barChartGroups: BarChartGroupModel[] = [];
    statsConfig: IStatsConfigurationModel = new StatsConfigurationModel();
    effectiveStats: IEffectiveStatsModel = new EffecticeStatsModel();
    maxExtent: number = 1;
}