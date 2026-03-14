import { Component, input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { SubmenuItem } from '../submenu-item';

@Component({
    selector: 'pokeoneweb-navbar-item',
    imports: [RouterLink],
    templateUrl: './navbar-item.html',
    styleUrl: './navbar-item.scss',
})
export class NavbarItemComponent {
    menuItemCaption = input('menu-item');
    menuItemImage = input('#');
    menuItemLink = input('');
    subMenuItems = input<SubmenuItem[]>([]);
    hasTwoLineCaption = input(false);
}
