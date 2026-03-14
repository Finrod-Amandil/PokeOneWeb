import { SELECT_OPTION_NONE } from '../../constants/string.constants';

export interface Nature {
    name: string;
    effect: string;
    attack: number;
    specialAttack: number;
    defense: number;
    specialDefense: number;
    speed: number;
}

export const defaultNature: Nature = {
    name: SELECT_OPTION_NONE,
    effect: '',
    attack: 0,
    specialAttack: 0,
    defense: 0,
    specialDefense: 0,
    speed: 0,
};
