export interface INatureOptionModel {
    natureName: string;
    natureEffect: string;
}

export class NatureOptionModel implements INatureOptionModel {
    natureName = '';
    natureEffect = '';
}
