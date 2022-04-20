import { RegionListModel } from 'src/app/core/models/region-list.model';

import { RegionListComponent } from './region-list.component';

describe('Region List Component', () => {
    let titleService: any;
    let regionService: any;
    let sortService: any;

    let component: RegionListComponent;

    beforeEach(() => {
        // Arrange

        titleService = jasmine.createSpy('Title');
        regionService = jasmine.createSpy('RegionService');
        sortService = jasmine.createSpy('RegionListSortService');

        component = new RegionListComponent(titleService, regionService, sortService);
    });

    describe('getRegionType', () => {
        it('When only isMainRegion flag is set should return "Main Region"', () => {
            // Arrange
            const model = new RegionListModel();
            model.isMainRegion = true;
            model.isSideRegion = false;
            model.isEventRegion = false;

            // Act
            const result = component.getRegionType(model);

            // Assert
            expect(result).toBe('Main Region');
        });

        it('When only isSideRegion flag is set should return "Side Region"', () => {
            // Arrange
            const model = new RegionListModel();
            model.isMainRegion = false;
            model.isSideRegion = true;
            model.isEventRegion = false;

            // Act
            const result = component.getRegionType(model);

            // Assert
            expect(result).toBe('Side Region');
        });

        it('When only isEventRegion flag is set should return "Event Region"', () => {
            // Arrange
            const model = new RegionListModel();
            model.isMainRegion = false;
            model.isSideRegion = false;
            model.isEventRegion = true;

            // Act
            const result = component.getRegionType(model);

            // Assert
            expect(result).toBe('Event Region');
        });

        it('When all flags are set should return "Main Region"', () => {
            // Arrange
            const model = new RegionListModel();
            model.isMainRegion = true;
            model.isSideRegion = true;
            model.isEventRegion = true;

            // Act
            const result = component.getRegionType(model);

            // Assert
            expect(result).toBe('Main Region');
        });
    });
});
