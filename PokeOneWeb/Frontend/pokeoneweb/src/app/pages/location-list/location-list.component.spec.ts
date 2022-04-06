import { LocationListComponent } from "./location-list.component";

describe('Pokemon Detail Component', () => {
    let router: any
    let pkmService: any
    let srtService: any
    let titService: any
    let url: any
    let route: any
    let component: LocationListComponent

    beforeEach(() => {
        // Arrange
        //mock all the required entities wich are used in the components constructor
        //they need to have non-empty arrays of mocked functions
        router = jasmine.createSpy('Router')
        pkmService = jasmine.createSpy('PokemonService')
        srtService = jasmine.createSpy('PokemonDetailSortService')
        titService = jasmine.createSpy('Title')
        url = jasmine.createSpy('PokemonUrlService')
        route = jasmine.createSpy('ActivatedRoute')
        component = new LocationListComponent(router,pkmService,srtService,titService,url,route);
    });

    // describe which Method is being tested
    describe('getAvailabilityClass', () => {
        //describe what is being tested
        it('getAvailabilityClass with Context Obtainable should return availability-obtainable', () => {
            // Act 
            
            // Assert
            // describe the test with what to expect
        });

        it('getAvailabilityClass with Context Unobtainable should return availability-unobtainable', () => {
            // Act 
            
            // Assert
        });
    });
});