export interface ILearnMethodModel {
    isAvailable: boolean;
    learnMethodName: string;
    description: string;
    price: string;
    sortIndex: number;
}

export class LearnMethodModel implements ILearnMethodModel {
    isAvailable = false;
    learnMethodName = '';
    description = '';
    price = '';
    sortIndex = 0;
}