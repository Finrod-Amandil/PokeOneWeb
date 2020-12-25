import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-pokemon-detail',
  templateUrl: './pokemon-detail.component.html',
  styleUrls: ['./pokemon-detail.component.scss']
})
export class PokemonDetailComponent implements OnInit {

  public pokemonName: string = '';

  constructor(private route : ActivatedRoute) { }

  ngOnInit(): void {
    this.route.data.subscribe(result => this.pokemonName = result["resourceName"]);
  }

}
