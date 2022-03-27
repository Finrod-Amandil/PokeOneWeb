import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LocationListComponent } from './location-list.component';
import { LocationListRoutingModule } from './location-list-routing.module';
import { ComponentsModule } from 'src/app/core/components/components.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { VirtualScrollerModule } from 'ngx-virtual-scroller';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@NgModule({
	declarations: [LocationListComponent],
	imports: [
		CommonModule,
		LocationListRoutingModule,
		ComponentsModule,
		NgSelectModule,
		VirtualScrollerModule,
		FormsModule,
		RouterModule
	]
})
export class LocationListModule {}
