import { IsFocusableConfig } from '@angular/cdk/a11y';
import { ISpawnModel, SpawnModel } from '../../models/spawn.model';
import { DateService } from '../../services/date.service';
import { SpawnListColumn } from './core/spawn-list-column.enum';
import { SpawnListSortService } from './core/spawn-list-sort.service';
import { SpawnListComponent } from './spawn-list.component';

describe('SpawnListComponent', () => {
  let component: SpawnListComponent;
  let sortService: SpawnListSortService;
  let dateService: DateService;

  beforeEach(() => {
      // Arrange
      //mock all the required entities wich are used in the components constructor
      //they need to have non-empty arrays of mocked functions
      sortService = new SpawnListSortService()
      dateService = new DateService()

      component = new SpawnListComponent(dateService, sortService);
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
        component.model.spawns = spawns as ISpawnModel[];

        //component.model.pokemon = pokemon
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
        component.model.spawns = spawns as ISpawnModel[];

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
        component.model.spawns = spawns as ISpawnModel[];

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
        component.model.spawns = spawns as ISpawnModel[];

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
    it('hideEventExclusiveSpawns with inactive event no pokemon data', () => {
        // Act
        component.hideEventExclusiveSpawns();

        // Assert
        expect(component.model.visibleSpawns.length)
            .toBe(0);
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
        component.model.spawns = spawns as ISpawnModel[];

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
        component.model.spawns = spawns as ISpawnModel[];

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
  
  describe('checkLocations', () => {
    it('checkLocations() with only one regionName', () => {
        var spawn1 = new SpawnModel();
        spawn1.pokemonName = "a"
        spawn1.pokemonFormSortIndex = 0
        spawn1.regionName = "test"

        var spawn2 = new SpawnModel();
        spawn2.pokemonName = "b"
        spawn2.pokemonFormSortIndex = 1
        spawn2.regionName = "test"
        
        let spawns:SpawnModel[] = [spawn1, spawn2]
        component.model.spawns = spawns as ISpawnModel[];

        component.checkLocations();

        expect(component.model.hasOnlyOneLocation)
            .toBeTrue;
    });
    it('checkLocations() with multiple regionNames', () => {
        var spawn1 = new SpawnModel();
        spawn1.pokemonName = "a"
        spawn1.pokemonFormSortIndex = 0
        spawn1.regionName = "test"

        var spawn2 = new SpawnModel();
        spawn2.pokemonName = "b"
        spawn2.pokemonFormSortIndex = 1
        spawn2.regionName = "keintest"
        
        let spawns:SpawnModel[] = [spawn1, spawn2]
        component.model.spawns = spawns as ISpawnModel[];

        component.checkLocations();

        expect(component.model.hasOnlyOneLocation)
            .toBeFalse;
    });
  });

  describe('SpawnListSortService', () => {
    it('sortSpawns(SpawnListColumn.Pokemon) test high to low', () => {
        var spawn1 = new SpawnModel();
        spawn1.pokemonName = "a"
        spawn1.pokemonFormSortIndex = 0

        var spawn2 = new SpawnModel();
        spawn2.pokemonName = "b"
        spawn2.pokemonFormSortIndex = 1
        
        let spawns:SpawnModel[] = [spawn1, spawn2]

        let sortedList = sortService.sortSpawns(spawns, SpawnListColumn.Pokemon, 1)

        expect(sortedList[0])
            .toEqual(spawn1);
    });
    describe('SpawnListSortService', () => {
        it('sortSpawns(SpawnListColumn.Pokemon) test low to high', () => {
            var spawn1 = new SpawnModel();
            spawn1.pokemonName = "a"
            spawn1.pokemonFormSortIndex = 0

            var spawn2 = new SpawnModel();
            spawn2.pokemonName = "b"
            spawn2.pokemonFormSortIndex = 1
            
            let spawns: SpawnModel[] = [spawn1, spawn2]
    
            let sortedList = sortService.sortSpawns(spawns, SpawnListColumn.Pokemon, -1)
    
            expect(sortedList[0])
                .toEqual(spawn2);
            });
        });
    });
    it('sortSpawns(SpawnListColumn.SpawnType) test high to low', () => {
        var spawn1 = new SpawnModel();
        spawn1.pokemonName = "a"
        spawn1.pokemonFormSortIndex = 0
        spawn1.spawnTypeSortIndex = 0

        var spawn2 = new SpawnModel();
        spawn2.pokemonName = "b"
        spawn2.pokemonFormSortIndex = 1
        spawn2.spawnTypeSortIndex = 1
        
        let spawns:SpawnModel[] = [spawn1, spawn2]

        let sortedList = sortService.sortSpawns(spawns, SpawnListColumn.SpawnType, -1)

        expect(sortedList[0])
            .toEqual(spawn2);
    });
    it('sortSpawns(SpawnListColumn.SpawnType) test low to high', () => {
        var spawn1 = new SpawnModel();
        spawn1.pokemonName = "a"
        spawn1.spawnTypeSortIndex = 0

        var spawn2 = new SpawnModel();
        spawn2.pokemonName = "b"
        spawn2.spawnTypeSortIndex = 1
        
        let spawns:SpawnModel[] = [spawn1, spawn2]

        let sortedList = sortService.sortSpawns(spawns, SpawnListColumn.SpawnType, 1)

        expect(sortedList[0])
            .toEqual(spawn1);
    });
    it('sortSpawns(SpawnListColumn.Rarity) test low to high', () => {
        var spawn1 = new SpawnModel();
        spawn1.pokemonName = "a"
        spawn1.rarityValue = 1

        var spawn2 = new SpawnModel();
        spawn2.pokemonName = "b"
        spawn2.rarityValue = 5
        
        let spawns:SpawnModel[] = [spawn1, spawn2]

        let sortedList = sortService.sortSpawns(spawns, SpawnListColumn.Rarity, 1)

        expect(sortedList[0])
            .toEqual(spawn2);
    });
    it('sortSpawns(SpawnListColumn.Rarity) test low to high', () => {
        var spawn1 = new SpawnModel();
        spawn1.pokemonName = "a"
        spawn1.rarityValue = 1

        var spawn2 = new SpawnModel();
        spawn2.pokemonName = "b"
        spawn2.rarityValue = 5
        
        let spawns:SpawnModel[] = [spawn1, spawn2]

        let sortedList = sortService.sortSpawns(spawns, SpawnListColumn.Rarity, -1)

        expect(sortedList[0])
            .toEqual(spawn1);
    });
});