import { ISpawnModel } from 'src/app/core/models/spawn.model';
import { SpawnListColumn } from './spawn-list-column.enum';

export class SpawnListComponentModel {
    public spawnsSortedByColumn = SpawnListColumn.Location;
    public spawnsSortDirection = 1;

    public areEventExclusiveSpawnsHidden = true;
    public areOnlyEventExclusiveSpawnsAvailable = false;
    public areNoEventSpawnsAvailable = false;

    public spawns: ISpawnModel[] = [];
    public visibleSpawns: ISpawnModel[] = [];

    public hasOnlyOneLocation = false;
    public hasOnlyOnePokemon = false;
    public isLocationPage = false;
}
