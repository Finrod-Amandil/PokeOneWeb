import { Component, Input, OnInit } from '@angular/core';
import { SpawnListColumn } from 'src/app/pages/pokemon-detail/core/spawn-list-column.enum';
import { ISpawnModel } from '../../models/spawn.model';
import { SpawnListComponentModel } from './core/spawn-list-component.model';
import { DateService } from 'src/app/core/services/date.service';
import { PokemonDetailSortService } from 'src/app/pages/pokemon-detail/core/pokemon-detail-sort.service';

@Component({
  selector: 'app-spawn-list',
  templateUrl: './spawn-list.component.html',
  styleUrls: ['./spawn-list.component.scss']
})
export class SpawnListComponent implements OnInit {
  @Input() spawns : ISpawnModel[] = [];
  
  public model : SpawnListComponentModel = new SpawnListComponentModel();
  public spawnsColumn = SpawnListColumn;
  
  constructor(
    private dateService: DateService,
    private sortService: PokemonDetailSortService
  ) {}

  ngOnInit(): void {
    this.model.spawns = this.spawns;
    this.hideEventExclusiveSpawns();
    this.applyInitialSorting();
  }

  public hideEventExclusiveSpawns() {
    if (!this.model.spawns) return;

    this.model.areEventExclusiveSpawnsHidden = true;
    this.model.visibleSpawns = [];

    this.checkAreNoEventSpawnsAvailable(this.model.spawns);

    for (let spawn of this.model.spawns){
        if(this.isSpawnAvailable(spawn)){
            this.model.visibleSpawns.push(spawn);
        }
    }

    //if only event-exclusive spawns are available that are not active show them and disable (un-)hide button
    if(this.model.visibleSpawns.length === 0){
        this.model.areOnlyEventExclusiveSpawnsAvailable = true;
        for (let spawn of this.model.spawns){
            this.model.visibleSpawns.push(spawn);
        }
    }
    else{
        this.model.areOnlyEventExclusiveSpawnsAvailable = false;
    }
    this.sortSpawns(this.model.spawnsSortedByColumn, this.model.spawnsSortDirection);
  }

  private checkAreNoEventSpawnsAvailable(pokemonSpawns: ISpawnModel[]) {
    let eventCounter = 0;

    for(let spawn of pokemonSpawns) {
        if(spawn.isEvent) {
            eventCounter += 1;
        }
    }

    if(eventCounter === 0) {
        this.model.areNoEventSpawnsAvailable = true;
    }
    else{
        this.model.areNoEventSpawnsAvailable = false;
    }
  }

  private isSpawnAvailable(spawn: ISpawnModel){
    if(spawn.isEvent){
        //Source https://stackoverflow.com/a/16080662
        var todaysDate = this.dateService.getTodaysDate().split("/");
        var eventStartDate = this.dateService.convertDate(spawn.eventStartDate).split("/");
        var eventEndDate = this.dateService.convertDate(spawn.eventEndDate).split("/");

        var from = new Date(parseInt(eventStartDate[2]), parseInt(eventStartDate[1])-1, parseInt(eventStartDate[0]));  // -1 because months are from 0 to 11
        var to   = new Date(parseInt(eventEndDate[2]), parseInt(eventEndDate[1])-1, parseInt(eventEndDate[0]));
        var check = new Date(parseInt(todaysDate[2]), parseInt(todaysDate[1])-1, parseInt(todaysDate[0]));

        if (check >= from && check <= to) {
            return true;
        }
        return false;
    }
    else{
        return true;
    }   
  }

  public showEventExclusiveSpawns() {
    this.model.areEventExclusiveSpawnsHidden = false;
    this.model.visibleSpawns = [];

    if(this.model.spawns) {
        this.model.visibleSpawns = this.model.spawns;
        this.checkAreNoEventSpawnsAvailable(this.model.spawns);
    }
    this.sortSpawns(this.model.spawnsSortedByColumn, this.model.spawnsSortDirection);
  }

  public getSpawnSortButtonClass(sortColumn: SpawnListColumn, sortDirection: number): string {
    if (this.model.spawnsSortedByColumn === sortColumn && this.model.spawnsSortDirection === sortDirection) {
        return 'sorted';
    }
    return 'unsorted';
  }

  public sortSpawns(sortColumn: SpawnListColumn, sortDirection: number) {
    if (!this.model.spawns) return;

    this.model.spawnsSortedByColumn = sortColumn;
    this.model.spawnsSortDirection = sortDirection;

    this.model.visibleSpawns = this.sortService.sortSpawns(this.model.visibleSpawns, sortColumn, sortDirection);
  }
  
  private applyInitialSorting() {
    if (!this.model.spawns) return;

    this.model.visibleSpawns = this.sortService.sortSpawns(this.model.visibleSpawns, SpawnListColumn.SpawnType, 1);
    this.model.visibleSpawns = this.sortService.sortSpawns(this.model.visibleSpawns, SpawnListColumn.Location, 1);
    this.model.visibleSpawns = this.sortService.sortSpawns(this.model.visibleSpawns, SpawnListColumn.Rarity, 1);
  }
}
