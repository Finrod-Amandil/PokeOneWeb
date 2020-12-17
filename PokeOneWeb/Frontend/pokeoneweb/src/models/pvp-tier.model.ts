import { IBaseModel } from './base.model'

export interface IPvpTierModel extends IBaseModel<IPvpTierModel> {
    name: string;
}

export class PvpTierModel implements IPvpTierModel {
    id = 0;
    name = '';
}