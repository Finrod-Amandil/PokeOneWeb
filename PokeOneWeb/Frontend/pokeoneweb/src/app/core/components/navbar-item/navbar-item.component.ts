import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'pokeone-navbar-item',
  templateUrl: './navbar-item.component.html',
  styleUrls: ['./navbar-item.component.scss']
})
export class NavbarItemComponent implements OnInit {
  @Input() menuItemCaption: string = "menu-item";
  @Input() menuItemImage: string = "#";
  @Input() menuItemLink: string = "";
  @Input() subMenuItems: string[] = [];

  ngOnInit(): void {
  }

}
