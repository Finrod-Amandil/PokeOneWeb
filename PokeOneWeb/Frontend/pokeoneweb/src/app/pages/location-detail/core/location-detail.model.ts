import { ILocationGroupModel } from 'src/app/core/models/location-group.model';

export class LocationDetailModel {
    public locationGroupResourceName = '';
    public locationGroup: ILocationGroupModel | null = null;
}
