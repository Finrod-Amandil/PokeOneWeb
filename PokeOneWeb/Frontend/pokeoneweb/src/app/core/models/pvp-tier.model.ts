export interface IPvpTierModel {
    sortIndex: number;
    name: string;
}

export class PvpTierModel implements IPvpTierModel {
    sortIndex = 0;
    name = '';
}