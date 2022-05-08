import { environment } from 'src/environments/environment';

describe('UI-tests for page pokemon-list', () => {
    beforeEach(() => {
        //mock DB with json Files in Cypress/fixtures
        cy.intercept('GET', environment.baseUrl.concat('/varieties.json'), { fixture: 'getall.json' });

        //baseURL is localhost:4200 see cypress.json, now visit pokedex (/p)
        cy.visit('/p');
    });

    it('Should search for all Pokemons with "Mega" in their name', () => {
        cy.get('input[id="input_search"]').type('Mega');
        cy.contains('Bulbasaur').should('not.exist');
        cy.contains('Mega Venusaur').should('exist');
    });

    it('Should click "only fully evolved" and check that Bulbasaur is not there', () => {
        cy.get('#input_fully_evolved').click();
        cy.contains('Bulbasaur').should('not.exist');
        cy.contains('Venusaur').should('exist');
    });

    it('Should filter for all obtainable Pokemon and not find any Mega Evolutions anymore', () => {
        cy.get('#input_availability').click();
        cy.get('#input_availability').get('.ng-option').contains('Obtainable').click();
        cy.contains('Bulbasaur').should('exist');
        cy.contains('Mega Venusaur').should('not.exist');
    });

    it('Should filter for all Pokemon with only the type Fire', () => {
        cy.get('#input_type1').click();
        cy.get('#input_type1').get('.ng-option').contains('FIRE').click();
        cy.get('#input_type2').click();
        cy.get('#input_type2').get('.ng-option').contains('NONE').click();
        cy.get('#pokemon_list tbody').children().should('have.length', 2);
        cy.contains('Charmander').should('exist');
        cy.contains('Charmeleon').should('exist');
        cy.contains('Charizard').should('not.exist');
    });

    it('Should filter for all Pokemon with only one type', () => {
        cy.get('#input_type1').click();
        cy.get('#input_type1').get('.ng-option').contains('ANY').click();
        cy.get('#input_type2').click();
        cy.get('#input_type2').get('.ng-option').contains('NONE').click();
        cy.get('#pokemon_list tbody').children().should('have.length', 4);
        cy.contains('Charmander').should('exist');
        cy.contains('Charmeleon').should('exist');
        cy.contains('Squirtle').should('exist');
        cy.contains('Wartortle').should('exist');
        cy.contains('Charizard').should('not.exist');
    });
});
