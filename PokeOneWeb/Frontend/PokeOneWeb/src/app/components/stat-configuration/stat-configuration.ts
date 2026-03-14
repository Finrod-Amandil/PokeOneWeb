import { Component, computed, inject, input, signal } from '@angular/core';
import { SELECT_OPTION_NONE } from '../../constants/string.constants';
import { PokemonVariety } from '../../models/api/pokemon';
import { Bar, BarChartGroup } from './bar-chart-group.model';
import {
    defaultStatConfiguration,
    EffectiveStats,
    StatConfiguration,
    Stats,
} from './stat-configuration.model';
import { StatInputComponent } from './stat-input/stat-input';
import { StatsService } from './stats.service';

@Component({
    selector: 'pokeoneweb-stat-configuration',
    imports: [StatInputComponent],
    templateUrl: './stat-configuration.html',
    styleUrl: './stat-configuration.scss',
})
export class StatConfigurationComponent {
    pokemon = input.required<PokemonVariety>();

    private statsService = inject(StatsService);

    readonly viewPortExtent = 1000;
    readonly selectOptionNone = SELECT_OPTION_NONE;

    statsConfiguration = signal(defaultStatConfiguration);
    effectiveStats = computed(() => this.statsService.getEffectiveStats(this.statsConfiguration()));
    maxExtent = computed(() => this.getMaxTotalExtent(this.effectiveStats()));
    barChartGroups = computed(() =>
        this.calculateBarChart(this.statsConfiguration(), this.effectiveStats(), this.maxExtent()),
    );

    onStatChanged(statsConfiguration: StatConfiguration) {
        this.statsConfiguration.set(statsConfiguration);
    }

    getStatTotal(stats: Stats): number {
        return this.statsService.getStatTotal(stats);
    }

    private calculateBarChart(
        statsConfig: StatConfiguration,
        effectiveStats: EffectiveStats,
        maxExtent: number,
    ) {
        const groups: BarChartGroup[] = [];

        let i = 0;
        while (i < 6) {
            const group = new BarChartGroup();
            group.label = this.getBarChartGroupLabel(i);
            group.className = this.getBarChartGroupClassName(i);
            group.baseStatValue = this.getValueForStatIndex(statsConfig.baseStats, i);
            group.totalValue = this.getValueForStatIndex(effectiveStats.total, i);
            group.xTotal = this.getScaledWidth(
                this.getValueForStatIndex(effectiveStats.total, i),
                maxExtent,
            );

            group.bars.push(
                this.getBar(
                    'Base Stats',
                    'base',
                    0,
                    this.getValueForStatIndex(effectiveStats.baseStatsExtent, i),
                    maxExtent,
                ),
            );

            group.bars.push(
                this.getBar(
                    'IV',
                    'iv',
                    this.getValueForStatIndex(effectiveStats.baseStatsSubtotal, i),
                    this.getValueForStatIndex(effectiveStats.ivExtent, i),
                    maxExtent,
                ),
            );

            group.bars.push(
                this.getBar(
                    'EV',
                    'ev',
                    this.getValueForStatIndex(effectiveStats.ivSubTotal, i),
                    this.getValueForStatIndex(effectiveStats.evExtent, i),
                    maxExtent,
                ),
            );

            group.bars.push(
                this.getBar(
                    'Nature',
                    'nature',
                    this.getValueForStatIndex(effectiveStats.evSubTotal, i),
                    this.getValueForStatIndex(effectiveStats.natureExtent, i),
                    maxExtent,
                ),
            );

            group.bars.push(
                this.getBar(
                    'Ability',
                    'ability',
                    this.getValueForStatIndex(effectiveStats.natureSubTotal, i),
                    this.getValueForStatIndex(effectiveStats.abilityExtent, i),
                    maxExtent,
                ),
            );

            group.bars.push(
                this.getBar(
                    'Item',
                    'item',
                    this.getValueForStatIndex(effectiveStats.abilitySubTotal, i),
                    this.getValueForStatIndex(effectiveStats.itemExtent, i),
                    maxExtent,
                ),
            );

            group.bars.push(
                this.getBar(
                    'Stat modifiers',
                    'modifier',
                    this.getValueForStatIndex(effectiveStats.itemSubTotal, i),
                    this.getValueForStatIndex(effectiveStats.statModifierExtent, i),
                    maxExtent,
                ),
            );

            group.bars.push(
                this.getBar(
                    'Active field boosts',
                    'field',
                    this.getValueForStatIndex(effectiveStats.statModifierSubTotal, i),
                    this.getValueForStatIndex(effectiveStats.fieldBoostsExtent, i),
                    maxExtent,
                ),
            );

            group.bars = group.bars.sort(
                (b1, b2) => +b1.isNegativeDirection - +b2.isNegativeDirection,
            );

            groups.push(group);
            i++;
        }

        return groups;
    }

    private getBar(
        label: string,
        className: string,
        start: number,
        extent: number,
        maxExtent: number,
    ): Bar {
        if (extent > 0) {
            return {
                label: label,
                className: className,
                isNegativeDirection: false,
                x: this.getBarX(start, maxExtent),
                y: 9,
                width: this.getScaledWidth(extent, maxExtent),
                height: 20,
            };
        } else {
            return {
                label: label,
                className: className,
                isNegativeDirection: true,
                x: this.getBarX(start, maxExtent) - this.getScaledWidth(-extent, maxExtent),
                y: 14,
                width: this.getScaledWidth(-extent, maxExtent),
                height: 10,
            };
        }
    }

    private getBarX(value: number, maxExtent: number): number {
        const max = maxExtent;
        return ((this.viewPortExtent * 1.0) / max) * value;
    }

    private getScaledWidth(width: number, maxExtent: number): number {
        return ((this.viewPortExtent * 1.0) / maxExtent) * width;
    }

    private getMaxTotalExtent(effectiveStats: EffectiveStats): number {
        return Math.max(
            this.getMaxStat(effectiveStats.total),
            this.getMaxStat(effectiveStats.evSubTotal),
            this.getMaxStat(effectiveStats.natureSubTotal),
            this.getMaxStat(effectiveStats.abilitySubTotal),
            this.getMaxStat(effectiveStats.itemSubTotal),
            this.getMaxStat(effectiveStats.statModifierSubTotal),
        );
    }

    private getMaxStat(stats: Stats): number {
        return [0, 1, 2, 3, 4, 5]
            .map((i) => this.getValueForStatIndex(stats, i))
            .reduce((max, val) => (max > val ? max : val));
    }

    private getBarChartGroupLabel(statIndex: number): string {
        return ['Hit Points', 'Attack', 'Defense', 'Special Attack', 'Special Defense', 'Speed'][
            statIndex
        ];
    }

    private getBarChartGroupClassName(statIndex: number): string {
        return ['hp', 'atk', 'def', 'spa', 'spd', 'spe'][statIndex];
    }

    private getValueForStatIndex(stat: Stats, statIndex: number): number {
        return [
            stat.hitPoints,
            stat.attack,
            stat.defense,
            stat.specialAttack,
            stat.specialDefense,
            stat.speed,
        ][statIndex];
    }
}
