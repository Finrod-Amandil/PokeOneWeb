import { Component, OnInit } from '@angular/core';
import { PokemonListService } from '../../../services/pokemonlist.service';
import { IListPokemonModel, ListPokemonModel } from '../../../models/listpokemon.model';
import { PvpTierModel } from '../../../models/pvp-tier.model';
import { GenerationModel } from '../../../models/generation.model';
import { MovesService } from '../../../services/moves.service';
import { LearnableMovesService } from '../../../services/learnable-moves-service';
import { 
  SMOGON_BASE_URL, 
  BULBAPEDIA_BASE_URL, 
  POKEONECOMMUNITY_BASE_URL, 
  POKEMONSHOWDOWN_BASE_URL, 
  SEREBII_BASE_URL, 
  POKEMONDB_BASE_URL 
} from '../../common/url.constants';
import {
  SELECT_OPTION_ANY,
  SELECT_OPTION_NONE
} from '../../common/string.constants';
import { PokemonListColumn } from './pokemon-list-column.enum'

@Component({
  selector: 'pokeone-pokemon-list',
  templateUrl: './pokemon-list.component.html',
  styleUrls: ['./pokemon-list.component.scss'],
})
export class PokemonListComponent implements OnInit {
  public listPokemonModels: ListPokemonModel[] = [];
  public moves: string[] = [];

  public maxAtk: number = 1;
  public maxSpa: number = 1;
  public maxDef: number = 1;
  public maxSpd: number = 1;
  public maxSpe: number = 1;
  public maxHp: number = 1;
  public maxTotal: number = 1;

  public pvpTiers: PvpTierModel[] = [];
  public types1: string[] = [SELECT_OPTION_ANY];
  public types2: string[] = [SELECT_OPTION_ANY, SELECT_OPTION_NONE];
  public abilities: string[] = [];
  public availabilities: string[] = [];
  public generations: GenerationModel[] = [
    { id: 1, name: 'Gen. 1 (Kanto; Red / Green / Blue / Yellow)' },
    { id: 2, name: 'Gen. 2 (Johto; Gold / Silver / Crystal)' },
    { id: 3, name: 'Gen. 3 (Hoenn; Ruby / Sapphire / Emerald, FireRed / LeafGreen)' },
    { id: 4, name: 'Gen. 4 (Sinnoh; Diamond / Perl / Platinum, HeartGold / SoulSilver)' },
    { id: 5, name: 'Gen. 5 (Unova; Black / White, Black 2 / White 2)' },
    { id: 6, name: 'Gen. 6 (Kalos; X/Y, Omega Ruby / Alpha Sapphire)' },
    { id: 7, name: 'Gen. 7 (Alola; Sun / Moon, Ultra Sun / Ultra Moon, Let\'s Go!)' }
  ];

  public searchTerm: string = '';
  public selectedAvailabilities: string[] = [];
  public selectedPvpTiers: PvpTierModel[] = [];
  public selectedType1: string = SELECT_OPTION_ANY;
  public selectedType2: string = SELECT_OPTION_ANY;
  public selectedAbility: any = null;
  public selectedMinAtk: any = 0;
  public selectedMaxAtk: any = 1;
  public selectedMinSpa: any = 0;
  public selectedMaxSpa: any = 1;
  public selectedMinDef: any = 0;
  public selectedMaxDef: any = 1;
  public selectedMinSpd: any = 0;
  public selectedMaxSpd: any = 1;
  public selectedMinSpe: any = 0;
  public selectedMaxSpe: any = 1;
  public selectedMinHp: any = 0;
  public selectedMaxHp: any = 1;
  public selectedMinTotal: any = 0;
  public selectedMaxTotal: any = 1;
  public selectedMove1Option1: string | null = null;
  public selectedMove1Option2: string | null = null;
  public selectedMove1Option3: string | null = null;
  public selectedMove1Option4: string | null = null;
  public selectedMove2Option1: string | null = null;
  public selectedMove2Option2: string | null = null;
  public selectedMove2Option3: string | null = null;
  public selectedMove2Option4: string | null = null;
  public selectedMove3Option1: string | null = null;
  public selectedMove3Option2: string | null = null;
  public selectedMove3Option3: string | null = null;
  public selectedMove3Option4: string | null = null;
  public selectedMove4Option1: string | null = null;
  public selectedMove4Option2: string | null = null;
  public selectedMove4Option3: string | null = null;
  public selectedMove4Option4: string | null = null;
  public selectedGenerations: GenerationModel[] = [];
  public showMegaEvolutions: boolean = true;
  public showFullyEvolvedOnly: boolean = false;

  public displayedPokemonModels: ListPokemonModel[] = [];
  public filteredPokemonByMoveset: string[] = [];

  public smogonBaseUrl: string = SMOGON_BASE_URL;
  public bulbapediaBaseUrl: string = BULBAPEDIA_BASE_URL;
  public pokeOneCommunityBaseUrl: string = POKEONECOMMUNITY_BASE_URL;
  public pokemonShowDownBaseUrl: string = POKEMONSHOWDOWN_BASE_URL;
  public serebiiBaseUrl: string = SEREBII_BASE_URL;
  public pokemonDbBaseUrl: string = POKEMONDB_BASE_URL;

  public column = PokemonListColumn;
  public sortColumn: PokemonListColumn = PokemonListColumn.PokedexNumber;
  public sortDirection: number = 1;

  private timeOut: any;
  private timeOutDuration: number = 500;

  constructor(
    private pokemonListService: PokemonListService,
    private movesService: MovesService,
    private learnableMovesService: LearnableMovesService
  ) { }

  ngOnInit(): void {
    this.pokemonListService
      .getAll()
      .subscribe(response => {
        this.listPokemonModels = response as IListPokemonModel[];
        this.displayedPokemonModels = this.listPokemonModels.slice();
        this.filteredPokemonByMoveset = this.listPokemonModels.map(p => p.name);
        this.calculateGlobals();
      });

    this.movesService
      .getAllMoveNames()
      .subscribe(response => {
        this.moves = response as string[];
      })
  }

  public onFilterChangedDelayed() {
    this.forceValidStatInputs();

    clearTimeout(this.timeOut);
    this.timeOut = setTimeout(() => {
      this.onFilterChanged();
    }, this.timeOutDuration)
  }

  public onFilterChanged() {
    this.displayedPokemonModels = this.listPokemonModels.slice();

    if (this.searchTerm) {
      this.displayedPokemonModels = this.displayedPokemonModels.filter(p => 
        p.name.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        String(p.pokedexNumber) === this.searchTerm ||
        (p.primaryAbility && p.primaryAbility.toLowerCase().includes(this.searchTerm.toLowerCase())) ||
        (p.secondaryAbility && p.secondaryAbility.toLowerCase().includes(this.searchTerm.toLowerCase())) ||
        (p.hiddenAbility && p.hiddenAbility.toLowerCase().includes(this.searchTerm.toLowerCase())));
    }

    if (this.selectedAvailabilities.length > 0) {
      this.displayedPokemonModels = this.displayedPokemonModels.filter(p => 
        this.selectedAvailabilities.includes(p.availability));
    }

    if (this.selectedPvpTiers.length > 0) {
      this.displayedPokemonModels = this.displayedPokemonModels.filter(p => 
        this.selectedPvpTiers.map(t => t.id).includes(p.pvpTierSortIndex));
    }

    //Double types
    if (
      this.selectedType1 && this.selectedType1 !== SELECT_OPTION_ANY &&
      this.selectedType2 && this.selectedType2 !== SELECT_OPTION_ANY && 
      this.selectedType2 !== SELECT_OPTION_NONE) {
        this.displayedPokemonModels = this.displayedPokemonModels.filter(p => 
          (p.type1 && p.type1 === this.selectedType1 && p.type2 && p.type2 === this.selectedType2) ||
          (p.type1 && p.type1 === this.selectedType2 && p.type2 && p.type2 === this.selectedType1));
    }
    //Only one type required
    else if (
      this.selectedType1 && this.selectedType1 !== SELECT_OPTION_ANY &&
      this.selectedType2 === SELECT_OPTION_ANY) {
        this.displayedPokemonModels = this.displayedPokemonModels.filter(p => 
          (p.type1 && p.type1 === this.selectedType1) || 
          (p.type2 && p.type2 === this.selectedType1));
    }
    //Only one type required (reverse)
    else if (
      this.selectedType2 && this.selectedType2 !== SELECT_OPTION_ANY && this.selectedType2 !== SELECT_OPTION_NONE &&
      this.selectedType1 === SELECT_OPTION_ANY) {
        this.displayedPokemonModels = this.displayedPokemonModels.filter(p => 
          (p.type1 && p.type1 === this.selectedType2) || 
          (p.type2 && p.type2 === this.selectedType2));
    }
    //Specific single types (second type is none)
    else if (
      this.selectedType1 && this.selectedType1 !== SELECT_OPTION_ANY &&
      this.selectedType2 === SELECT_OPTION_NONE) {
        this.displayedPokemonModels = this.displayedPokemonModels.filter(p =>
          (p.type1 && p.type1 === this.selectedType1 && !p.type2) ||
          (p.type2 && p.type2 === this.selectedType1 && !p.type1));
    }
    //Any single type (ANY + NONE)
    else if(this.selectedType1 === SELECT_OPTION_ANY && this.selectedType2 === SELECT_OPTION_NONE) {
      this.displayedPokemonModels = this.displayedPokemonModels.filter(p =>
        (p.type1 && !p.type2) || (!p.type1 && p.type2));
    }

    if (this.selectedAbility) {
      this.displayedPokemonModels = this.displayedPokemonModels.filter(p =>
        (p.primaryAbility && p.primaryAbility === this.selectedAbility) ||
        (p.secondaryAbility && p.secondaryAbility === this.selectedAbility) ||
        (p.hiddenAbility && p.hiddenAbility === this.selectedAbility))
    }

    this.displayedPokemonModels = this.displayedPokemonModels.filter(p => 
      p.atk >= +this.selectedMinAtk && p.atk <= +this.selectedMaxAtk &&
      p.spa >= +this.selectedMinSpa && p.spa <= +this.selectedMaxSpa &&
      p.def >= +this.selectedMinDef && p.def <= +this.selectedMaxDef &&
      p.spd >= +this.selectedMinSpd && p.spd <= +this.selectedMaxSpd &&
      p.spe >= +this.selectedMinSpe && p.spe <= +this.selectedMaxSpe &&
      p.hp >= +this.selectedMinHp && p.hp <= +this.selectedMaxHp &&
      p.statTotal >= +this.selectedMinTotal && p.statTotal <= +this.selectedMaxTotal);

    if (this.selectedGenerations.length > 0) {
      this.displayedPokemonModels = this.displayedPokemonModels.filter(p => 
        this.selectedGenerations.map(g => g.id).includes(p.generation));
    }

    if (!this.showMegaEvolutions) {
      this.displayedPokemonModels = this.displayedPokemonModels.filter(p => 
        !p.isMega);
    }

    if (this.showFullyEvolvedOnly) {
      this.displayedPokemonModels = this.displayedPokemonModels.filter(p =>
        p.isFullyEvolved);
    }

    this.displayedPokemonModels = this.displayedPokemonModels.filter(p => 
      this.filteredPokemonByMoveset.includes(p.name));

    this.applySorting();
  }

  public onMoveSetFilterChanged() {
    if (
      !this.selectedMove1Option1 && !this.selectedMove1Option2 && !this.selectedMove1Option3 && !this.selectedMove1Option4 &&
      !this.selectedMove2Option1 && !this.selectedMove2Option2 && !this.selectedMove2Option3 && !this.selectedMove2Option4 &&
      !this.selectedMove3Option1 && !this.selectedMove3Option2 && !this.selectedMove3Option3 && !this.selectedMove3Option4 &&
      !this.selectedMove4Option1 && !this.selectedMove4Option2 && !this.selectedMove4Option3 && !this.selectedMove4Option4) {
        this.filteredPokemonByMoveset = this.listPokemonModels.map(p => p.name);
        this.onFilterChanged();
        return;
      }

    this.learnableMovesService
      .getAllPokemonForMoveSet(
        this.selectedMove1Option1, this.selectedMove1Option2, this.selectedMove1Option3, this.selectedMove1Option4,
        this.selectedMove2Option1, this.selectedMove2Option2, this.selectedMove2Option3, this.selectedMove2Option4,
        this.selectedMove3Option1, this.selectedMove3Option2, this.selectedMove3Option3, this.selectedMove3Option4,
        this.selectedMove4Option1, this.selectedMove4Option2, this.selectedMove4Option3, this.selectedMove4Option4
      )
      .subscribe(response => {
        console.log(response);
        this.filteredPokemonByMoveset = response as string[];
        this.onFilterChanged();
      })
  }

  public trackById(index: number, item: IListPokemonModel): number {
    return item.id;
  }

  public sortNumber(sortDirection: number) {
    this.sortColumn = PokemonListColumn.PokedexNumber;
    this.sortDirection = sortDirection;

    this.displayedPokemonModels = this.displayedPokemonModels
      .slice()
      .sort((n1,n2) => {
        if (n1.pokedexNumber > n2.pokedexNumber) {
            return sortDirection * 1;
        }
    
        if (n1.pokedexNumber < n2.pokedexNumber) {
            return sortDirection * -1;
        }
    
        if (n1.resourceName > n2.resourceName) {
          return sortDirection * 1;
        }

        if (n1.resourceName < n2.resourceName) {
          return sortDirection * -1;
        }

        return 0;
      }
    );
  }

  public sortName(sortDirection: number) {
    this.sortColumn = PokemonListColumn.Name;
    this.sortDirection = sortDirection;

    this.displayedPokemonModels = this.displayedPokemonModels
      .slice()
      .sort((n1,n2) => {
        if (n1.name > n2.name) {
            return sortDirection * 1;
        }
    
        if (n1.name < n2.name) {
            return sortDirection * -1;
        }
    
        return 0;
    });
  }

  public sortAttack(sortDirection: number) {
    this.sortColumn = PokemonListColumn.Atk;
    this.sortDirection = sortDirection;

    this.displayedPokemonModels = this.displayedPokemonModels
      .slice()
      .sort((n1,n2) => sortDirection * (n2.atk - n1.atk));
  }

  public sortSpecialAttack(sortDirection: number) {
    this.sortColumn = PokemonListColumn.Spa;
    this.sortDirection = sortDirection;

    this.displayedPokemonModels = this.displayedPokemonModels
      .slice()
      .sort((n1,n2) => sortDirection * (n2.spa - n1.spa));
  }

  public sortDefense(sortDirection: number) {
    this.sortColumn = PokemonListColumn.Def;
    this.sortDirection = sortDirection;

    this.displayedPokemonModels = this.displayedPokemonModels
      .slice()
      .sort((n1,n2) => sortDirection * (n2.def - n1.def));
  }

  public sortSpecialDefense(sortDirection: number) {
    this.sortColumn = PokemonListColumn.Spd;
    this.sortDirection = sortDirection;

    this.displayedPokemonModels = this.displayedPokemonModels
      .slice()
      .sort((n1,n2) => sortDirection * (n2.spd - n1.spd));
  }

  public sortSpeed(sortDirection: number) {
    this.sortColumn = PokemonListColumn.Spe;
    this.sortDirection = sortDirection;

    this.displayedPokemonModels = this.displayedPokemonModels
      .slice()
      .sort((n1,n2) => sortDirection * (n2.spe - n1.spe));
  }

  public sortHitPoints(sortDirection: number) {
    this.sortColumn = PokemonListColumn.Hp;
    this.sortDirection = sortDirection;

    this.displayedPokemonModels = this.displayedPokemonModels
      .slice()
      .sort((n1,n2) => sortDirection * (n2.hp - n1.hp));
  }

  public sortTotal(sortDirection: number) {
    this.sortColumn = PokemonListColumn.StatTotal;
    this.sortDirection = sortDirection;

    this.displayedPokemonModels = this.displayedPokemonModels
      .slice()
      .sort((n1,n2) => sortDirection * (n2.statTotal - n1.statTotal));
  }

  public sortPvp(sortDirection: number) {
    this.sortColumn = PokemonListColumn.PvpTier;
    this.sortDirection = sortDirection;

    this.displayedPokemonModels = this.displayedPokemonModels
      .slice()
      .sort((n1,n2) => {
        if (n1.pvpTierSortIndex > n2.pvpTierSortIndex) {
            return sortDirection * 1;
        }
    
        if (n1.pvpTierSortIndex < n2.pvpTierSortIndex) {
            return sortDirection * -1;
        }
    
        return 0;
    });
  }

  private calculateGlobals() {
    this.listPokemonModels.forEach(pokemon => {
      if (this.maxAtk < pokemon.atk) this.maxAtk = pokemon.atk;
      if (this.maxSpa < pokemon.spa) this.maxSpa = pokemon.spa;
      if (this.maxDef < pokemon.def) this.maxDef = pokemon.def;
      if (this.maxSpd < pokemon.spd) this.maxSpd = pokemon.spd;
      if (this.maxSpe < pokemon.spe) this.maxSpe = pokemon.spe;
      if (this.maxHp < pokemon.hp) this.maxHp = pokemon.hp;
      if (this.maxTotal < pokemon.statTotal) this.maxTotal = pokemon.statTotal;

      if (!this.pvpTiers.find(p => p.id === pokemon.pvpTierSortIndex)) {
        this.pvpTiers.push({ id: pokemon.pvpTierSortIndex, name: pokemon.pvpTier ?? 'Untiered' });
      }

      if (!this.types1.includes(pokemon.type1)) {
        this.types1.push(pokemon.type1);
        this.types2.push(pokemon.type1);
      }
      if (pokemon.type2 && !this.types1.includes(pokemon.type2)) {
        this.types1.push(pokemon.type2);
        this.types2.push(pokemon.type2);
      }

      if (!this.abilities.includes(pokemon.primaryAbility)) {
        this.abilities.push(pokemon.primaryAbility);
      }
      if (pokemon.secondaryAbility && !this.abilities.includes(pokemon.secondaryAbility)) {
        this.abilities.push(pokemon.secondaryAbility);
      }
      if (pokemon.hiddenAbility && !this.abilities.includes(pokemon.hiddenAbility)) {
        this.abilities.push(pokemon.hiddenAbility);
      }

      if (!this.availabilities.includes(pokemon.availability)) {
        this.availabilities.push(pokemon.availability);
      }
    })

    this.pvpTiers.sort((n1, n2) => n1.id - n2.id);
    this.availabilities.sort((n1, n2) => n1 > n2 ? 1 : -1);
    this.abilities.sort((n1, n2) => n1 > n2 ? 1 : -1);
    this.types1.sort((n1, n2) => 
      n1 === SELECT_OPTION_ANY ? -1 : n2 === SELECT_OPTION_ANY ? 1 : 
      n1 === SELECT_OPTION_NONE ? -1 : n2 === SELECT_OPTION_NONE ? 1 :
      n1 > n2 ? 1 : -1);
    this.types2.sort((n1, n2) => 
      n1 === SELECT_OPTION_ANY ? -1 : n2 === SELECT_OPTION_ANY ? 1 : 
      n1 === SELECT_OPTION_NONE ? -1 : n2 === SELECT_OPTION_NONE ? 1 :
      n1 > n2 ? 1 : -1);

    this.selectedMaxAtk = this.maxAtk;
    this.selectedMaxSpa = this.maxSpa;
    this.selectedMaxDef = this.maxDef;
    this.selectedMaxSpd = this.maxSpd;
    this.selectedMaxSpe = this.maxSpe;
    this.selectedMaxHp = this.maxHp;
    this.selectedMaxTotal = this.maxTotal;
  }

  private forceValidStatInputs() {
    if (isNaN(+this.selectedMinAtk) || this.selectedMinAtk < 0 || this.selectedMinAtk > this.maxAtk) {
      this.selectedMinAtk = new String("0"); //new String is required to trigger change detection.
    }
    else {
      this.selectedMinAtk = +this.selectedMinAtk;
    }

    if (isNaN(+this.selectedMaxAtk) || this.selectedMaxAtk < 0 || this.selectedMaxAtk > this.maxAtk) {
      this.selectedMaxAtk = new String(this.maxAtk);
    }
    else {
      this.selectedMaxAtk = +this.selectedMaxAtk;
    }

    
    if (isNaN(+this.selectedMinSpa) || this.selectedMinSpa < 0 || this.selectedMinSpa > this.maxSpa) {
      this.selectedMinSpa = new String("0"); //new String is required to trigger change detection.
    }
    else {
      this.selectedMinSpa = +this.selectedMinSpa;
    }

    if (isNaN(+this.selectedMaxSpa) || this.selectedMaxSpa < 0 || this.selectedMaxSpa > this.maxSpa) {
      this.selectedMaxSpa = new String(this.maxSpa);
    }
    else {
      this.selectedMaxSpa = +this.selectedMaxSpa;
    }


    if (isNaN(+this.selectedMinDef) || this.selectedMinDef < 0 || this.selectedMinDef > this.maxDef) {
      this.selectedMinDef = new String("0"); //new String is required to trigger change detection.
    }
    else {
      this.selectedMinDef = +this.selectedMinDef;
    }

    if (isNaN(+this.selectedMaxDef) || this.selectedMaxDef < 0 || this.selectedMaxDef > this.maxDef) {
      this.selectedMaxDef = new String(this.maxDef);
    }
    else {
      this.selectedMaxDef = +this.selectedMaxDef;
    }


    if (isNaN(+this.selectedMinSpd) || this.selectedMinSpd < 0 || this.selectedMinSpd > this.maxSpd) {
      this.selectedMinSpd = new String("0"); //new String is required to trigger change detection.
    }
    else {
      this.selectedMinSpd = +this.selectedMinSpd;
    }

    if (isNaN(+this.selectedMaxSpd) || this.selectedMaxSpd < 0 || this.selectedMaxSpd > this.maxSpd) {
      this.selectedMaxSpd = new String(this.maxSpd);
    }
    else {
      this.selectedMaxSpd = +this.selectedMaxSpd;
    }

    
    if (isNaN(+this.selectedMinSpe) || this.selectedMinSpe < 0 || this.selectedMinSpe > this.maxSpe) {
      this.selectedMinSpe = new String("0"); //new String is required to trigger change detection.
    }
    else {
      this.selectedMinSpe = +this.selectedMinSpe;
    }

    if (isNaN(+this.selectedMaxSpe) || this.selectedMaxSpe < 0 || this.selectedMaxSpe > this.maxSpe) {
      this.selectedMaxSpe = new String(this.maxSpe);
    }
    else {
      this.selectedMaxSpe = +this.selectedMaxSpe;
    }


    if (isNaN(+this.selectedMinHp) || this.selectedMinHp < 0 || this.selectedMinHp > this.maxHp) {
      this.selectedMinHp = new String("0"); //new String is required to trigger change detection.
    }
    else {
      this.selectedMinHp = +this.selectedMinHp;
    }

    if (isNaN(+this.selectedMaxHp) || this.selectedMaxHp < 0 || this.selectedMaxHp > this.maxHp) {
      this.selectedMaxHp = new String(this.maxHp);
    }
    else {
      this.selectedMaxHp = +this.selectedMaxHp;
    }


    if (isNaN(+this.selectedMinTotal) || this.selectedMinTotal < 0 || this.selectedMinTotal > this.maxTotal) {
      this.selectedMinTotal = new String("0"); //new String is required to trigger change detection.
    }
    else {
      this.selectedMinTotal = +this.selectedMinTotal;
    }

    if (isNaN(+this.selectedMaxTotal) || this.selectedMaxTotal < 0 || this.selectedMaxTotal > this.maxTotal) {
      this.selectedMaxTotal = new String(this.maxTotal);
    }
    else {
      this.selectedMaxTotal = +this.selectedMaxTotal;
    }
  }

  private applySorting() {
    switch (this.sortColumn) {
      case PokemonListColumn.PokedexNumber: this.sortNumber(this.sortDirection); break;
      case PokemonListColumn.Name: this.sortName(this.sortDirection); break;
      case PokemonListColumn.Atk: this.sortAttack(this.sortDirection); break;
      case PokemonListColumn.Spa: this.sortSpecialAttack(this.sortDirection); break;
      case PokemonListColumn.Def: this.sortDefense(this.sortDirection); break;
      case PokemonListColumn.Spd: this.sortSpecialDefense(this.sortDirection); break;
      case PokemonListColumn.Spe: this.sortSpeed(this.sortDirection); break;
      case PokemonListColumn.Hp: this.sortHitPoints(this.sortDirection); break;
      case PokemonListColumn.StatTotal: this.sortTotal(this.sortDirection); break;
    }
  }
}