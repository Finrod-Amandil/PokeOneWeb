import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { NgSelectModule } from "@ng-select/ng-select";
import { ComponentsModule } from "src/app/core/components/components.module";
import { PokemonDetailRoutingModule } from "./pokemon-detail-routing.module";
import { PokemonDetailComponent } from "./pokemon-detail.component";

@NgModule({
    declarations: [PokemonDetailComponent],
    imports: [
        CommonModule,
        PokemonDetailRoutingModule,
        ComponentsModule,
        NgSelectModule,
        FormsModule,
        RouterModule,
    ]
})
export class PokemonDetailModule { }