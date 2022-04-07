describe('UI-tests for page pokemon-list', () => {
    beforeEach(() => {
      //mock DB with json Files in Cypress/fixtures
      cy.intercept('GET', 'https://localhost:5001/varieties.json', { fixture: 'getall.json' })
  
      //baseURL is localhost:4200 see cypress.json, now visit pokedex (/p)
      cy.visit('/p');
    });
  
    it('Should search for all Pokemons with "Mega" in their name', () => {
      cy.get('input[id="input_search"]').type('Mega')
      cy.contains('Bulbasaur').should('not.exist')
      cy.contains('Mega Venusaur').should('exist')
    })
  
    it('Should click "only fully evolved" and check that Bulbasaur is not there', () => {
      cy.get('#input_fully_evolved').click()
      cy.contains('Bulbasaur').should('not.exist')
      cy.contains('Venusaur').should('exist')
    })
  
    it('Should filter for all obtainable Pokemon and not find any Mega Evolutions anymore', () => {
      cy.get('#input_availability').click()
      cy.get('#input_availability').get(".ng-option").contains("Obtainable").click()
      cy.contains('Bulbasaur').should('exist')
      cy.contains('Mega Venusaur').should('not.exist')
    })
  
  })