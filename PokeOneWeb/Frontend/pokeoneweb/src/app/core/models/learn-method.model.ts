export interface ILearnMethodModel {
	isAvailable: boolean;
	learnMethodName: string;
	description: string;
	sortIndex: number;
}

export class LearnMethodModel implements ILearnMethodModel {
	isAvailable = false;
	learnMethodName = '';
	description = '';
	sortIndex = 0;
}
