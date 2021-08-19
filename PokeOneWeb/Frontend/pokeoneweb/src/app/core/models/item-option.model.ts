export interface IItemOptionModel {
    itemResourceName: string;
    itemName: string;
}

export class ItemOptionModel implements IItemOptionModel {
    itemResourceName = '';
    itemName = '';
}