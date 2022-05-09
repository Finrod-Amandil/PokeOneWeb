export interface IMoveNameModel {
    name: string;
    resourceName: string;
}

export interface IMoveModel extends IMoveNameModel {
    elementalType: string;
    damageClass: string;
    attackPower: number;
    accuracy: number;
    powerPoints: number;
    priority: number;
    effectDescription: string;
}

export class MoveNameModel implements IMoveNameModel {
    name = '';
    resourceName = '';
}

export class MoveModel extends MoveNameModel implements IMoveModel {
    elementalType = '';
    damageClass = '';
    attackPower = 0;
    accuracy = 0;
    powerPoints = 0;
    priority = 0;
    effectDescription = '';
}
