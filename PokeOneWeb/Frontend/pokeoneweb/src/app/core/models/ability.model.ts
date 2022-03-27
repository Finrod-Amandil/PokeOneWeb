export interface IAbilityModel {
	name: string;
	effect: string;

	attackBoost: number;
	specialAttackBoost: number;
	defenseBoost: number;
	specialDefenseBoost: number;
	speedBoost: number;
	hitPointsBoost: number;
	boostConditions: string;
}

export class AbilityModel implements IAbilityModel {
	name = '';
	effect = '';

	attackBoost = 1;
	specialAttackBoost = 1;
	defenseBoost = 1;
	specialDefenseBoost = 1;
	speedBoost = 1;
	hitPointsBoost = 1;
	boostConditions = '';
}
