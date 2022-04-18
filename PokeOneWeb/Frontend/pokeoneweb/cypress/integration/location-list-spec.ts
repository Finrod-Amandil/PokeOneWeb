describe('UI-tests for page location-list', () => {
    beforeEach(() => {
        //mock DB with json Files in Cypress/fixtures
        cy.intercept('GET', 'https://localhost:5001/api/locationGroups/getallforregion?regionName=Kanto', {
            fixture: 'getallforregion_kanto.json'
        });
        cy.intercept('GET', 'https://localhost:5001/api/getentitytypeforpath?path=Kanto', {
            fixture: 'getentitytypeforpath_kanto.json'
        });
        //baseURL is localhost:4200 see cypress.json, now visit location kanto
        cy.visit('/Kanto');
    });

    it('Should check if initial sorting is done by sortIndex', () => {
        cy.get('#location_list tbody').children().should('have.length', 4);
        cy.get('#location_list tbody tr:nth-child(1)').should('contain', 'Pallet Town');
        cy.get('#location_list tbody tr:nth-child(2)').should('contain', 'Route 1');
        cy.get('#location_list tbody tr:nth-child(3)').should('contain', 'Viridian City');
        cy.get('#location_list tbody tr:nth-child(4)').should('contain', 'Route 22');
    });

    it('Should reverse sort all locations by name and check if amount, first and last elemnt are correct', () => {
        cy.get('.table-header-sort.unsorted').click();
        cy.get('#location_list tbody').children().should('have.length', 4);
        cy.get('#location_list tbody tr:nth-child(1)').should('contain', 'Viridian City');
        cy.get('#location_list tbody tr:nth-child(4)').should('contain', 'Pallet Town');
    });

    it('Should search for all locations with "Route" in their name', () => {
        cy.get('input[id="input_search"]').type('Route');
        cy.get('#location_list tbody').children().should('have.length', 2);
        cy.contains('Pallet Town').should('not.exist');
        cy.contains('Viridian City').should('not.exist');
        cy.contains('Route 22').should('exist');
        cy.contains('Route 1').should('exist');
    });

    it('Should check if sorting works after entering a search', () => {
        cy.get('input[id="input_search"]').type('Route');
        cy.get('.table-header-sort.unsorted').click();
        cy.get('#location_list tbody').children().should('have.length', 2);
        cy.contains('Pallet Town').should('not.exist');
        cy.contains('Viridian City').should('not.exist');
        cy.get('#location_list tbody tr:nth-child(1)').should('contain', 'Route 22');
        cy.get('#location_list tbody tr:nth-child(2)').should('contain', 'Route 1');
    });
});
