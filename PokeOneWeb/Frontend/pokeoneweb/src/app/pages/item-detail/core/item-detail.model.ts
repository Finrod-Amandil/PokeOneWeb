import { IItemModel } from 'src/app/core/models/api/item.model';

export class ItemDetailModel {
    public itemName = '';
    public item: IItemModel | null = null;
}
