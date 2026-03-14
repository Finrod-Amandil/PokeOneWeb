import { Injectable } from '@angular/core';
import { defaultStats, EffectiveStats, StatConfiguration, Stats } from './stat-configuration.model';

const NATURE_BOOST = 0.1;

@Injectable({
    providedIn: 'root',
})
export class StatsService {
    getEffectiveStats(model: StatConfiguration): EffectiveStats {
        let stats = new EffectiveStats();

        if (isNaN(model.level) || model.level < 1) {
            model.level = 1;
        }

        stats.baseStatsExtent = this.getBaseStatsExtent(model);
        stats.baseStatsSubtotal = this.getBaseStatsSubTotal(stats.baseStatsExtent);

        stats.ivExtent = this.getIvExtent(model);
        stats.ivSubTotal = this.getIvSubTotal(stats.baseStatsSubtotal, stats.ivExtent);

        stats.evExtent = this.getEvExtent(model);
        stats.evSubTotal = this.getEvSubTotal(stats.ivSubTotal, stats.evExtent);

        stats.natureExtent = this.getNatureExtent(model, stats.evSubTotal);
        stats.natureSubTotal = this.getNatureSubTotal(stats.evSubTotal, stats.natureExtent);

        stats.abilityExtent = this.getAbilityExtent(model, stats.natureSubTotal);
        stats.abilitySubTotal = this.getAbilitySubTotal(stats.natureSubTotal, stats.abilityExtent);

        stats.itemExtent = this.getItemExtent(model, stats.abilitySubTotal);
        stats.itemSubTotal = this.getItemSubTotal(stats.abilitySubTotal, stats.itemExtent);

        stats.statModifierExtent = this.getStatModifiersExtent(model, stats.itemSubTotal);
        stats.statModifierSubTotal = this.getStatModifiersSubTotal(
            stats.itemSubTotal,
            stats.statModifierExtent,
        );

        stats.fieldBoostsExtent = this.getFieldBoostsExtent(model, stats.statModifierSubTotal);
        stats.fieldBoostsSubTotal = this.getFieldBoostsSubTotal(
            stats.statModifierSubTotal,
            stats.fieldBoostsExtent,
        );

        stats.total = stats.fieldBoostsSubTotal;

        stats = this.applyExceptions(stats, model);

        return stats;
    }

    getStatTotal(stats: Stats) {
        return (
            stats.attack +
            stats.defense +
            stats.specialAttack +
            stats.specialDefense +
            stats.speed +
            stats.hitPoints
        );
    }

    private getBaseStatsExtent(model: StatConfiguration): Stats {
        return {
            attack: this.getBaseStatPartialValue(model.baseStats.attack, model.level),
            specialAttack: this.getBaseStatPartialValue(model.baseStats.specialAttack, model.level),
            defense: this.getBaseStatPartialValue(model.baseStats.defense, model.level),
            specialDefense: this.getBaseStatPartialValue(
                model.baseStats.specialDefense,
                model.level,
            ),
            speed: this.getBaseStatPartialValue(model.baseStats.speed, model.level),
            hitPoints: this.getHpBaseStatPartialValue(model.baseStats.hitPoints, model.level),
        };
    }

    private getIvExtent(model: StatConfiguration): Stats {
        return {
            attack: this.getIvPartialValue(model.iv.attack, model.level),
            specialAttack: this.getIvPartialValue(model.iv.specialAttack, model.level),
            defense: this.getIvPartialValue(model.iv.defense, model.level),
            specialDefense: this.getIvPartialValue(model.iv.specialDefense, model.level),
            speed: this.getIvPartialValue(model.iv.speed, model.level),
            hitPoints: this.getIvPartialValue(model.iv.hitPoints, model.level),
        };
    }

    private getEvExtent(model: StatConfiguration): Stats {
        return {
            attack: this.getEvPartialValue(model.ev.attack, model.level),
            specialAttack: this.getEvPartialValue(model.ev.specialAttack, model.level),
            defense: this.getEvPartialValue(model.ev.defense, model.level),
            specialDefense: this.getEvPartialValue(model.ev.specialDefense, model.level),
            speed: this.getEvPartialValue(model.ev.speed, model.level),
            hitPoints: this.getEvPartialValue(model.ev.hitPoints, model.level),
        };
    }

    private getNatureExtent(model: StatConfiguration, subTotal: Stats): Stats {
        if (!model.nature) {
            return defaultStats;
        }

        return {
            attack: this.getNaturePartialValue(model.nature.attack, subTotal.attack),
            specialAttack: this.getNaturePartialValue(
                model.nature.specialAttack,
                subTotal.specialAttack,
            ),
            defense: this.getNaturePartialValue(model.nature.defense, subTotal.defense),
            specialDefense: this.getNaturePartialValue(
                model.nature.specialDefense,
                subTotal.specialDefense,
            ),
            speed: this.getNaturePartialValue(model.nature.speed, subTotal.speed),
            hitPoints: 0,
        };
    }

    private getAbilityExtent(model: StatConfiguration, subTotal: Stats): Stats {
        if (!model.ability) {
            return defaultStats;
        }

        return {
            attack: this.getAbilityPartialValue(model.ability.attackBoost, subTotal.attack),
            specialAttack: this.getAbilityPartialValue(
                model.ability.specialAttackBoost,
                subTotal.specialAttack,
            ),
            defense: this.getAbilityPartialValue(model.ability.defenseBoost, subTotal.defense),
            specialDefense: this.getAbilityPartialValue(
                model.ability.specialDefenseBoost,
                subTotal.specialDefense,
            ),
            speed: this.getAbilityPartialValue(model.ability.speedBoost, subTotal.speed),
            hitPoints: 0,
        };
    }

    private getItemExtent(model: StatConfiguration, subTotal: Stats): Stats {
        if (!model.item) {
            return defaultStats;
        }

        return {
            attack: this.getItemPartialValue(model.item.attackBoost, subTotal.attack),
            specialAttack: this.getItemPartialValue(
                model.item.specialAttackBoost,
                subTotal.specialAttack,
            ),
            defense: this.getItemPartialValue(model.item.defenseBoost, subTotal.defense),
            specialDefense: this.getItemPartialValue(
                model.item.specialDefenseBoost,
                subTotal.specialDefense,
            ),
            speed: this.getItemPartialValue(model.item.speedBoost, subTotal.speed),
            hitPoints: 0,
        };
    }

    private getStatModifiersExtent(model: StatConfiguration, subTotal: Stats): Stats {
        return {
            attack: this.getStatModifierPartialValue(model.statModifiers.attack, subTotal.attack),
            specialAttack: this.getStatModifierPartialValue(
                model.statModifiers.specialAttack,
                subTotal.specialAttack,
            ),
            defense: this.getStatModifierPartialValue(
                model.statModifiers.defense,
                subTotal.defense,
            ),
            specialDefense: this.getStatModifierPartialValue(
                model.statModifiers.specialDefense,
                subTotal.specialDefense,
            ),
            speed: this.getStatModifierPartialValue(model.statModifiers.speed, subTotal.speed),
            hitPoints: 0,
        };
    }

    private getFieldBoostsExtent(model: StatConfiguration, subTotal: Stats): Stats {
        const totalFactors: Stats = {
            attack: 1,
            specialAttack: 1,
            defense: 1,
            specialDefense: 1,
            speed: 1,
            hitPoints: 1,
        };

        for (const fieldBoost of model.fieldBoosts.filter((fb) => fb.active)) {
            totalFactors.attack *= fieldBoost.attackBoost;
            totalFactors.specialAttack *= fieldBoost.specialAttackBoost;
            totalFactors.defense *= fieldBoost.defenseBoost;
            totalFactors.specialDefense *= fieldBoost.specialDefenseBoost;
            totalFactors.speed *= fieldBoost.speedBoost;
            totalFactors.hitPoints *= fieldBoost.hitPointsBoost;
        }

        return {
            attack: this.getFieldBoostPartialValue(totalFactors.attack, subTotal.attack),
            specialAttack: this.getFieldBoostPartialValue(
                totalFactors.specialAttack,
                subTotal.specialAttack,
            ),
            defense: this.getFieldBoostPartialValue(totalFactors.defense, subTotal.defense),
            specialDefense: this.getFieldBoostPartialValue(
                totalFactors.specialDefense,
                subTotal.specialDefense,
            ),
            speed: this.getFieldBoostPartialValue(totalFactors.speed, subTotal.speed),
            hitPoints: this.getFieldBoostPartialValue(totalFactors.hitPoints, subTotal.hitPoints),
        };
    }

    private getBaseStatsSubTotal(baseStatsExtent: Stats): Stats {
        return {
            attack: baseStatsExtent.attack,
            specialAttack: baseStatsExtent.specialAttack,
            defense: baseStatsExtent.defense,
            specialDefense: baseStatsExtent.specialDefense,
            speed: baseStatsExtent.speed,
            hitPoints: baseStatsExtent.hitPoints,
        };
    }

    private getIvSubTotal(subTotal: Stats, ivExtent: Stats): Stats {
        return {
            attack: subTotal.attack + ivExtent.attack,
            specialAttack: subTotal.specialAttack + ivExtent.specialAttack,
            defense: subTotal.defense + ivExtent.defense,
            specialDefense: subTotal.specialDefense + ivExtent.specialDefense,
            speed: subTotal.speed + ivExtent.speed,
            hitPoints: subTotal.hitPoints + ivExtent.hitPoints,
        };
    }

    private getEvSubTotal(subTotal: Stats, evExtent: Stats): Stats {
        return {
            attack: Math.floor(subTotal.attack + evExtent.attack),
            specialAttack: Math.floor(subTotal.specialAttack + evExtent.specialAttack),
            defense: Math.floor(subTotal.defense + evExtent.defense),
            specialDefense: Math.floor(subTotal.specialDefense + evExtent.specialDefense),
            speed: Math.floor(subTotal.speed + evExtent.speed),
            hitPoints: Math.floor(subTotal.hitPoints + evExtent.hitPoints),
        };
    }

    private getNatureSubTotal(subTotal: Stats, natureExtent: Stats): Stats {
        return {
            attack: Math.floor(subTotal.attack + natureExtent.attack),
            specialAttack: Math.floor(subTotal.specialAttack + natureExtent.specialAttack),
            defense: Math.floor(subTotal.defense + natureExtent.defense),
            specialDefense: Math.floor(subTotal.specialDefense + natureExtent.specialDefense),
            speed: Math.floor(subTotal.speed + natureExtent.speed),
            hitPoints: Math.floor(subTotal.hitPoints + natureExtent.hitPoints),
        };
    }

    private getAbilitySubTotal(subTotal: Stats, abilityExtent: Stats): Stats {
        return {
            attack: Math.floor(subTotal.attack + abilityExtent.attack),
            specialAttack: Math.floor(subTotal.specialAttack + abilityExtent.specialAttack),
            defense: Math.floor(subTotal.defense + abilityExtent.defense),
            specialDefense: Math.floor(subTotal.specialDefense + abilityExtent.specialDefense),
            speed: Math.floor(subTotal.speed + abilityExtent.speed),
            hitPoints: Math.floor(subTotal.hitPoints + abilityExtent.hitPoints),
        };
    }

    private getItemSubTotal(subTotal: Stats, itemExtent: Stats): Stats {
        return {
            attack: Math.floor(subTotal.attack + itemExtent.attack),
            specialAttack: Math.floor(subTotal.specialAttack + itemExtent.specialAttack),
            defense: Math.floor(subTotal.defense + itemExtent.defense),
            specialDefense: Math.floor(subTotal.specialDefense + itemExtent.specialDefense),
            speed: Math.floor(subTotal.speed + itemExtent.speed),
            hitPoints: Math.floor(subTotal.hitPoints + itemExtent.hitPoints),
        };
    }

    private getStatModifiersSubTotal(subTotal: Stats, statModifiersExtent: Stats): Stats {
        return {
            attack: Math.floor(subTotal.attack + statModifiersExtent.attack),
            specialAttack: Math.floor(subTotal.specialAttack + statModifiersExtent.specialAttack),
            defense: Math.floor(subTotal.defense + statModifiersExtent.defense),
            specialDefense: Math.floor(
                subTotal.specialDefense + statModifiersExtent.specialDefense,
            ),
            speed: Math.floor(subTotal.speed + statModifiersExtent.speed),
            hitPoints: Math.floor(subTotal.hitPoints + statModifiersExtent.hitPoints),
        };
    }

    private getFieldBoostsSubTotal(subTotal: Stats, fieldBoostsExtent: Stats): Stats {
        return {
            attack: Math.floor(subTotal.attack + fieldBoostsExtent.attack),
            specialAttack: Math.floor(subTotal.specialAttack + fieldBoostsExtent.specialAttack),
            defense: Math.floor(subTotal.defense + fieldBoostsExtent.defense),
            specialDefense: Math.floor(subTotal.specialDefense + fieldBoostsExtent.specialDefense),
            speed: Math.floor(subTotal.speed + fieldBoostsExtent.speed),
            hitPoints: Math.floor(subTotal.hitPoints + fieldBoostsExtent.hitPoints),
        };
    }

    private getBaseStatPartialValue(baseStatValue: number, level: number): number {
        return (2.0 * baseStatValue * level) / 100.0 + 5;
    }

    private getHpBaseStatPartialValue(baseStatValue: number, level: number): number {
        return (2.0 * baseStatValue * level) / 100.0 + level + 10;
    }

    private getIvPartialValue(ivValue: number, level: number): number {
        return (ivValue * level) / 100.0;
    }

    private getEvPartialValue(evValue: number, level: number): number {
        return (Math.floor(evValue / 4.0) * level) / 100.0;
    }

    private getNaturePartialValue(natureInfluence: number, subTotal: number): number {
        return natureInfluence * NATURE_BOOST * Math.floor(subTotal);
    }

    private getItemPartialValue(itemBoost: number, subTotal: number): number {
        return (itemBoost - 1) * subTotal;
    }

    private getAbilityPartialValue(abilityBoost: number, subTotal: number): number {
        return (abilityBoost - 1) * subTotal;
    }

    private getStatModifierPartialValue(statModifierStage: number, subTotal: number): number {
        return (this.getFactorForStatModifier(statModifierStage) - 1) * subTotal;
    }

    private getFactorForStatModifier(stage: number): number {
        stage = stage > 6 ? 6 : stage;
        stage = stage < -6 ? -6 : stage;

        return stage >= 0 ? (2.0 + stage) / 2.0 : 2.0 / (2.0 - stage);
    }

    private getFieldBoostPartialValue(totalFieldBoostFactor: number, subTotal: number): number {
        return Math.floor((totalFieldBoostFactor - 1) * subTotal);
    }

    private applyExceptions(stats: EffectiveStats, model: StatConfiguration): EffectiveStats {
        if (model.pokemon.resourceName === 'shedinja') {
            stats.baseStatsExtent.hitPoints = 1;
            stats.baseStatsSubtotal.hitPoints = 1;
            stats.ivExtent.hitPoints = 0;
            stats.ivSubTotal.hitPoints = 1;
            stats.evExtent.hitPoints = 0;
            stats.evSubTotal.hitPoints = 1;
            stats.natureExtent.hitPoints = 0;
            stats.natureSubTotal.hitPoints = 1;
            stats.abilityExtent.hitPoints = 0;
            stats.abilitySubTotal.hitPoints = 1;
            stats.itemExtent.hitPoints = 0;
            stats.itemSubTotal.hitPoints = 1;
            stats.statModifierExtent.hitPoints = 0;
            stats.statModifierSubTotal.hitPoints = 1;
            stats.fieldBoostsExtent.hitPoints = 0;
            stats.fieldBoostsSubTotal.hitPoints = 0;
            stats.total.hitPoints = 1;
        }

        return stats;
    }
}
