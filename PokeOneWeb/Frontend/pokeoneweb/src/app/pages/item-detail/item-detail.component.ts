import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.scss']
})
export class ItemDetailComponent implements OnInit {

  public itemName: string = '';

  constructor(private route : ActivatedRoute) { }

  ngOnInit(): void {
    this.route.data.subscribe(result => this.itemName = result["resourceName"]);
  }

}
