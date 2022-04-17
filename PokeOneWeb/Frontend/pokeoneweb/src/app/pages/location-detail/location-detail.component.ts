import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { WEBSITE_NAME } from 'src/app/core/constants/string.constants';
import { ILocationGroupModel, LocationGroupModel } from 'src/app/core/models/location-group.model';
import { LocationService } from 'src/app/core/services/api/location.service';

@Component({
    selector: 'app-location-detail',
    templateUrl: './location-detail.component.html',
    styleUrls: ['./location-detail.component.scss']
})
export class LocationDetailComponent implements OnInit {

    public model: LocationGroupModel = new LocationGroupModel();
    public locationName: string = "";

    constructor(
        private route: ActivatedRoute,
        private titleService: Title,
        private LocationService: LocationService
    ) {}

    ngOnInit(): void {
        this.route.data.subscribe((result) => {
            this.model.locationGroupName = result['locationGroupResourceName'];
            
            /*this.LocationService.getByLocationGroupResourceNameFull(this.model.locationGroupName).subscribe((result) => {
                this.model.locationGroup = result as ILocationGroupModel;

                this.titleService.setTitle(`${this.model.locationGroup.locationGroupName} - ${WEBSITE_NAME}`);
            });*/
        });
    }
}
