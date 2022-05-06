import { IPokemonVarietyListModel } from 'src/app/core/models/pokemon-variety-list.model';
import { PokemonListFilterModel } from './core/pokemon-list-filter.model';
import { PokemonListFilterService } from './core/pokemon-list-filter.service';
import { PokemonListSortService } from './core/pokemon-list-sort.service';
import { PokemonListComponent } from './pokemon-list.component';
import { SELECT_OPTION_ANY, SELECT_OPTION_NONE } from 'src/app/core/constants/string.constants';
import { PokemonListModel } from './core/pokemon-list.model';
import { PokemonService } from 'src/app/core/services/api/pokemon.service';
import { IPokemonVarietyNameModel } from 'src/app/core/models/pokemon-variety-name.model';
import { forkJoin, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

describe('Pokemon Detail Component', () => {
    let titleService: any;
    let pokemonService: any;
    let moveService: any;
    let generationService: any;
    let urlService: any;
    let router: any;
    let filter: PokemonListFilterModel;
    let component: PokemonListComponent;
    let http: any;

    let pokemonVariety1: IPokemonVarietyListModel = {
        resourceName: 'bulbasaur',
        sortIndex: 100,
        pokedexNumber: 1,
        name: 'Bulbasaur',
        spriteName: '1.png',

        primaryElementalType: 'Grass',
        secondaryElementalType: 'Poison',

        attack: 1,
        specialAttack: 1,
        defense: 1,
        specialDefense: 1,
        speed: 1,
        hitPoints: 1,
        statTotal: 1,
        bulk: 1,

        primaryAbility: 'Overgrow',
        primaryAbilityEffect:
            'When this Pok\u00E9mon has 1/3 or less of its HP remaining, its grass-type moves inflict 1.5\u00D7 as much regular damage.',
        secondaryAbility: '',
        secondaryAbilityEffect: '',
        hiddenAbility: 'Chlorophyll',
        hiddenAbilityEffect:
            'This Pok\u00E9mon\u0027s Speed is doubled during strong sunlight. This bonus does not count as a stat modifier.',

        availability: 'Obtainable',
        pvpTier: 'Untiered',
        pvpTierSortIndex: 2000,
        generation: 1,
        isFullyEvolved: false,
        isMega: false,

        urls: [],

        notes: ''
    };

    let pokemonVariety2: IPokemonVarietyListModel = {
        resourceName: 'charmander',
        sortIndex: 400,
        pokedexNumber: 4,
        name: 'Charmander',
        spriteName: '4.png',

        primaryElementalType: 'Fire',
        secondaryElementalType: '',

        attack: 1,
        specialAttack: 1,
        defense: 1,
        specialDefense: 1,
        speed: 1,
        hitPoints: 1,
        statTotal: 1,
        bulk: 1,

        primaryAbility: 'Blaze',
        primaryAbilityEffect:
            'When this Pok\u00E9mon has 1/3 or less of its HP remaining, its fire-type moves inflict 1.5\u00D7 as much regular damage.',
        secondaryAbility: '',
        secondaryAbilityEffect: '',
        hiddenAbility: 'Solar Power',
        hiddenAbilityEffect:
            'During strong sunlight, this Pok\u00E9mon has 1.5\u00D7 its Special Attack but takes 1/8 of its maximum HP in damage after each turn.',

        availability: 'Obtainable',
        pvpTier: 'Untiered',
        pvpTierSortIndex: 2000,
        generation: 1,
        isFullyEvolved: false,
        isMega: false,

        urls: [],

        notes: ''
    };

    beforeEach(() => {
        // Arrange
        //mock all the required entities wich are used in the components constructor
        //they need to have non-empty arrays of mocked functions
        titleService = jasmine.createSpy('TitleService');
        http = jasmine.createSpy('HttpClient');
        pokemonService = new PokemonService(http);
        moveService = jasmine.createSpy('MoveService');
        generationService = jasmine.createSpy('GenerationService');
        urlService = jasmine.createSpy('PokemonUrlService');
        router = jasmine.createSpy('Router');

        let results: Observable<IPokemonVarietyNameModel[]>[] = [];
        spyOn(pokemonService, 'getAllPokemonForMoveSet').and.returnValue(
            forkJoin(results).pipe(map((resp) => resp.reduce((all, models) => all.concat(models), [])))
        );

        component = new PokemonListComponent(
            titleService,
            pokemonService,
            moveService,
            generationService,
            new PokemonListFilterService(pokemonService),
            new PokemonListSortService(),
            urlService,
            router
        );

        filter = new PokemonListFilterModel();

        component.model = new PokemonListModel();
        component.model.pokemonModels = [pokemonVariety1, pokemonVariety2];
        component.model.displayedPokemonModels = [pokemonVariety1, pokemonVariety2];
    });

    describe('filter', () => {
        it('expect the pokemon list to be reduced to pokemons with only type fire and no second type', async () => {
            // Arrange
            filter.selectedType1 = 'Fire';
            filter.selectedType2 = SELECT_OPTION_NONE;
            component.model.filter = filter;

            // Act
            await component.onFilterChanged();

            // Assert
            expect(component.model.displayedPokemonModels.length).toBe(1);
            expect(component.model.displayedPokemonModels[0].resourceName).toBe('charmander');
        });

        it('expect the pokemon list to be reduced to pokemons with only one type', async () => {
            // Arrange
            filter.selectedType1 = SELECT_OPTION_ANY;
            filter.selectedType2 = SELECT_OPTION_NONE;
            component.model.filter = filter;

            // Act
            await component.onFilterChanged();

            // Assert
            expect(component.model.displayedPokemonModels.length).toBe(1);
            expect(component.model.displayedPokemonModels[0].resourceName).toBe('charmander');
        });
    });
});
