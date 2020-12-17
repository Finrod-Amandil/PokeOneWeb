import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { ILearnableMoveModel } from 'src/models/learnable-move.model';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class LearnableMovesService extends BaseService<ILearnableMoveModel> {
    constructor (http: HttpClient) {
        super(http);
    }

    public getPathSegment(): string {
        return 'GetAllMoves';
    }

    public getAllPokemonForMoveSet(
        m11: string | null, m12: string | null, m13: string | null, m14: string | null,
        m21: string | null, m22: string | null, m23: string | null, m24: string | null,
        m31: string | null, m32: string | null, m33: string | null, m34: string | null,
        m41: string | null, m42: string | null, m43: string | null, m44: string | null): Observable<string[]> {

        var url = `${this.baseUrl}/GetAllPokemonForMoveSet?`;
        url += `m11=${m11??''}&m12=${m12??''}&m13=${m13??''}&m14=${m14??''}&`
        url += `m21=${m21??''}&m22=${m22??''}&m23=${m23??''}&m24=${m24??''}&`
        url += `m31=${m31??''}&m32=${m32??''}&m33=${m33??''}&m34=${m34??''}&`
        url += `m41=${m41??''}&m42=${m42??''}&m43=${m43??''}&m44=${m44??''}`

        return this.http.get<string[]>(url, {
            headers: this.httpHeaders
        });
    }
}