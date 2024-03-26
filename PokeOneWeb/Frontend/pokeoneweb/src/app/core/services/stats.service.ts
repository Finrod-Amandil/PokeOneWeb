import { Injectable } from '@angular/core';
import { EffectiveStatsModel, IEffectiveStatsModel } from '../models/service/effective-stats.model';
import { IStatsConfigurationModel } from '../models/service/stats-configuration.model';
import { IStatsModel, StatsModel } from '../models/service/stats.model';

const NATURE_BOOST = 0.1;

@Injectable({
    providedIn: 'root'
})
export class StatsService {
    public getEffectiveStats(model: IStatsConfigurationModel): IEffectiveStatsModel {
        let stats = new EffectiveStatsModel();

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
        stats.statModifierSubTotal = this.getStatModifiersSubTotal(stats.itemSubTotal, stats.statModifierExtent);

        stats.fieldBoostsExtent = this.getFieldBoostsExtent(model, stats.statModifierSubTotal);
        stats.fieldBoostsSubTotal = this.getFieldBoostsSubTotal(stats.statModifierSubTotal, stats.fieldBoostsExtent);

        stats.total = stats.fieldBoostsSubTotal;

        stats = this.applyExceptions(stats, model);

        return stats;
    }

    private getBaseStatsExtent(model: IStatsConfigurationModel): IStatsModel {
        return <IStatsModel>{
            atk: this.getBaseStatPartialValue(model.baseStats.atk, model.level),
            spa: this.getBaseStatPartialValue(model.baseStats.spa, model.level),
            def: this.getBaseStatPartialValue(model.baseStats.def, model.level),
            spd: this.getBaseStatPartialValue(model.baseStats.spd, model.level),
            spe: this.getBaseStatPartialValue(model.baseStats.spe, model.level),
            hp: this.getHpBaseStatPartialValue(model.baseStats.hp, model.level)
        };
    }

    private getIvExtent(model: IStatsConfigurationModel): IStatsModel {
        return <IStatsModel>{
            atk: this.getIvPartialValue(model.iv.atk, model.level),
            spa: this.getIvPartialValue(model.iv.spa, model.level),
            def: this.getIvPartialValue(model.iv.def, model.level),
            spd: this.getIvPartialValue(model.iv.spd, model.level),
            spe: this.getIvPartialValue(model.iv.spe, model.level),
            hp: this.getIvPartialValue(model.iv.hp, model.level)
        };
    }

    private getEvExtent(model: IStatsConfigurationModel): IStatsModel {
        return <IStatsModel>{
            atk: this.getEvPartialValue(model.ev.atk, model.level),
            spa: this.getEvPartialValue(model.ev.spa, model.level),
            def: this.getEvPartialValue(model.ev.def, model.level),
            spd: this.getEvPartialValue(model.ev.spd, model.level),
            spe: this.getEvPartialValue(model.ev.spe, model.level),
            hp: this.getEvPartialValue(model.ev.hp, model.level)
        };
    }

    private getNatureExtent(model: IStatsConfigurationModel, subTotal: IStatsModel): IStatsModel {
        if (!model.nature) {
            return new StatsModel();
        }

        return <IStatsModel>{
            atk: this.getNaturePartialValue(model.nature.attack, subTotal.atk),
            spa: this.getNaturePartialValue(model.nature.specialAttack, subTotal.spa),
            def: this.getNaturePartialValue(model.nature.defense, subTotal.def),
            spd: this.getNaturePartialValue(model.nature.specialDefense, subTotal.spd),
            spe: this.getNaturePartialValue(model.nature.speed, subTotal.spe),
            hp: 0
        };
    }

    private getAbilityExtent(model: IStatsConfigurationModel, subTotal: IStatsModel): IStatsModel {
        if (!model.ability) {
            return new StatsModel();
        }

        return <IStatsModel>{
            atk: this.getAbilityPartialValue(model.ability.attackBoost, subTotal.atk),
            spa: this.getAbilityPartialValue(model.ability.specialAttackBoost, subTotal.spa),
            def: this.getAbilityPartialValue(model.ability.defenseBoost, subTotal.def),
            spd: this.getAbilityPartialValue(model.ability.specialDefenseBoost, subTotal.spd),
            spe: this.getAbilityPartialValue(model.ability.speedBoost, subTotal.spe),
            hp: 0
        };
    }

    private getItemExtent(model: IStatsConfigurationModel, subTotal: IStatsModel): IStatsModel {
        if (!model.item) {
            return new StatsModel();
        }

        return <IStatsModel>{
            atk: this.getItemPartialValue(model.item.attackBoost, subTotal.atk),
            spa: this.getItemPartialValue(model.item.specialAttackBoost, subTotal.spa),
            def: this.getItemPartialValue(model.item.defenseBoost, subTotal.def),
            spd: this.getItemPartialValue(model.item.specialDefenseBoost, subTotal.spd),
            spe: this.getItemPartialValue(model.item.speedBoost, subTotal.spe),
            hp: 0
        };
    }

    private getStatModifiersExtent(model: IStatsConfigurationModel, subTotal: IStatsModel): IStatsModel {
        return <IStatsModel>{
            atk: this.getStatModifierPartialValue(model.statModifiers.atk, subTotal.atk),
            spa: this.getStatModifierPartialValue(model.statModifiers.spa, subTotal.spa),
            def: this.getStatModifierPartialValue(model.statModifiers.def, subTotal.def),
            spd: this.getStatModifierPartialValue(model.statModifiers.spd, subTotal.spd),
            spe: this.getStatModifierPartialValue(model.statModifiers.spe, subTotal.spe),
            hp: 0
        };
    }

    private getFieldBoostsExtent(model: IStatsConfigurationModel, subTotal: IStatsModel): IStatsModel {
        const totalFactors: IStatsModel = <IStatsModel>{
            atk: 1,
            spa: 1,
            def: 1,
            spd: 1,
            spe: 1,
            hp: 1
        };

        for (const fieldBoost of model.fieldBoosts.filter((fb) => fb.active)) {
            totalFactors.atk *= fieldBoost.attackBoost;
            totalFactors.spa *= fieldBoost.specialAttackBoost;
            totalFactors.def *= fieldBoost.defenseBoost;
            totalFactors.spd *= fieldBoost.specialDefenseBoost;
            totalFactors.spe *= fieldBoost.speedBoost;
            totalFactors.hp *= fieldBoost.hitPointsBoost;
        }

        return <IStatsModel>{
            atk: this.getFieldBoostPartialValue(totalFactors.atk, subTotal.atk),
            spa: this.getFieldBoostPartialValue(totalFactors.spa, subTotal.spa),
            def: this.getFieldBoostPartialValue(totalFactors.def, subTotal.def),
            spd: this.getFieldBoostPartialValue(totalFactors.spd, subTotal.spd),
            spe: this.getFieldBoostPartialValue(totalFactors.spe, subTotal.spe),
            hp: this.getFieldBoostPartialValue(totalFactors.hp, subTotal.hp)
        };
    }

    private getBaseStatsSubTotal(baseStatsExtent: IStatsModel): IStatsModel {
        return <IStatsModel>{
            atk: baseStatsExtent.atk,
            spa: baseStatsExtent.spa,
            def: baseStatsExtent.def,
            spd: baseStatsExtent.spd,
            spe: baseStatsExtent.spe,
            hp: baseStatsExtent.hp
        };
    }

    private getIvSubTotal(subTotal: IStatsModel, ivExtent: IStatsModel): IStatsModel {
        return <IStatsModel>{
            atk: subTotal.atk + ivExtent.atk,
            spa: subTotal.spa + ivExtent.spa,
            def: subTotal.def + ivExtent.def,
            spd: subTotal.spd + ivExtent.spd,
            spe: subTotal.spe + ivExtent.spe,
            hp: subTotal.hp + ivExtent.hp
        };
    }

    private getEvSubTotal(subTotal: IStatsModel, evExtent: IStatsModel): IStatsModel {
        return <IStatsModel>{
            atk: Math.floor(subTotal.atk + evExtent.atk),
            spa: Math.floor(subTotal.spa + evExtent.spa),
            def: Math.floor(subTotal.def + evExtent.def),
            spd: Math.floor(subTotal.spd + evExtent.spd),
            spe: Math.floor(subTotal.spe + evExtent.spe),
            hp: Math.floor(subTotal.hp + evExtent.hp)
        };
    }

    private getNatureSubTotal(subTotal: IStatsModel, natureExtent: IStatsModel): IStatsModel {
        return <IStatsModel>{
            atk: Math.floor(subTotal.atk + natureExtent.atk),
            spa: Math.floor(subTotal.spa + natureExtent.spa),
            def: Math.floor(subTotal.def + natureExtent.def),
            spd: Math.floor(subTotal.spd + natureExtent.spd),
            spe: Math.floor(subTotal.spe + natureExtent.spe),
            hp: Math.floor(subTotal.hp + natureExtent.hp)
        };
    }

    private getAbilitySubTotal(subTotal: IStatsModel, abilityExtent: IStatsModel): IStatsModel {
        return <IStatsModel>{
            atk: Math.floor(subTotal.atk + abilityExtent.atk),
            spa: Math.floor(subTotal.spa + abilityExtent.spa),
            def: Math.floor(subTotal.def + abilityExtent.def),
            spd: Math.floor(subTotal.spd + abilityExtent.spd),
            spe: Math.floor(subTotal.spe + abilityExtent.spe),
            hp: Math.floor(subTotal.hp + abilityExtent.hp)
        };
    }

    private getItemSubTotal(subTotal: IStatsModel, itemExtent: IStatsModel): IStatsModel {
        return <IStatsModel>{
            atk: Math.floor(subTotal.atk + itemExtent.atk),
            spa: Math.floor(subTotal.spa + itemExtent.spa),
            def: Math.floor(subTotal.def + itemExtent.def),
            spd: Math.floor(subTotal.spd + itemExtent.spd),
            spe: Math.floor(subTotal.spe + itemExtent.spe),
            hp: Math.floor(subTotal.hp + itemExtent.hp)
        };
    }

    private getStatModifiersSubTotal(subTotal: IStatsModel, statModifiersExtent: IStatsModel): IStatsModel {
        return <IStatsModel>{
            atk: Math.floor(subTotal.atk + statModifiersExtent.atk),
            spa: Math.floor(subTotal.spa + statModifiersExtent.spa),
            def: Math.floor(subTotal.def + statModifiersExtent.def),
            spd: Math.floor(subTotal.spd + statModifiersExtent.spd),
            spe: Math.floor(subTotal.spe + statModifiersExtent.spe),
            hp: Math.floor(subTotal.hp + statModifiersExtent.hp)
        };
    }

    private getFieldBoostsSubTotal(subTotal: IStatsModel, fieldBoostsExtent: IStatsModel): IStatsModel {
        return <IStatsModel>{
            atk: Math.floor(subTotal.atk + fieldBoostsExtent.atk),
            spa: Math.floor(subTotal.spa + fieldBoostsExtent.spa),
            def: Math.floor(subTotal.def + fieldBoostsExtent.def),
            spd: Math.floor(subTotal.spd + fieldBoostsExtent.spd),
            spe: Math.floor(subTotal.spe + fieldBoostsExtent.spe),
            hp: Math.floor(subTotal.hp + fieldBoostsExtent.hp)
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

    private applyExceptions(stats: IEffectiveStatsModel, model: IStatsConfigurationModel): IEffectiveStatsModel {
        if (model.pokemon.resourceName === 'shedinja') {
            stats.baseStatsExtent.hp = 1;
            stats.baseStatsSubtotal.hp = 1;
            stats.ivExtent.hp = 0;
            stats.ivSubTotal.hp = 1;
            stats.evExtent.hp = 0;
            stats.evSubTotal.hp = 1;
            stats.natureExtent.hp = 0;
            stats.natureSubTotal.hp = 1;
            stats.abilityExtent.hp = 0;
            stats.abilitySubTotal.hp = 1;
            stats.itemExtent.hp = 0;
            stats.itemSubTotal.hp = 1;
            stats.statModifierExtent.hp = 0;
            stats.statModifierSubTotal.hp = 1;
            stats.fieldBoostsExtent.hp = 0;
            stats.fieldBoostsSubTotal.hp = 0;
            stats.total.hp = 1;
        }

        return stats;
    }
}
