import { DateService } from 'src/app/core/services/date.service';

describe('Date Service', () => {
    let dateService: DateService

    beforeEach(() => {
        // Arrange
        jasmine.clock().install();
        var baseTime = new Date(2022, 0, 1);
        jasmine.clock().mockDate(baseTime);

        dateService = new DateService();
    });

    afterEach(function () {
        jasmine.clock().uninstall();
    });
    
    describe('convertDate', () => {
        it('convertDate with date Jan 1, 2022 should return 01/01/2022', () => {
            // Act 
            var date = "Jan 1, 2022"
            var result = dateService.convertDate(date)
            
            // Assert
            expect(result)
                .toMatch('01/01/2022');
        });
    
        it('convertDate with date Apr 1, 2022 should return 01/04/2022', () => {
            // Act 
            var date = "Apr 1, 2022"
            var result = dateService.convertDate(date)
            
            // Assert
            expect(result)
                .toMatch('01/04/2022');
        });

        it('convertDate with date Dec 1, 2022 should return 01/12/2022', () => {
            // Act 
            var date = "Dec 1, 2022"
            var result = dateService.convertDate(date)
            
            // Assert
            expect(result)
                .toMatch('01/12/2022');
        });
    });

    describe('getTodaysDate', () => {
        it('getTodaysDate should return 01/01/2022', () => {
            // Act
            var result = dateService.getTodaysDate()
            
            // Assert
            expect(result)
                .toMatch('01/01/2022');
        });
    });
});
