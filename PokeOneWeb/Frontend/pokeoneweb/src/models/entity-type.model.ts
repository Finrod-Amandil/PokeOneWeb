import { IBaseModel } from './base.model'
import { EntityType } from './entity-type.enum';

export interface IEntityTypeModel extends IBaseModel<IEntityTypeModel> {
    entityType: EntityType;
}

export class EntityTypeModel implements IEntityTypeModel {
    entityType = EntityType.Unknown;
}