import { Injectable } from '@angular/core';
import { FieldBoost } from './stat-configuration.model';

@Injectable({
    providedIn: 'root',
})
export class FieldBoostService {
    getFieldBoosts(): FieldBoost[] {
        return [
            {
                name: 'Tailwind',
                attackBoost: 1,
                specialAttackBoost: 1,
                defenseBoost: 1,
                specialDefenseBoost: 1,
                speedBoost: 2,
                hitPointsBoost: 1,
                active: false,
            },
            {
                name: 'Light Screen\n(single battle)',
                attackBoost: 1,
                specialAttackBoost: 1,
                defenseBoost: 1,
                specialDefenseBoost: 2,
                speedBoost: 1,
                hitPointsBoost: 1,
                active: false,
            },
            {
                name: 'Light Screen\n(double/triple battle)',
                attackBoost: 1,
                specialAttackBoost: 1,
                defenseBoost: 1,
                specialDefenseBoost: 1.5,
                speedBoost: 1,
                hitPointsBoost: 1,
                active: false,
            },
            {
                name: 'Reflect\n(single battle)',
                attackBoost: 1,
                specialAttackBoost: 1,
                defenseBoost: 2,
                specialDefenseBoost: 1,
                speedBoost: 1,
                hitPointsBoost: 1,
                active: false,
            },
            {
                name: 'Reflect\n(double/triple battle)',
                attackBoost: 1,
                specialAttackBoost: 1,
                defenseBoost: 1.5,
                specialDefenseBoost: 1,
                speedBoost: 1,
                hitPointsBoost: 1,
                active: false,
            },
            {
                name: 'Aurora Veil\n(single battle)',
                attackBoost: 1,
                specialAttackBoost: 1,
                defenseBoost: 2,
                specialDefenseBoost: 2,
                speedBoost: 1,
                hitPointsBoost: 1,
                active: false,
            },
            {
                name: 'Aurora Veil\n(double/triple battle)',
                attackBoost: 1,
                specialAttackBoost: 1,
                defenseBoost: 1.5,
                specialDefenseBoost: 1.5,
                speedBoost: 1,
                hitPointsBoost: 1,
                active: false,
            },
        ];
    }
}
