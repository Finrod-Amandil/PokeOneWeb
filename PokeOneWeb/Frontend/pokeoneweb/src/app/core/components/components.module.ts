import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatSliderModule } from '@angular/material/slider';
import { NgSelectModule } from '@ng-select/ng-select';
import { AdvancedStatChartComponent } from './advanced-stat-chart/advanced-stat-chart.component';
import { EvolutionChartComponent } from './evolution-chart/evolution-chart.component';
import { NavbarItemComponent } from './navbar-item/navbar-item.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ResourceTextComponent } from './resource-text/resource-text.component';
import { ScrollToTopComponent } from './scroll-to-top/scroll-to-top.component';
import { SeasonBadgeComponent } from './season-badge/season-badge.component';
import { SliderInputComponent } from './slider-input/slider-input.component';
import { StatInputComponent } from './stat-input/stat-input.component';
import { TimeBadgeComponent } from './time-badge/time-badge.component';
import { TypeBadgeComponent } from './type-badge/type-badge.component';
import { VerticalStatBarComponent } from './vertical-stat-bar/vertical-stat-bar.component';
import { GlassButtonComponent } from './glass-button/glass-button.component';
import { SpawnListComponent } from './spawn-list/spawn-list.component';
import { PlacedItemListComponent } from './placed-item-list/placed-item-list.component';

@NgModule({
    declarations: [
        AdvancedStatChartComponent,
        EvolutionChartComponent,
        NavbarComponent,
        NavbarItemComponent,
        ResourceTextComponent,
        ScrollToTopComponent,
        SeasonBadgeComponent,
        SliderInputComponent,
        StatInputComponent,
        TimeBadgeComponent,
        TypeBadgeComponent,
        VerticalStatBarComponent,
        GlassButtonComponent,
        SpawnListComponent,
        PlacedItemListComponent,
        PlacedItemListComponent
    ],
    imports: [CommonModule, NgSelectModule, FormsModule, MatSliderModule, RouterModule],
    exports: [
        AdvancedStatChartComponent,
        EvolutionChartComponent,
        NavbarComponent,
        NavbarItemComponent,
        ResourceTextComponent,
        ScrollToTopComponent,
        SeasonBadgeComponent,
        SliderInputComponent,
        StatInputComponent,
        TimeBadgeComponent,
        TypeBadgeComponent,
        VerticalStatBarComponent,
        GlassButtonComponent,
        SpawnListComponent,
        PlacedItemListComponent
    ],
})
export class ComponentsModule {}
