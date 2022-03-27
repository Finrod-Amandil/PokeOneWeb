import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GuidesComponent } from './guides.component';
import { GuidesRoutingModule } from './guides-routing.module';
import { ComponentsModule } from 'src/app/core/components/components.module';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@NgModule({
	declarations: [GuidesComponent],
	imports: [CommonModule, GuidesRoutingModule, ComponentsModule, FormsModule, RouterModule]
})
export class GuidesModule {}
