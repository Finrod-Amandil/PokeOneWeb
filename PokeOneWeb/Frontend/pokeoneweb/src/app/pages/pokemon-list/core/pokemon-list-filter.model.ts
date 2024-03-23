import { SELECT_OPTION_ANY } from 'src/app/core/constants/string.constants';
import { GenerationModel } from 'src/app/core/models/service/generation.model';
import { IMoveNameModel } from 'src/app/core/models/api/move.model';
import { PvpTierModel } from 'src/app/core/models/service/pvp-tier.model';

export class PokemonListFilterModel {
    public searchTerm = '';
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
    public selectedMoveOption1: IMoveNameModel | null = null;
    public selectedMoveOption2: IMoveNameModel | null = null;
    public selectedMoveOption3: IMoveNameModel | null = null;
    public selectedMoveOption4: IMoveNameModel | null = null;
    public selectedGenerations: GenerationModel[] = [];
    public showMegaEvolutions = true;
    public showFullyEvolvedOnly = false;
}
