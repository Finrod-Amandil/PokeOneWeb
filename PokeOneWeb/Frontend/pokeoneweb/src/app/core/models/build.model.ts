import { IItemOptionModel } from './item-option.model';
import { IMoveOptionModel } from './move-option.model';
import { INatureOptionModel } from './nature-option.model';

export interface IBuildModel {
	pokemonResourceName: string;
	pokemonName: string;

	buildName: string;
	buildDescription: string;
	ability: string;
	abilityDescription: string;
	atkEv: number;
	spaEv: number;
	defEv: number;
	spdEv: number;
	speEv: number;
	hpEv: number;

	moveOptions: IMoveOptionModel[];
	itemOptions: IItemOptionModel[];
	natureOptions: INatureOptionModel[];
}

export class BuildModel implements IBuildModel {
	pokemonResourceName = '';
	pokemonName = '';

	buildName = '';
	buildDescription = '';
	ability = '';
	abilityDescription = '';
	atkEv = 0;
	spaEv = 0;
	defEv = 0;
	spdEv = 0;
	speEv = 0;
	hpEv = 0;

	moveOptions = [];
	itemOptions = [];
	natureOptions = [];
}
