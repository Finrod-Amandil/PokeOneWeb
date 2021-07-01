import { IItemOptionModel } from './item-option.model';
import { INatureOptionModel } from './nature-option.model';

export interface IBuildModel {
    pokemonResourceName: string;
    pokemonName: string;

    buildName: string;
    buildDescription: string;
    move1: string;
    move2: string;
    move3: string;
    move4: string;
    itemOptions: IItemOptionModel[];
    natureOptions: INatureOptionModel[];
    ability: string;
    abilityDescription: string;

    atkEv: number;
    spaEv: number;
    defEv: number;
    spdEv: number;
    speEv: number;
    hpEv: number;
}

export class BuildModel implements IBuildModel {
    pokemonResourceName = '';
    pokemonName = '';

    buildName = '';
    buildDescription = '';
    move1 = '';
    move2 = '';
    move3 = '';
    move4 = '';
    itemOptions = [];
    natureOptions = [];
    ability = '';
    abilityDescription = '';

    atkEv = 0;
    spaEv = 0;
    defEv = 0;
    spdEv = 0;
    speEv = 0;
    hpEv = 0;
}