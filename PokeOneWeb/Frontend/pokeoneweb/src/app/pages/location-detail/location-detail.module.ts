import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { NgSelectModule } from "@ng-select/ng-select";
import { ComponentsModule } from "src/app/core/components/components.module";
import { LocationDetailRoutingModule } from "./location-detail-routing.module";
import { LocationDetailComponent } from "./location-detail.component";

@NgModule({
    declarations: [LocationDetailComponent],
    imports: [
        CommonModule,
        LocationDetailRoutingModule,
        ComponentsModule,
        NgSelectModule,
        FormsModule,
        RouterModule,
    ]
})
export class LocationDetailModule { }