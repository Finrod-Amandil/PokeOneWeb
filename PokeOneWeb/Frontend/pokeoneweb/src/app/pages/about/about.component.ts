import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { WEBSITE_NAME } from 'src/app/core/constants/string.constants';

@Component({
    selector: 'pokeone-about',
    templateUrl: './about.component.html',
    styleUrls: ['./about.component.scss']
})
export class AboutComponent implements OnInit {
    constructor(private titleService: Title) {}

    ngOnInit(): void {
        this.titleService.setTitle(`About - ${WEBSITE_NAME}`);
    }
}
