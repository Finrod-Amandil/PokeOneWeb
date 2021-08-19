import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { MatSliderModule } from "@angular/material/slider";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { NgSelectModule } from "@ng-select/ng-select";
import { VirtualScrollerModule } from "ngx-virtual-scroller";
import { AppRoutingModule } from "src/app/app-routing.module";
import { AdvancedStatChartComponent } from "./advanced-stat-chart/advanced-stat-chart.component";
import { EvolutionChartComponent } from "./evolution-chart/evolution-chart.component";
import { NavbarItemComponent } from "./navbar-item/navbar-item.component";
import { NavbarComponent } from "./navbar/navbar.component";
import { ResourceTextComponent } from "./resource-text/resource-text.component";
import { ScrollToTopComponent } from "./scroll-to-top/scroll-to-top.component";
import { SeasonBadgeComponent } from "./season-badge/season-badge.component";
import { SliderInputComponent } from "./slider-input/slider-input.component";
import { StatInputComponent } from "./stat-input/stat-input.component";
import { TimeBadgeComponent } from "./time-badge/time-badge.component";
import { TypeBadgeComponent } from "./type-badge/type-badge.component";
import { VerticalStatBarComponent } from "./vertical-stat-bar/vertical-stat-bar.component";

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
        VerticalStatBarComponent
    ],
    imports: [
        CommonModule,
        NgSelectModule,
        FormsModule,
        MatSliderModule,
    ],
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
        VerticalStatBarComponent
    ],
})
export class ComponentsModule {}