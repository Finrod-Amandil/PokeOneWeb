export interface IMoveOptionModel {
	slot: number;
	moveName: string;
	elementalType: string;
	damageClass: string;
	attackPower: number;
	accuracy: number;
	powerPoints: number;
	priority: number;
	effectDescription: string;
}

export class MoveOptionModel implements IMoveOptionModel {
	slot = 0;
	moveName = '';
	elementalType = '';
	damageClass = '';
	attackPower = 0;
	accuracy = 0;
	powerPoints = 0;
	priority = 0;
	effectDescription = '';
}
