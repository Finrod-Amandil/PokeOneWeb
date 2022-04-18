import { ILocationListModel } from 'src/app/core/models/location-list.model';
import { LocationListColumn } from './core/location-list-column.enum';
import { LocationListFilterService } from './core/location-list-filter.service';
import { LocationListSortService } from './core/location-list-sort.service';
import { LocationListPageModel } from './core/location-list.model';
import { LocationListComponent } from './location-list.component';

describe('Location List Component', () => {
    let component: LocationListComponent;
    let location1: ILocationListModel = {
        resourceName: 'route-1',
        name: 'Route 1',
        sortIndex: 1000,
        regionResourceName: 'kanto',
        regionName: 'Kanto'
    };
    let location2: ILocationListModel = {
        resourceName: 'pallet-town',
        name: 'Pallet Town',
        sortIndex: 1001,
        regionResourceName: 'kanto',
        regionName: 'Kanto'
    };
    let location3: ILocationListModel = {
        resourceName: 'viridian-city',
        name: 'Viridian City',
        sortIndex: 1002,
        regionResourceName: 'kanto',
        regionName: 'Kanto'
    };
    let location4: ILocationListModel = {
        resourceName: 'route-22',
        name: 'Route 22',
        sortIndex: 1003,
        regionResourceName: 'kanto',
        regionName: 'Kanto'
    };
    let locationService: any;
    let titleService: any;
    let router: any;
    let activatedRoute: any;

    beforeEach(() => {
        // Arrange
        //mock all the required entities wich are used in the components constructor
        //they need to have non-empty arrays of mocked functions
        titleService = jasmine.createSpy('Title');
        locationService = jasmine.createSpy('LocationService');
        router = jasmine.createSpy('Router');
        activatedRoute = jasmine.createSpy('ActivatedRoute');

        component = new LocationListComponent(
            titleService,
            locationService,
            new LocationListFilterService(),
            new LocationListSortService(),
            router,
            activatedRoute
        );

        component.model = new LocationListPageModel();
        component.model.locationModels = [location1, location2, location3, location4];
        component.model.displayedLocationModels = [location1, location2, location3, location4];
    });

    // describe which Method is being tested
    describe('getSortButtonClass', () => {
        //describe what is being tested
        it('expect the location list to be sorted by name and with direction 1', () => {
            // Act
            let response = component.getSortButtonClass(LocationListColumn.Name, 1);

            // Assert
            expect(response).toMatch('sorted');
        });

        it('expect the location list to be unsorted by name and with direction -1', () => {
            // Act
            let response = component.getSortButtonClass(LocationListColumn.Name, -1);

            // Assert
            expect(response).toMatch('unsorted');
        });
    });

    describe('trackById', () => {
        it('expect the sortIndex ID to be 1000 from location 1', () => {
            // Act
            let sortIndex = component.trackById(0, location1);

            // Assert
            expect(sortIndex).toBe(1000);
        });

        it('expect the sortIndex ID to be 1001 from location 2', () => {
            // Act
            let sortIndex = component.trackById(0, location2);

            // Assert
            expect(sortIndex).toBe(1001);
        });
    });

    describe('sort', () => {
        it('expect the location list to be sorted by name and with direction 1', () => {
            // Act
            component.sort(LocationListColumn.Name, 1);

            // Assert
            expect(component.model.locationModels.length).toBe(4);
            expect(component.model.displayedLocationModels[0].resourceName).toBe('pallet-town');
        });

        it('expect the location list to be sorted by name and with direction -1', () => {
            // Act
            component.sort(LocationListColumn.Name, -1);

            // Assert
            expect(component.model.locationModels.length).toBe(4);
            expect(component.model.displayedLocationModels[0].resourceName).toBe('viridian-city');
        });

        it('expect the location list to be sorted by sortIndex and with direction 1', () => {
            // Act
            component.sort(LocationListColumn.SortIndex, 1);

            // Assert
            expect(component.model.locationModels.length).toBe(4);
            expect(component.model.displayedLocationModels[0].resourceName).toBe('route-1');
        });

        it('expect the location list to be sorted by sortIndex and with direction -1', () => {
            // Act
            component.sort(LocationListColumn.SortIndex, -1);

            // Assert
            expect(component.model.locationModels.length).toBe(4);
            expect(component.model.displayedLocationModels[0].resourceName).toBe('route-22');
        });
    });

    describe('filter', () => {
        it('expect the location list to be reduced to the element pallet-town', async () => {
            // Act
            component.model.filter.searchTerm = 'pall';
            await component.onFilterChanged();

            // Assert
            expect(component.model.displayedLocationModels.length).toBe(1);
            expect(component.model.displayedLocationModels[0].resourceName).toBe('pallet-town');
        });

        it('expect the location list to be reduced to elements containint route', async () => {
            // Act
            component.model.filter.searchTerm = 'route';
            await component.onFilterChanged();

            // Assert
            expect(component.model.displayedLocationModels.length).toBe(2);
            expect(component.model.displayedLocationModels[0].resourceName).toBe('route-1');
            expect(component.model.displayedLocationModels[1].resourceName).toBe('route-22');
        });

        it('expect the location list to be empty', async () => {
            // Act
            component.model.filter.searchTerm = 'asdf';
            await component.onFilterChanged();

            // Assert
            expect(component.model.displayedLocationModels.length).toBe(0);
        });

        it('expect the location list to be the same', async () => {
            // Act
            component.model.filter.searchTerm = '';
            await component.onFilterChanged();

            // Assert
            expect(component.model.displayedLocationModels.length).toBe(4);
        });
    });
});
