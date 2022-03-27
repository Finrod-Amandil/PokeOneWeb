import { Injectable } from '@angular/core';
import { IFieldBoostModel } from '../models/field-boost.model';

@Injectable({
	providedIn: 'root'
})
export class FieldBoostService {
	public getFieldBoosts(): IFieldBoostModel[] {
		return [
			{
				name: 'Tailwind',
				attackBoost: 1,
				specialAttackBoost: 1,
				defenseBoost: 1,
				specialDefenseBoost: 1,
				speedBoost: 2,
				hitPointsBoost: 1,
				active: false
			},
			{
				name: 'Light Screen (single battle)',
				attackBoost: 1,
				specialAttackBoost: 1,
				defenseBoost: 1,
				specialDefenseBoost: 2,
				speedBoost: 1,
				hitPointsBoost: 1,
				active: false
			},
			{
				name: 'Light Screen (double/triple battle)',
				attackBoost: 1,
				specialAttackBoost: 1,
				defenseBoost: 1,
				specialDefenseBoost: 1.5,
				speedBoost: 1,
				hitPointsBoost: 1,
				active: false
			},
			{
				name: 'Reflect (single battle)',
				attackBoost: 1,
				specialAttackBoost: 1,
				defenseBoost: 2,
				specialDefenseBoost: 1,
				speedBoost: 1,
				hitPointsBoost: 1,
				active: false
			},
			{
				name: 'Reflect (double/triple battle)',
				attackBoost: 1,
				specialAttackBoost: 1,
				defenseBoost: 1.5,
				specialDefenseBoost: 1,
				speedBoost: 1,
				hitPointsBoost: 1,
				active: false
			}
		];
	}
}
