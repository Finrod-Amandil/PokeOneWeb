import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { WEBSITE_NAME } from 'src/app/core/constants/string.constants';

@Component({
    selector: 'pokeone-faq',
    templateUrl: './faq.component.html',
    styleUrls: ['./faq.component.scss']
})
export class FaqComponent implements OnInit {
    constructor(private titleService: Title) {}

    ngOnInit(): void {
        this.titleService.setTitle(`FAQ - ${WEBSITE_NAME}`);
    }
}
