import { ILocationGroupModel } from 'src/app/core/models/api/location-group.model';

export class LocationDetailModel {
    public locationGroupResourceName = '';
    public locationGroup: ILocationGroupModel | null = null;
}
