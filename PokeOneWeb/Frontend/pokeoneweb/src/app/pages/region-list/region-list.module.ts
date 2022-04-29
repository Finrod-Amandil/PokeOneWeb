import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegionListComponent } from './region-list.component';
import { RegionListRoutingModule } from './region-list-routing.module';
import { ComponentsModule } from 'src/app/core/components/components.module';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    RegionListComponent
  ],
  imports: [
    CommonModule,
    RegionListRoutingModule,
    ComponentsModule,
    RouterModule
  ]
})
export class RegionListModule { }
