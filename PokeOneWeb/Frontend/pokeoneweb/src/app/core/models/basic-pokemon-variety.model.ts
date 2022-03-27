export interface IBasicPokemonVarietyModel {
	resourceName: string;
	sortIndex: number;
	pokedexNumber: number;
	name: string;
	spriteName: string;
	availability: string;
}

export class BasicPokemonVarietyModel implements IBasicPokemonVarietyModel {
	resourceName = '';
	sortIndex = 0;
	pokedexNumber = 0;
	name = '';
	spriteName = '';
	availability = '';
}
