import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { WEBSITE_NAME } from 'src/app/core/constants/string.constants';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
    constructor(private titleService: Title) {}

    ngOnInit(): void {
        this.titleService.setTitle(`${WEBSITE_NAME}`);
    }
}
