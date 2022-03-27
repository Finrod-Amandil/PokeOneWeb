import { PokemonDetailComponent } from './pokemon-detail.component';

let route: any;
let pkmService: any;
let srtService: any;
let titService: any;
let url: any;

beforeEach(() => {
	//mock all the required entities wich are used in the components constructor
	//they need to have non-empty arrays of mocked functions
	route = jasmine.createSpyObj('ActivatedRoute', ['test']);
	pkmService = jasmine.createSpyObj('PokemonService', ['test']);
	srtService = jasmine.createSpyObj('PokemonDetailSortService', ['test']);
	titService = jasmine.createSpyObj('Title', ['test']);
	url = jasmine.createSpyObj('PokemonUrlService', ['test']);
});

describe('Pokemon Detail Component', () => {
	//descrive what is being tested
	it('getAvailabilityClass with Context Obtainable should return availability-obtainable', () => {
		//initialize the component with the mock entities
		const comp = new PokemonDetailComponent(route, pkmService, srtService, titService, url);
		//describe the test with what to expect
		expect(comp.getAvailabilityClass('Obtainable')).toMatch('availability-obtainable');
	});
});
