import { IPokemonVarietyListModel } from './pokemon-variety-list.model';
export interface IMoveModel {
	name: string;
	resourceName: string;
	elementalType: string;
	damageClass: string;
	attackPower: number;
	accuracy: number;
	powerPoints: number;
	priority: number;
	effectDescription: string;
}

export class MoveModel implements IMoveModel {
	name = '';
	resourceName = '';
	elementalType = '';
	damageClass = '';
	attackPower = 0;
	accuracy = 0;
	powerPoints = 0;
	priority = 0;
	effectDescription = '';
}
