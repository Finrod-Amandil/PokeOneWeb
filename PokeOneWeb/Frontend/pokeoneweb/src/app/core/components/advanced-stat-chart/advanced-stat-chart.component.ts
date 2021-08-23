import { Component, Input, OnInit } from '@angular/core';
import { IPokemonVarietyModel, PokemonVarietyModel } from '../../models/pokemon-variety.model';
import { IStatsConfigurationModel } from '../../models/stats-configuration.model';
import { IStatsModel } from '../../models/stats.model';
import { StatsService } from '../../services/stats.service';
import { AdvancedStatChartModel } from './core/advanced-stat-chart.model';
import { BarChartGroupModel } from './core/bar-chart-group.model';
import { BarModel } from './core/bar.model';

const VIEWPORT_EXTENT: number = 1000;

@Component({
    selector: 'pokeone-advanced-stat-chart',
    templateUrl: './advanced-stat-chart.component.html',
    styleUrls: ['./advanced-stat-chart.component.scss']
})
export class AdvancedStatChartComponent implements OnInit {

    @Input() pokemon: IPokemonVarietyModel = new PokemonVarietyModel();

    public model: AdvancedStatChartModel = new AdvancedStatChartModel();

    public viewPortExtent = VIEWPORT_EXTENT;

    constructor(private statsService: StatsService) { }

    ngOnInit(): void { }

    public onStatChanged(statsConfiguration: IStatsConfigurationModel) {
        this.model.statsConfig = statsConfiguration;
        this.model.effectiveStats = this.statsService.getEffectiveStats(this.model.statsConfig);

        this.calculateBarChart();
    }

    public getStatTotal(stats: IStatsModel): number {
        return stats.atk + stats.spa + stats.def + stats.spd + stats.spe + stats.hp;
    }

    private calculateBarChart() {
        this.model.maxExtent = this.getMaxTotalExtent();

        let groups: BarChartGroupModel[] = [];

        let i = 0;
        while (i < 6) {
            let group = new BarChartGroupModel();
            group.label = this.getBarChartGroupLabel(i);
            group.className = this.getBarChartGroupClassName(i);
            group.baseStatValue = this.getValueForStatIndex(this.model.statsConfig.baseStats, i);
            group.totalValue = this.getValueForStatIndex(this.model.effectiveStats.total, i);
            group.xTotal = this.getScaledWidth(this.getValueForStatIndex(this.model.effectiveStats.total, i));

            group.bars.push(this.getBar(
                "Base Stats",
                "base",
                0, 
                this.getValueForStatIndex(this.model.effectiveStats.baseStatsExtent, i)
            ));

            group.bars.push(this.getBar(
                "IV", 
                "iv",
                this.getValueForStatIndex(this.model.effectiveStats.baseStatsSubtotal, i), 
                this.getValueForStatIndex(this.model.effectiveStats.ivExtent, i)
            ));

            group.bars.push(this.getBar(
                "EV", 
                "ev",
                this.getValueForStatIndex(this.model.effectiveStats.ivSubTotal, i), 
                this.getValueForStatIndex(this.model.effectiveStats.evExtent, i)
            ));

            group.bars.push(this.getBar(
                "Nature", 
                "nature",
                this.getValueForStatIndex(this.model.effectiveStats.evSubTotal, i), 
                this.getValueForStatIndex(this.model.effectiveStats.natureExtent, i)
            ));

            group.bars.push(this.getBar(
                "Ability", 
                "ability",
                this.getValueForStatIndex(this.model.effectiveStats.natureSubTotal, i), 
                this.getValueForStatIndex(this.model.effectiveStats.abilityExtent, i)
            ));

            group.bars.push(this.getBar(
                "Item", 
                "item",
                this.getValueForStatIndex(this.model.effectiveStats.abilitySubTotal, i), 
                this.getValueForStatIndex(this.model.effectiveStats.itemExtent, i)
            ));
            
            group.bars.push(this.getBar(
                "Stat modifiers", 
                "modifier",
                this.getValueForStatIndex(this.model.effectiveStats.itemSubTotal, i), 
                this.getValueForStatIndex(this.model.effectiveStats.statModifierExtent, i)
            ));

            group.bars.push(this.getBar(
                "Active field boosts", 
                "field",
                this.getValueForStatIndex(this.model.effectiveStats.statModifierSubTotal, i), 
                this.getValueForStatIndex(this.model.effectiveStats.fieldBoostsExtent, i)
            ));

            group.bars = group.bars.sort((b1, b2) => +b1.isNegativeDirection - +b2.isNegativeDirection);

            groups.push(group);
            i++;
        }

        this.model.barChartGroups = groups;
    }

    private getBar(label: string, className: string, start: number, extent: number): BarModel {
        if (extent > 0) {
            return <BarModel> {
                label: label,
                className: className,
                isNegativeDirection: false,
                x: this.getBarX(start),
                y: 5,
                width: this.getScaledWidth(extent),
                height: 20,
            }
        }
        else {
            return <BarModel> {
                label: label,
                className: className,
                isNegativeDirection: true,
                x: this.getBarX(start) - this.getScaledWidth(-extent),
                y: 10,
                width: this.getScaledWidth(-extent),
                height: 10,
            }
        }
    }

    private getBarX(value: number): number {
        const max = this.model.maxExtent;
        return ((VIEWPORT_EXTENT * 1.0) / max) * value;
    }

    private getScaledWidth(width: number): number {
        const max = this.model.maxExtent;
        return ((VIEWPORT_EXTENT * 1.0) / max) * width;
    }

    private getMaxTotalExtent(): number {
        return Math.max(
            this.getMaxStat(this.model.effectiveStats.total),
            this.getMaxStat(this.model.effectiveStats.evSubTotal),
            this.getMaxStat(this.model.effectiveStats.natureSubTotal),
            this.getMaxStat(this.model.effectiveStats.abilitySubTotal),
            this.getMaxStat(this.model.effectiveStats.itemSubTotal),
            this.getMaxStat(this.model.effectiveStats.statModifierSubTotal)
        )
    }

    private getMaxStat(stats: IStatsModel): number {
        return [0, 1, 2, 3, 4, 5]
            .map(i => this.getValueForStatIndex(stats, i))
            .reduce((max, val) => max > val ? max : val);
    }

    private getBarChartGroupLabel(statIndex: number): string {
        return [ "Hit Points", "Attack", "Defense", "Special Attack", "Special Defense", "Speed" ][statIndex];
    }

    private getBarChartGroupClassName(statIndex: number): string {
        return [ "hp", "atk", "def", "spa", "spd", "spe" ][statIndex];
    }

    private getValueForStatIndex(stat: IStatsModel, statIndex: number): number {
        return [ stat.hp, stat.atk, stat.def, stat.spa, stat.spd, stat.spe ][statIndex];
    }
}
