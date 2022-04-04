import { IPokemonVarietyModel, PokemonVarietyModel } from "src/app/core/models/pokemon-variety.model";
import { ISpawnModel, SpawnModel } from "src/app/core/models/spawn.model";
import { DateService } from "src/app/core/services/date.service";
import { PokemonDetailSortService } from "./core/pokemon-detail-sort.service";
import { PokemonDetailComponent } from "./pokemon-detail.component";

describe('Pokemon Detail Component', () => {
    let route: any
    let pkmService: any
    let srtService: PokemonDetailSortService
    let titService: any
    let urlService: any
    let dateService: DateService
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
        dateService = new DateService()

        component = new PokemonDetailComponent(route,pkmService,srtService,titService,urlService,dateService);
    });

    describe('hideEventExclusiveSpawns', () => {
        beforeEach(() => {
            spyOn(dateService, "getTodaysDate").and.returnValue("01/01/2022");
        });
        it('hideEventExclusiveSpawns with two spawns should hide the one that is an event', () => {
            //Arrange
            var spawn1 = new SpawnModel();
            spawn1.eventName = "isEvent"
            spawn1.isEvent = true;
            spawn1.eventStartDate = "Dec 1, 2020"
            spawn1.eventEndDate = "Jan 6, 2021"
    
            var spawn2 = new SpawnModel();
            spawn2.eventName = "isNotEvent"
            spawn2.isEvent = false;
            spawn2.eventStartDate = "null"
            spawn2.eventEndDate = "null"
    
            let spawns:SpawnModel[] = [spawn1, spawn2]
    
            var pokemon = new PokemonVarietyModel() as IPokemonVarietyModel;
            pokemon.spawns = spawns as ISpawnModel[];
    
            component.model.pokemon = pokemon
            // Act
            component.hideEventExclusiveSpawns();

            // Assert
            expect(component.model.visibleSpawns.length)
                .toBe(1);
            expect(component.model.areOnlyEventExclusiveSpawnsAvailable)
                .toBeFalse
            expect(component.model.visibleSpawns[0].eventName)
                .toMatch("isNotEvent");
            expect(component.model.areEventExclusiveSpawnsHidden)
                .toBeTrue
        });
        
        it('hideEventExclusiveSpawns with only eventspawns should show all event spawns', () => {
            //Arrange
            var spawn1 = new SpawnModel();
            spawn1.eventName = "isEvent"
            spawn1.isEvent = true;
            spawn1.eventStartDate = "Dec 1, 2020"
            spawn1.eventEndDate = "Jan 6, 2021"
    
            var spawn2 = new SpawnModel();
            spawn2.eventName = "isEvent"
            spawn2.isEvent = true;
            spawn2.eventStartDate = "Dec 1, 2020"
            spawn2.eventEndDate = "Jan 6, 2021"
    
            let spawns:SpawnModel[] = [spawn1, spawn2]
    
            var pokemon = new PokemonVarietyModel() as IPokemonVarietyModel;
            pokemon.spawns = spawns as ISpawnModel[];
    
            component.model.pokemon = pokemon
            // Act
            component.hideEventExclusiveSpawns();

            // Assert
            expect(component.model.visibleSpawns.length)
                .toBe(2);
            expect(component.model.areOnlyEventExclusiveSpawnsAvailable)
                .toBeTrue
            expect(component.model.visibleSpawns[0].eventName && component.model.visibleSpawns[1].eventName)
                .toMatch("isEvent");
            expect(component.model.areEventExclusiveSpawnsHidden)
                .toBeTrue
        });

        it('hideEventExclusiveSpawns with active event should show event and non-event spawns', () => {
            //Arrange
            var spawn1 = new SpawnModel();
            spawn1.eventName = "isEvent"
            spawn1.isEvent = true;
            spawn1.eventStartDate = "Dec 1, 2021"
            spawn1.eventEndDate = "Jan 6, 2022"
    
            var spawn2 = new SpawnModel();
            spawn2.eventName = "isNotEvent"
            spawn2.isEvent = false;
            spawn2.eventStartDate = "null"
            spawn2.eventEndDate = "null"
    
            let spawns:SpawnModel[] = [spawn1, spawn2]
    
            var pokemon = new PokemonVarietyModel() as IPokemonVarietyModel;
            pokemon.spawns = spawns as ISpawnModel[];
    
            component.model.pokemon = pokemon
            // Act
            component.hideEventExclusiveSpawns();

            // Assert
            expect(component.model.visibleSpawns.length)
                .toBe(2);
            expect(component.model.areOnlyEventExclusiveSpawnsAvailable)
                .toBeFalse
            expect(component.model.areEventExclusiveSpawnsHidden)
                .toBeTrue
        });

        it('hideEventExclusiveSpawns with inactive event should show only non-event spawns', () => {
            //Arrange
            var spawn1 = new SpawnModel();
            spawn1.eventName = "isEvent"
            spawn1.isEvent = true;
            spawn1.eventStartDate = "Dec 1, 2020"
            spawn1.eventEndDate = "Jan 6, 2021"
    
            var spawn2 = new SpawnModel();
            spawn2.eventName = "isNotEvent"
            spawn2.isEvent = false;
            spawn2.eventStartDate = "null"
            spawn2.eventEndDate = "null"
    
            let spawns:SpawnModel[] = [spawn1, spawn2]
    
            var pokemon = new PokemonVarietyModel() as IPokemonVarietyModel;
            pokemon.spawns = spawns as ISpawnModel[];
    
            component.model.pokemon = pokemon
            // Act
            component.hideEventExclusiveSpawns();

            // Assert
            expect(component.model.visibleSpawns.length)
                .toBe(1);
            expect(component.model.areOnlyEventExclusiveSpawnsAvailable)
                .toBeFalse
            expect(component.model.areEventExclusiveSpawnsHidden)
                .toBeTrue
        });
    });

    describe('showEventExclusiveSpawns', () => {
        beforeEach(() => {
            spyOn(dateService, "getTodaysDate").and.returnValue("01/01/2022");
        });

        it('showEventExclusiveSpawns with two events one of them being event-exclusive spawns', () => {
            //Arrange
            var spawn1 = new SpawnModel();
            spawn1.eventName = "isEvent"
            spawn1.isEvent = true;
            spawn1.eventStartDate = "Dec 1, 2020"
            spawn1.eventEndDate = "Jan 6, 2021"
    
            var spawn2 = new SpawnModel();
            spawn2.eventName = "isNotEvent"
            spawn2.isEvent = false;
            spawn2.eventStartDate = "null"
            spawn2.eventEndDate = "null"
    
            let spawns:SpawnModel[] = [spawn1, spawn2]
    
            var pokemon = new PokemonVarietyModel() as IPokemonVarietyModel;
            pokemon.spawns = spawns as ISpawnModel[];
    
            component.model.pokemon = pokemon
            // Act
            component.showEventExclusiveSpawns();

            // Assert
            expect(component.model.visibleSpawns.length)
                .toBe(2);
            expect(component.model.areNoEventSpawnsAvailable)
                .toBeFalse
            expect(component.model.areOnlyEventExclusiveSpawnsAvailable)
                .toBeFalse
            expect(component.model.areEventExclusiveSpawnsHidden)
                .toBeFalse
        });

        it('showEventExclusiveSpawns with no event spawns', () => {
            //Arrange
            var spawn1 = new SpawnModel();
            spawn1.eventName = "isNotEvent"
            spawn1.isEvent = false;
            spawn1.eventStartDate = "null"
            spawn1.eventEndDate = "null"
    
            var spawn2 = new SpawnModel();
            spawn2.eventName = "isNotEvent"
            spawn2.isEvent = false;
            spawn2.eventStartDate = "null"
            spawn2.eventEndDate = "null"
    
            let spawns:SpawnModel[] = [spawn1, spawn2]
    
            var pokemon = new PokemonVarietyModel() as IPokemonVarietyModel;
            pokemon.spawns = spawns as ISpawnModel[];
    
            component.model.pokemon = pokemon
            // Act
            component.showEventExclusiveSpawns();

            // Assert
            expect(component.model.visibleSpawns.length)
                .toBe(2);
            expect(component.model.areNoEventSpawnsAvailable)
                .toBeTrue
            expect(component.model.areOnlyEventExclusiveSpawnsAvailable)
                .toBeFalse
            expect(component.model.areEventExclusiveSpawnsHidden)
                .toBeFalse
        });
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
