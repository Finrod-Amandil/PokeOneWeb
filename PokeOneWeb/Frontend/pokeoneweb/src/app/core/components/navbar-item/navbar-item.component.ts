import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'pokeone-navbar-item',
    templateUrl: './navbar-item.component.html',
    styleUrls: ['./navbar-item.component.scss']
})
export class NavbarItemComponent implements OnInit {
    @Input() menuItemCaption = 'menu-item';
    @Input() menuItemImage = '#';
    @Input() menuItemLink = '';
    @Input() subMenuItems: string[] = [];

    ngOnInit(): void {}
}
