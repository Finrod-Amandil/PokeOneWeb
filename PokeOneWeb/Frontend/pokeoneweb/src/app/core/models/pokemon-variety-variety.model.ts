export interface IPokemonVarietyVarietyModel {
	name: string;
	sortIndex: number;
	resourceName: string;
	spriteName: string;
	availability: string;
	primaryType: string;
	secondaryType: string;
}

export class PokemonVarietyVarietyModel implements IPokemonVarietyVarietyModel {
	name = '';
	sortIndex = 0;
	resourceName = '';
	spriteName = '';
	availability = '';
	primaryType = '';
	secondaryType = '';
}
