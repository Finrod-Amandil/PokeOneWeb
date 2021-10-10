export interface IMoveNameModel {
    resourceName: string;
    name: string;
}

export class MoveNameModel implements IMoveNameModel {
    resourceName = '';
    name = '';
}