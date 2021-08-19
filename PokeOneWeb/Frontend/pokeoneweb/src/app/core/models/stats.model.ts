export interface IStatsModel {
    atk: number;
    spa: number;
    def: number;
    spd: number;
    spe: number;
    hp: number;

    total(): number;
}

export class StatsModel implements IStatsModel {
    atk = 0;
    spa = 0;
    def = 0;
    spd = 0;
    spe = 0;
    hp = 0;

    public total(): number {
        return this.atk + this.spa + this.def + this.spd + this.spe + this.hp;
    }
}