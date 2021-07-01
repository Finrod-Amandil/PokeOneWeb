import { Component, Input, OnInit } from '@angular/core';
import { BasicPokemonModel, IBasicPokemonModel } from 'src/models/basic-pokemon.model';
import { IStatsConfigurationModel, StatsConfigurationModel } from 'src/models/stats-configuration.model';

@Component({
  selector: 'pokeone-advanced-stat-chart',
  templateUrl: './advanced-stat-chart.component.html',
  styleUrls: ['./advanced-stat-chart.component.scss']
})
export class AdvancedStatChartComponent implements OnInit {

  @Input() pokemon: IBasicPokemonModel = new BasicPokemonModel;

  public statsConfig: IStatsConfigurationModel = new StatsConfigurationModel();

  constructor( ) { }

  ngOnInit(): void { }

  public onStatChanged(index: number, statsConfiguration: IStatsConfigurationModel) {
    if (index === -1) {
      this.statsConfig = statsConfiguration;
    }
  }
}
