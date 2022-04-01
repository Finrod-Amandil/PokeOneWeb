import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgSelectModule } from '@ng-select/ng-select';
import { ComponentsModule } from 'src/app/core/components/components.module';
import { ItemDetailRoutingModule } from './item-detail-routing.module';
import { ItemDetailComponent } from './item-detail.component';

@NgModule({
    declarations: [ItemDetailComponent],
    imports: [CommonModule, ItemDetailRoutingModule, ComponentsModule, NgSelectModule, FormsModule, RouterModule]
})
export class ItemDetailModule {}
