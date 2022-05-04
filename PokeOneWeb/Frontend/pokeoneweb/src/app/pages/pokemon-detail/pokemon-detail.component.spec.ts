import { IPokemonVarietyModel, PokemonVarietyModel } from "src/app/core/models/pokemon-variety.model";
import { PokemonDetailSortService } from "./core/pokemon-detail-sort.service";
import { PokemonDetailComponent } from "./pokemon-detail.component";

describe('Pokemon Detail Component', () => {
    let route: any
    let pkmService: any
    let srtService: PokemonDetailSortService
    let titService: any
    let urlService: any
    let component: PokemonDetailComponent

    beforeEach(() => {
        // Arrange
        //mock all the required entities wich are used in the components constructor
        //they need to have non-empty arrays of mocked functions
        route = jasmine.createSpy('ActivatedRoute')
        pkmService = jasmine.createSpyObj('PokemonService',['getByNameFull'])
        srtService = new PokemonDetailSortService();
        titService = jasmine.createSpy('Title')
        urlService = jasmine.createSpy('PokemonUrlService')

        component = new PokemonDetailComponent(route,pkmService,srtService,titService,urlService);
    });

    describe('getAvailabilityClass', () => {
        //describe what is being tested
        it('getAvailabilityClass with Context Obtainable should return availability-obtainable', () => {
            // Act 
            var result = component.getAvailabilityClass('Obtainable')
            
            // Assert
            // describe the test with what to expect
            expect(result)
                .toMatch('availability-obtainable');
        });

        it('getAvailabilityClass with Context Unobtainable should return availability-unobtainable', () => {
            // Act 
            var result = component.getAvailabilityClass('Unobtainable')
            
            // Assert
            expect(result)
                .toMatch('availability-unobtainable');
        });

        it('getAvailabilityClass with Context Event-exclusive should return availability-event', () => {
            // Act 
            var result = component.getAvailabilityClass('Event-exclusive')
            
            // Assert
            expect(result)
                .toMatch('availability-event');
        });

        it('getAvailabilityClass with Context Removed should return availability-removed', () => {
            // Act 
            var result = component.getAvailabilityClass('Removed')
            
            // Assert
            expect(result)
                .toMatch('availability-removed');
        });

        it('getAvailabilityClass with wrong Context should return availability-unobtainable', () => {
            // Act 
            var result = component.getAvailabilityClass('wrong')
            
            // Assert
            expect(result)
                .toMatch('availability-unobtainable');
        });
    });

    describe('getEggSteps', () => {
        it('getEggSteps with eggCycle 15 should return 3840', () => {
            // Arrange
            var pokemon = new PokemonVarietyModel() as IPokemonVarietyModel;
            // like corphish
            pokemon.eggCycles = 15
    
            component.model.pokemon = pokemon
            // Act 
            var result = component.getEggSteps()
            
            // Assert
            // describe the test with what to expect
            expect(result)
                .toBe(3840);
        });

        it('getEggSteps with empty pokemon', () => {
            // Act 
            var result = component.getEggSteps()
            
            // Assert
            // describe the test with what to expect
            expect(result)
                .toBe(0);
        });
    });

    describe('getEggHatchingTime', () => {
        it('getEggHatchingTime with eggCycle 15 should return ~0h 10min', () => {
            // Arrange
            var pokemon = new PokemonVarietyModel() as IPokemonVarietyModel;
            // like corphish
            pokemon.eggCycles = 15
    
            component.model.pokemon = pokemon
            // Act 
            var result = component.getEggHatchingTime()
            
            // Assert
            expect(result)
                .toBe('~0h 10min');
        });
    });

    describe('getHueForMoveStrength', () => {
        it('expect getHueForMoveStrength with 110 to be 140', () => {
            // Act
            var result = component.getHueForMoveStrength(110)
            
            // Assert
            expect(result)
                .toBe(120);
        });

        it('expect getHueForMoveStrength with 50 to be 20', () => {
            // Act
            var result = component.getHueForMoveStrength(50)
            
            // Assert
            expect(result)
                .toBe(20);
        });
        
        it('expect getHueForMoveStrength with 41 to be 2', () => {
            // Act
            var result = component.getHueForMoveStrength(41)
            
            // Assert
            expect(result)
                .toBe(2);
        });

        it('expect getHueForMoveStrength with 30 to be 0', () => {
            // Act
            var result = component.getHueForMoveStrength(30)
            
            // Assert
            expect(result)
                .toBe(0);
        });

        it('expect getHueForMoveStrength with negative value to be 0', () => {
            // Act
            var result = component.getHueForMoveStrength(-1)
            
            // Assert
            expect(result)
                .toBe(0);
        });
    });

    describe('getHueForAccuracy', () => {
        it('expect getHueForAccuracy with 110 to be 120', () => {
            // Act

            var result = component.getHueForAccuracy(110)
            
            // Assert
            expect(result)
                .toBe(120);
        });

        it('expect getHueForAccuracy with 90 to be 60', () => {
            // Act

            var result = component.getHueForAccuracy(90)
            
            // Assert
            expect(result)
                .toBe(60);
        });

        it('expect getHueForAccuracy with 60 to be 0', () => {
            // Act

            var result = component.getHueForAccuracy(60)
            
            // Assert
            expect(result)
                .toBe(0);
        });

        it('expect getHueForAccuracy with negative value to be 0', () => {
            // Act
            var result = component.getHueForAccuracy(-1)
            
            // Assert
            expect(result)
                .toBe(0);
        });
    });

    describe('getHueForPowerPoints', () => {
        it('expect getHueForPowerPoints with 30 to be 120', () => {
            // Act
            var result = component.getHueForPowerPoints(30)
            
            // Assert
            expect(result)
                .toBe(120);
        });

        it('expect getHueForPowerPoints with 29 to be 96', () => {
            // Act
            // Because javascript is shit with calculating decimal places the comparison has to be done with a tolerance
            var result = component.getHueForPowerPoints(29) - 115.2
            
            // Assert
            // tolerance of the difference max 0.1
            expect(result)
                .toBeLessThan(Math.abs(0.1));
        });

        it('expect getHueForPowerPoints with 6 to be 4', () => {
            // Act
            var result = component.getHueForPowerPoints(6)
            
            // Assert
            expect(result)
                .toBe(4.8);
        });

        it('expect getHueForPowerPoints with 5 to be 0', () => {
            // Act
            var result = component.getHueForPowerPoints(5)
            
            // Assert
            expect(result)
                .toBe(0);
        });
        
        it('expect getHueForPowerPoints with 0 to be 0', () => {
            // Act
            var result = component.getHueForPowerPoints(0)
            
            // Assert
            expect(result)
                .toBe(0);
        });

        //descrive what is being tested
        it('expect getHueForPowerPoints with negative value to be 0', () => {
            // Act
            var result = component.getHueForPowerPoints(-1)
            
            // Assert
            expect(result)
                .toBe(0);
        });
    });
});
