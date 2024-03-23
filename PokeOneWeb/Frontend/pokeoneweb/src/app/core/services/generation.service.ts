import { Injectable } from '@angular/core';
import { IGenerationModel } from '../models/service/generation.model';

@Injectable({
    providedIn: 'root'
})
export class GenerationService {
    public getGenerations(): IGenerationModel[] {
        return [
            { id: 1, name: 'Gen. 1 (Kanto; Red / Green / Blue / Yellow)' },
            { id: 2, name: 'Gen. 2 (Johto; Gold / Silver / Crystal)' },
            { id: 3, name: 'Gen. 3 (Hoenn; Ruby / Sapphire / Emerald, FireRed / LeafGreen)' },
            { id: 4, name: 'Gen. 4 (Sinnoh; Diamond / Perl / Platinum, HeartGold / SoulSilver)' },
            { id: 5, name: 'Gen. 5 (Unova; Black / White, Black 2 / White 2)' },
            { id: 6, name: 'Gen. 6 (Kalos; X/Y, Omega Ruby / Alpha Sapphire)' },
            { id: 7, name: "Gen. 7 (Alola; Sun / Moon, Ultra Sun / Ultra Moon, Let's Go!)" }
        ];
    }
}
