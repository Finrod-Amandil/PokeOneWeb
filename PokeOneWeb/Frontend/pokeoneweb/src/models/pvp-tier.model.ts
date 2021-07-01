export interface IPvpTierModel {
    id: number;
    name: string;
}

export class PvpTierModel implements IPvpTierModel {
    id = 0;
    name = '';
}