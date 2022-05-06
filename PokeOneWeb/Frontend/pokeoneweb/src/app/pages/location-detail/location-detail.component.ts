import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { WEBSITE_NAME } from 'src/app/core/constants/string.constants';
import { LocationService } from 'src/app/core/services/api/location.service';
import { LocationDetailModel } from './core/location-detail.model';

@Component({
    selector: 'app-location-detail',
    templateUrl: './location-detail.component.html',
    styleUrls: ['./location-detail.component.scss']
})
export class LocationDetailComponent implements OnInit {
    public model: LocationDetailModel = new LocationDetailModel();

    constructor(private route: ActivatedRoute, private titleService: Title, private locationService: LocationService) {}

    ngOnInit(): void {
        this.route.data.subscribe((result) => {
            this.model.locationGroupResourceName = result['resourceName'];

            this.locationService.getLocationGroup(this.model.locationGroupResourceName).subscribe((result) => {
                this.model.locationGroup = result;

                this.titleService.setTitle(`${this.model.locationGroup.name} - ${WEBSITE_NAME}`);
            });
        });
    }
}
