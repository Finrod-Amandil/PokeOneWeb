import { httpResource } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Move } from '../../models/api/move';

@Injectable({
    providedIn: 'root',
})
export class MoveDataService {
    private movesResource = httpResource<Move[]>(() => `${environment.baseUrl}/moves.json`, {
        defaultValue: [],
    });

    moves = this.movesResource.asReadonly();
}
