import { contains } from "cypress/types/jquery";

describe('Pokedex Titel testing', () => {
  beforeEach(() => {
    //mock DB with json Files in Cypress/fixtures
    cy.intercept('GET', 'https://localhost:5001/api/pokemon/getall', { fixture: 'getall.json' })
    cy.intercept('GET', 'https://localhost:5001/api/move/getallnames', { fixture: 'getallnames.json' })
    //standard URL is localhost:4200, now visit pokedex
    cy.visit('/p');
  });
  it('Click show "only fully evolved" and check that Bulbasaur is not there', () => {
    cy.get('#input_fully_evolved').click()
    cy.contains('Bulbasaur').should('not.exist')
  })
})
