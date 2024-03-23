import { EntityType } from '../../enums/entity-type.enum';

export interface IEntityTypeModel {
    entityType: EntityType;
}

export class EntityTypeModel implements IEntityTypeModel {
    entityType = EntityType.Unknown;
}
