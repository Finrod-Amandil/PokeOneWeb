import { IEffectiveStatsModel, EffectiveStatsModel } from 'src/app/core/models/service/effective-stats.model';
import {
    IStatsConfigurationModel,
    StatsConfigurationModel
} from 'src/app/core/models/service/stats-configuration.model';
import { BarChartGroupModel } from './bar-chart-group.model';

export class AdvancedStatChartModel {
    barChartGroups: BarChartGroupModel[] = [];
    statsConfig: IStatsConfigurationModel = new StatsConfigurationModel();
    effectiveStats: IEffectiveStatsModel = new EffectiveStatsModel();
    maxExtent = 1;
}
