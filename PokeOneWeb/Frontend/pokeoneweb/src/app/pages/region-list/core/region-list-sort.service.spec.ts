import { RegionModel } from 'src/app/core/models/api/region.model';
import { RegionListSortService } from './region-list-sort.service';

describe('Region List Component', () => {
    let service: RegionListSortService;

    let mainRegion1: RegionModel;
    let mainRegion2: RegionModel;
    let sideRegion1: RegionModel;
    let sideRegion2: RegionModel;
    let eventRegion1: RegionModel;
    let eventRegion2: RegionModel;

    beforeEach(() => {
        // Arrange
        service = new RegionListSortService();

        mainRegion1 = new RegionModel();
        mainRegion1.isMainRegion = true;
        mainRegion1.isSideRegion = false;
        mainRegion1.isEventRegion = false;
        mainRegion1.eventStartDate = '';

        mainRegion2 = new RegionModel();
        mainRegion2.isMainRegion = true;
        mainRegion2.isSideRegion = false;
        mainRegion2.isEventRegion = false;
        mainRegion2.eventStartDate = '';

        sideRegion1 = new RegionModel();
        sideRegion1.isMainRegion = false;
        sideRegion1.isSideRegion = true;
        sideRegion1.isEventRegion = false;
        sideRegion1.eventStartDate = '';

        sideRegion2 = new RegionModel();
        sideRegion2.isMainRegion = false;
        sideRegion2.isSideRegion = true;
        sideRegion2.isEventRegion = false;
        sideRegion2.eventStartDate = '';

        eventRegion1 = new RegionModel();
        eventRegion1.isMainRegion = false;
        eventRegion1.isSideRegion = false;
        eventRegion1.isEventRegion = true;
        eventRegion1.eventStartDate = '';

        eventRegion2 = new RegionModel();
        eventRegion2.isMainRegion = false;
        eventRegion2.isSideRegion = false;
        eventRegion2.isEventRegion = true;
        eventRegion2.eventStartDate = '';
    });

    describe('sort', () => {
        it('When [Side, Main] should sort [Main, Side]', () => {
            // Arrange
            const regions: RegionModel[] = [sideRegion1, mainRegion1];

            // Act
            const result = service.sort(regions);

            // Assert
            expect(result.length).toBe(2);
            expect(result[0]).toEqual(mainRegion1);
            expect(result[1]).toEqual(sideRegion1);
        });

        it('When [Main, Side] should sort [Main, Side]', () => {
            // Arrange
            const regions: RegionModel[] = [mainRegion1, sideRegion1];

            // Act
            const result = service.sort(regions);

            // Assert
            expect(result.length).toBe(2);
            expect(result[0]).toEqual(mainRegion1);
            expect(result[1]).toEqual(sideRegion1);
        });

        it('When [Event, Main] should sort [Main, Event]', () => {
            // Arrange
            const regions: RegionModel[] = [eventRegion1, mainRegion1];

            // Act
            const result = service.sort(regions);

            // Assert
            expect(result.length).toBe(2);
            expect(result[0]).toEqual(mainRegion1);
            expect(result[1]).toEqual(eventRegion1);
        });

        it('When [Main, Event] should sort [Main, Event]', () => {
            // Arrange
            const regions: RegionModel[] = [mainRegion1, eventRegion1];

            // Act
            const result = service.sort(regions);

            // Assert
            expect(result.length).toBe(2);
            expect(result[0]).toEqual(mainRegion1);
            expect(result[1]).toEqual(eventRegion1);
        });

        it('When [Event, Side] should sort [Side, Event]', () => {
            // Arrange
            const regions: RegionModel[] = [eventRegion1, sideRegion1];

            // Act
            const result = service.sort(regions);

            // Assert
            expect(result.length).toBe(2);
            expect(result[0]).toEqual(sideRegion1);
            expect(result[1]).toEqual(eventRegion1);
        });

        it('When [Side, Event] should sort [Side, Event]', () => {
            // Arrange
            const regions: RegionModel[] = [sideRegion1, eventRegion1];

            // Act
            const result = service.sort(regions);

            // Assert
            expect(result.length).toBe(2);
            expect(result[0]).toEqual(sideRegion1);
            expect(result[1]).toEqual(eventRegion1);
        });

        it('When [Event, Event] should order by date descending (case array unsorted)', () => {
            // Arrange
            eventRegion1.eventStartDate = '2022-01-01';
            eventRegion2.eventStartDate = '2021-01-01';
            const regions: RegionModel[] = [eventRegion2, eventRegion1];

            // Act
            const result = service.sort(regions);

            // Assert
            expect(result.length).toBe(2);
            expect(result[0]).toEqual(eventRegion1);
            expect(result[1]).toEqual(eventRegion2);
        });

        it('When [Event, Event] should order by date descending (case array sorted)', () => {
            // Arrange
            eventRegion1.eventStartDate = '2022-01-01';
            eventRegion2.eventStartDate = '2021-01-01';
            const regions: RegionModel[] = [eventRegion1, eventRegion2];

            // Act
            const result = service.sort(regions);

            // Assert
            expect(result.length).toBe(2);
            expect(result[0]).toEqual(eventRegion1);
            expect(result[1]).toEqual(eventRegion2);
        });
    });
});
