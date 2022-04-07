describe('UI-tests for page location-list', () => {
    beforeEach(() => {
      //mock DB with json Files in Cypress/fixtures
      cy.intercept('GET', 'https://localhost:5001/api/pokemon/getallforregion?regionName=Kanto', { fixture: 'getallforregion_kanto.json' })
  
      //baseURL is localhost:4200 see cypress.json, now visit location kanto
      cy.visit('/l?regionName=Kanto');
    });
  
    it('Should search for all Pokemons with "Mega" in their name', () => {

    })
  
  })