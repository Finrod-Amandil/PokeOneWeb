export interface IAttackEffectivityModel {
    typeName: string;
    effectivity: number;
}

export class AttackEffectivityModel implements IAttackEffectivityModel {
    typeName = '';
    effectivity = 0;
}
