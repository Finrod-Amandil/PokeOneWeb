import { Component, computed, inject, input } from '@angular/core';
import { Router } from '@angular/router';
import { ResourceType } from './resource-type.enum';
import { TextResource } from './text-resource';

@Component({
    selector: 'pokeoneweb-resource-text',
    imports: [],
    templateUrl: './resource-text.html',
    styleUrl: './resource-text.scss',
})
export class ResourceTextComponent {
    rawText = input.required<string>();

    private router = inject(Router);

    ResourceType = ResourceType;
    textElements = computed(() => {
        if (!this.rawText()) return [];

        let textElements: TextResource[] = [];
        let textToParse = this.rawText();
        while (textToParse.length > 0) {
            if (textToParse.startsWith('[')) {
                const [resourceElement, remainingText] = this.parseResource(textToParse);
                textElements.push(resourceElement);
                textToParse = remainingText;
            } else if (textToParse.includes('[')) {
                textElements.push({
                    resourceType: ResourceType.Text,
                    resourceName: '',
                    spriteName: '',
                    text: textToParse.substring(0, textToParse.indexOf('[')),
                });
                textToParse = textToParse.substring(textToParse.indexOf('['));
            } else {
                textElements.push({
                    resourceType: ResourceType.Text,
                    resourceName: '',
                    spriteName: '',
                    text: textToParse,
                });
                textToParse = '';
            }
        }

        return textElements;
    });

    navigate(url: string) {
        this.router.navigateByUrl(url);
    }

    private parseResource(textToParse: string): [TextResource, string] {
        if (textToParse.indexOf('[') === -1 || textToParse.indexOf(']') === -1) {
            throw new Error(
                'Resource could not be extracted, no opening tag found: ' + textToParse,
            );
        }

        const tag = textToParse.substring(textToParse.indexOf('[') + 1, textToParse.indexOf(']'));

        const typeName = tag.substring(0, tag.indexOf(' '));
        let type = ResourceType.Text;
        switch (typeName) {
            case 'item':
                type = ResourceType.Item;
                break;
            case 'location':
                type = ResourceType.Location;
                break;
            case 'pokemon':
                type = ResourceType.Pokemon;
                break;
            default:
                throw new Error('Unsupported resource type: ' + typeName);
        }

        let resourceName = '';
        let spriteName = '';
        tag.substring(typeName.length)
            .trim()
            .split(' ')
            .forEach((a) => {
                if (a.indexOf('=') === -1) {
                    throw new Error('Invalid attribute, no value provided: ' + a);
                }

                const attributeName = a.substring(0, a.indexOf('='));
                const attributeValue = a.substring(a.indexOf('=') + 2, a.length - 1);

                switch (attributeName) {
                    case 'r':
                        resourceName = attributeValue;
                        break;
                    case 's':
                        spriteName = attributeValue;
                        break;
                    default:
                        throw new Error('Unsupported attribute type: ' + a);
                }
            });

        const closingTag = '[/' + typeName + ']';

        if (textToParse.indexOf(closingTag) === -1) {
            throw new Error(
                'Resource could not be extracted, no closing tag found: ' + textToParse,
            );
        }

        const text = textToParse.substring(
            textToParse.indexOf(']') + 1,
            textToParse.indexOf(closingTag),
        );

        textToParse = textToParse.substring(textToParse.indexOf(closingTag) + closingTag.length);

        const resourceElement = {
            resourceType: type,
            resourceName: resourceName,
            spriteName: spriteName,
            text: text,
        };

        return [resourceElement, textToParse];
    }
}
