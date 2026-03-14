import { ResourceType } from './resource-type.enum';

export class TextResource {
    resourceType: ResourceType = ResourceType.Text;
    resourceName: string = '';
    spriteName: string = '';
    text: string = '';
}
