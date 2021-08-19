import { Injectable } from "@angular/core";
import { IPokemonVarietyListModel } from "src/app/core/models/pokemon-variety-list.model";
import { PokemonListColumn } from "./pokemon-list-column.enum";

@Injectable({
    providedIn: 'root'
})
export class PokemonListSortService {
    public sort(models: IPokemonVarietyListModel[], sortColumn: PokemonListColumn, sortDirection: number) {
        switch (sortColumn) {
            case PokemonListColumn.PokedexNumber:
                return this.sortNumber(models, sortDirection);
            case PokemonListColumn.Name:
                return this.sortName(models, sortDirection);
            case PokemonListColumn.Atk:
                return this.sortAttack(models, sortDirection);
            case PokemonListColumn.Spa:
                return this.sortSpecialAttack(models, sortDirection);
            case PokemonListColumn.Def:
                return this.sortDefense(models, sortDirection);
            case PokemonListColumn.Spd:
                return this.sortSpecialDefense(models, sortDirection);
            case PokemonListColumn.Spe:
                return this.sortSpeed(models, sortDirection);
            case PokemonListColumn.Hp:
                return this.sortHitPoints(models, sortDirection);
            case PokemonListColumn.StatTotal:
                return this.sortTotal(models, sortDirection);
            case PokemonListColumn.PvpTier:
                return this.sortPvp(models, sortDirection);
            default:
                return models;
        }
    }

    private sortNumber(models: IPokemonVarietyListModel[], sortDirection: number): IPokemonVarietyListModel[] {
        return models.slice()
            .sort((n1, n2) => {
                if (n1.pokedexNumber > n2.pokedexNumber) {
                    return sortDirection * 1;
                }

                if (n1.pokedexNumber < n2.pokedexNumber) {
                    return sortDirection * -1;
                }

                if (n1.sortIndex > n2.sortIndex) {
                    return sortDirection * 1;
                }

                if (n1.sortIndex < n2.sortIndex) {
                    return sortDirection * -1;
                }

                return 0;
            });
    }

    private sortName(models: IPokemonVarietyListModel[], sortDirection: number): IPokemonVarietyListModel[] {
        return models.slice()
            .sort((n1, n2) => {
                if (n1.name > n2.name) {
                    return sortDirection * 1;
                }

                if (n1.name < n2.name) {
                    return sortDirection * -1;
                }

                return 0;
            });
    }

    private sortAttack(models: IPokemonVarietyListModel[], sortDirection: number): IPokemonVarietyListModel[] {
        return models.slice().sort((n1, n2) => sortDirection * (n2.attack - n1.attack));
    }

    private sortSpecialAttack(models: IPokemonVarietyListModel[], sortDirection: number): IPokemonVarietyListModel[] {
        return models.slice().sort((n1, n2) => sortDirection * (n2.specialAttack - n1.specialAttack));
    }

    private sortDefense(models: IPokemonVarietyListModel[], sortDirection: number): IPokemonVarietyListModel[] {
        return models.slice().sort((n1, n2) => sortDirection * (n2.defense - n1.defense));
    }

    private sortSpecialDefense(models: IPokemonVarietyListModel[], sortDirection: number): IPokemonVarietyListModel[] {
        return models.slice().sort((n1, n2) => sortDirection * (n2.specialDefense - n1.specialDefense));
    }

    private sortSpeed(models: IPokemonVarietyListModel[], sortDirection: number): IPokemonVarietyListModel[] {
        return models.slice().sort((n1, n2) => sortDirection * (n2.speed - n1.speed));
    }

    private sortHitPoints(models: IPokemonVarietyListModel[], sortDirection: number): IPokemonVarietyListModel[] {
        return models.slice().sort((n1, n2) => sortDirection * (n2.hitPoints - n1.hitPoints));
    }

    private sortTotal(models: IPokemonVarietyListModel[], sortDirection: number): IPokemonVarietyListModel[] {
        return models.slice().sort((n1, n2) => sortDirection * (n2.statTotal - n1.statTotal));
    }

    private sortPvp(models: IPokemonVarietyListModel[], sortDirection: number): IPokemonVarietyListModel[] {
        return models.slice()
            .sort((n1, n2) => {
                if (n1.pvpTierSortIndex > n2.pvpTierSortIndex) {
                    return sortDirection * 1;
                }

                if (n1.pvpTierSortIndex < n2.pvpTierSortIndex) {
                    return sortDirection * -1;
                }

                return 0;
            });
    }
}