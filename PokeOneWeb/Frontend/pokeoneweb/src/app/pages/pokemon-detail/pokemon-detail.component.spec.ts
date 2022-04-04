import { PokemonDetailComponent } from "./pokemon-detail.component";

describe('Pokemon Detail Component', () => {
    let route: any
    let pkmService: any
    let srtService: any
    let titService: any
    let url: any
    let dateService: any
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
        dateService = jasmine.createSpy('DateService')
        component = new PokemonDetailComponent(route,pkmService,srtService,titService,dateService,url);
    });

    // describe which Method is being tested
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