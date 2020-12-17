import { IBaseModel } from './base.model'

export interface IMoveModel extends IBaseModel<IMoveModel> {
    id: number;
    name: string;
    damageClass: string;
    type: string;
    attackPower: number;
    accuracy: number;
    powerPoints: number;
}

export class MoveModel implements IMoveModel {
    id = 0;
    name = '';
    damageClass = '';
    type= '';
    attackPower = 0;
    accuracy = 0;
    powerPoints = 0;
}