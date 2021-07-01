import { IBasicPokemonModel } from "./basic-pokemon.model";
import { IMoveNameModel, MoveNameModel } from "./move-name.model";

export interface IMoveModel extends IMoveNameModel {
    damageClass: string;
    type: string;
    attackPower: number;
    accuracy: number;
    powerPoints: number;
    effectDescription: string;
}

export class MoveModel extends MoveNameModel implements IMoveModel {
    damageClass = '';
    type= '';
    attackPower = 0;
    accuracy = 0;
    powerPoints = 0;
    effectDescription = '';
}