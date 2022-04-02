import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FaqComponent } from './faq.component';
import { FaqRoutingModule } from './faq-routing.module';
import { ComponentsModule } from 'src/app/core/components/components.module';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@NgModule({
    declarations: [FaqComponent],
    imports: [CommonModule, FaqRoutingModule, ComponentsModule, FormsModule, RouterModule]
})
export class FaqModule {}
