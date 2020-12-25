import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-location-detail',
  templateUrl: './location-detail.component.html',
  styleUrls: ['./location-detail.component.scss']
})
export class LocationDetailComponent implements OnInit {

  public locationName: string = '';

  constructor(private route : ActivatedRoute) { }

  ngOnInit(): void {
    this.route.data.subscribe(result => this.locationName = result["resourceName"]);
  }

}
