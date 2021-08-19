import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { ResourceType } from './core/resource-type.enum';
import { TextResource } from './core/text-resource';

@Component({
  selector: 'pokeone-resource-text',
  templateUrl: './resource-text.component.html',
  styleUrls: ['./resource-text.component.scss']
})
export class ResourceTextComponent implements OnInit {

  @Input() rawText: String = '';

  public resourceType = ResourceType;
  public textElements: TextResource[] = []

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.parseText();
  }

  navigate(url: string) {
    this.router.navigateByUrl(url);
  }

  private parseText() {
    if (!this.rawText) return;

    let textToParse = this.rawText;
    while (textToParse.length > 0) {
      if (textToParse.startsWith("[")) {
        textToParse = this.parseResource(textToParse);
      }
      else if (textToParse.includes("[")) {
        this.textElements.push(<TextResource> {
          resourceType: ResourceType.Text,
          resourceName: '',
          spriteName: '',
          text: textToParse.substring(0, textToParse.indexOf("["))
        });
        textToParse = textToParse.substring(textToParse.indexOf("["));
      }
      else {
        this.textElements.push(<TextResource> {
          resourceType: ResourceType.Text,
          resourceName: '',
          spriteName: '',
          text: textToParse
        });
        textToParse = '';
      }
    }
  }

  private parseResource(textToParse: String): String {

    if (textToParse.indexOf("[") === -1 || textToParse.indexOf("]") === -1) {
      throw new Error("Resource could not be extracted, no opening tag found: " + textToParse);
    }

    let tag = textToParse.substring(textToParse.indexOf("[") + 1, textToParse.indexOf("]"));

    let typeName = tag.substring(0, tag.indexOf(" "));
    let type = ResourceType.Text;
    switch (typeName) {
      case "item":
        type = ResourceType.Item;
        break;
      case "location":
        type = ResourceType.Location;
        break;
      case "pokemon": 
        type = ResourceType.Pokemon;
        break;
      default:
          throw new Error("Unsupported resource type: " + typeName);
    }

    let resourceName = '';
    let spriteName = '';
    tag.substring(typeName.length).trim().split(" ").forEach(a => {
      if (a.indexOf("=") === -1) {
        throw new Error("Invalid attribute, no value provided: " + a);
      }

      let attributeName = a.substring(0, a.indexOf("="))
      let attributeValue = a.substring(a.indexOf("=") + 2, a.length - 1)

      switch(attributeName) {
        case "r":
          resourceName = attributeValue;
          break;
        case "s":
          spriteName = attributeValue;
          break;
        default:
          throw new Error("Unsupported attribute type: " + a);
      }
    })

    let closingTag = "[/" + typeName + "]";

    if (textToParse.indexOf(closingTag) === -1) {
      throw new Error("Resource could not be extracted, no closing tag found: " + textToParse);
    }

    let text = textToParse.substring(textToParse.indexOf("]") + 1, textToParse.indexOf(closingTag));

    textToParse = textToParse.substring(textToParse.indexOf(closingTag) + closingTag.length);

    this.textElements.push(<TextResource> {
      resourceType: type,
      resourceName: resourceName,
      spriteName: spriteName,
      text: text
    });

    return textToParse;
  }
}
