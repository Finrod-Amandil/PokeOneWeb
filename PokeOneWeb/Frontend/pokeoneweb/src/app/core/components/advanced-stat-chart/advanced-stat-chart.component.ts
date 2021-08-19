import { Component, Input, OnInit } from '@angular/core';
import { IPokemonVarietyModel, PokemonVarietyModel } from '../../models/pokemon-variety.model';
import { IStatsConfigurationModel, StatsConfigurationModel } from '../../models/stats-configuration.model';

@Component({
    selector: 'pokeone-advanced-stat-chart',
    templateUrl: './advanced-stat-chart.component.html',
    styleUrls: ['./advanced-stat-chart.component.scss']
})
export class AdvancedStatChartComponent implements OnInit {

    @Input() pokemon: IPokemonVarietyModel = new PokemonVarietyModel();

    public statsConfig: IStatsConfigurationModel = new StatsConfigurationModel();

    constructor( ) { }

    ngOnInit(): void { }

    public onStatChanged(statsConfiguration: IStatsConfigurationModel) {
        this.statsConfig = statsConfiguration;
    }
}
