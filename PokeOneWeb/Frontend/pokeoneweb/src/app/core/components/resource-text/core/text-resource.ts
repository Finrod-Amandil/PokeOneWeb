import { ResourceType } from './resource-type.enum';

export class TextResource {
    resourceType: ResourceType = ResourceType.Text;
    resourceName: String = '';
    spriteName: String = '';
    text: String = '';
}
