import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ItemListComponent } from './item-list.component';
import { ItemListRoutingModule } from './item-list-routing.module';
import { ComponentsModule } from 'src/app/core/components/components.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { VirtualScrollerModule } from 'ngx-virtual-scroller';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@NgModule({
    declarations: [ItemListComponent],
    imports: [
        CommonModule,
        ItemListRoutingModule,
        ComponentsModule,
        NgSelectModule,
        VirtualScrollerModule,
        FormsModule,
        RouterModule
    ]
})
export class ItemListModule {}
