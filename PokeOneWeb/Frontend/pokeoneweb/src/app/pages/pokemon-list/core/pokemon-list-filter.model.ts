import { SELECT_OPTION_ANY } from "src/app/core/constants/string.constants";
import { GenerationModel } from "src/app/core/models/generation.model";
import { IMoveNameModel } from "src/app/core/models/move-name.model";
import { PvpTierModel } from "src/app/core/models/pvp-tier.model";

export class PokemonListFilterModel {
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
    public selectedMove1Option1: IMoveNameModel | null = null;
    public selectedMove1Option2: IMoveNameModel | null = null;
    public selectedMove1Option3: IMoveNameModel | null = null;
    public selectedMove1Option4: IMoveNameModel | null = null;
    public selectedMove2Option1: IMoveNameModel | null = null;
    public selectedMove2Option2: IMoveNameModel | null = null;
    public selectedMove2Option3: IMoveNameModel | null = null;
    public selectedMove2Option4: IMoveNameModel | null = null;
    public selectedMove3Option1: IMoveNameModel | null = null;
    public selectedMove3Option2: IMoveNameModel | null = null;
    public selectedMove3Option3: IMoveNameModel | null = null;
    public selectedMove3Option4: IMoveNameModel | null = null;
    public selectedMove4Option1: IMoveNameModel | null = null;
    public selectedMove4Option2: IMoveNameModel | null = null;
    public selectedMove4Option3: IMoveNameModel | null = null;
    public selectedMove4Option4: IMoveNameModel | null = null;
    public selectedGenerations: GenerationModel[] = [];
    public showMegaEvolutions: boolean = true;
    public showFullyEvolvedOnly: boolean = false;
}