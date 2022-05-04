import { IItemModel } from 'src/app/core/models/item.model';

export class ItemDetailModel {
    public itemName = '';
    public item: IItemModel | null = null;
}
