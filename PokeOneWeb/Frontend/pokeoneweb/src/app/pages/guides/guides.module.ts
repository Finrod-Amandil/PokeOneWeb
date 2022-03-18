import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GuidesComponent } from './guides.component';
import { GuidesRoutingModule } from './guides-routing.module';
import { ComponentsModule } from 'src/app/core/components/components.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { VirtualScrollerModule } from 'ngx-virtual-scroller';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [GuidesComponent],
  imports: [
    CommonModule,
    GuidesRoutingModule,
    ComponentsModule,
    NgSelectModule,
    VirtualScrollerModule,
    FormsModule,
    RouterModule
  ]
})
export class ItemListModule { }
