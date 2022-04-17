import { ISpawnModel } from "src/app/core/models/spawn.model";
import { SpawnListColumn } from "src/app/pages/pokemon-detail/core/spawn-list-column.enum";

export class SpawnListComponentModel {
    public spawnsSortedByColumn: SpawnListColumn = SpawnListColumn.Location;
    public spawnsSortDirection: number = 1;
    public areEventExclusiveSpawnsHidden: boolean = true;
    public areOnlyEventExclusiveSpawnsAvailable: boolean = false;
    public areNoEventSpawnsAvailable: boolean = false;
    public visibleSpawns: ISpawnModel[] = [];
    public spawns: ISpawnModel[] = [];
}