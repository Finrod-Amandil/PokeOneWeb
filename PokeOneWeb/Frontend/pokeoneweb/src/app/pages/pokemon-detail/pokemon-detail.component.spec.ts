import { PokemonDetailComponent } from "./pokemon-detail.component";

describe('Pokemon Detail Component', () => {
    let route: any
        let pkmService: any
        let srtService: any
        let titService: any
        let url: any
        let component: PokemonDetailComponent

    beforeEach(() => {
        // Arrange
        //mock all the required entities wich are used in the components constructor
        //they need to have non-empty arrays of mocked functions
        route = jasmine.createSpy('ActivatedRoute')
        pkmService = jasmine.createSpy('PokemonService')
        srtService = jasmine.createSpy('PokemonDetailSortService')
        titService = jasmine.createSpy('Title')
        url = jasmine.createSpy('PokemonUrlService')
        component = new PokemonDetailComponent(route,pkmService,srtService,titService,url);
    });

    describe('getAvailabilityClass', () => {
        
        //descrive what is being tested
        it('getAvailabilityClass with Context Obtainable should return availability-obtainable', () => {
            // Act 
            var result = component.getAvailabilityClass('Obtainable')
            
            // Assert
            // describe the test with what to expect
            expect(result)
                .toMatch('availability-obtainable');
        });

        //descrive what is being tested
        it('getAvailabilityClass with Context Unobtainable should return availability-unobtainable', () => {
            // Act 
            var result = component.getAvailabilityClass('Unobtainable')
            
            // Assert
            // describe the test with what to expect
            expect(result)
                .toMatch('availability-unobtainable');
        });
    });

    describe('getHueForPowerPoints', () => {
        //descrive what is being tested
        it('expect getHueForPowerPoints with 30 to be 120', () => {
            // Act
            var result = component.getHueForPowerPoints(30)
            
            // Assert
            // describe the test with what to expect
            expect(result)
                .toBe(120);
        });

        //descrive what is being tested
        it('expect getHueForPowerPoints with 29 to be 96', () => {
            // Act
            // Because javascript is shit with calculating decimal places the comparison has to be done with a tolerance
            var result = component.getHueForPowerPoints(29) - 115.2
            
            // Assert
            // describe the test with what to expect
            // tolerance of the difference max 0.1
            expect(result)
                .toBeLessThan(Math.abs(0.1));
        });

        //descrive what is being tested
        it('expect getHueForPowerPoints with 6 to be 4', () => {
            // Act
            var result = component.getHueForPowerPoints(6)
            
            // Assert
            // describe the test with what to expect
            expect(result)
                .toBe(4.8);
        });

        //descrive what is being tested
        it('expect getHueForPowerPoints with 5 to be 0', () => {
            // Act
            var result = component.getHueForPowerPoints(5)
            
            // Assert
            // describe the test with what to expect
            expect(result)
                .toBe(0);
        });
        
        //descrive what is being tested
        it('expect getHueForPowerPoints with 0 to be 0', () => {
            // Act
            var result = component.getHueForPowerPoints(0)
            
            // Assert
            // describe the test with what to expect
            expect(result)
                .toBe(0);
        });

        //descrive what is being tested
        it('expect getHueForPowerPoints with negative value to be 0', () => {
            // Act
            var result = component.getHueForPowerPoints(-1)
            
            // Assert
            // describe the test with what to expect
            expect(result)
                .toBe(0);
        });
    });
});