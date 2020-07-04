import { Affilate } from './affiliate';
import { Chronicle } from './Chronicle';
import { Archetype } from './archetype';
import { Focus } from './focus';

export class Character {
    characterKey: string;

    chronicle: Chronicle;
    name: string;
    sect: Affilate;

    archetype: Archetype;
    concept: string;

    physicalAttribut: number;
    socialAttribut: number;
    mentalAttribut: number;

    physicalFocus: Focus[];
    socialFocus: Focus[];
    mentalFocus: Focus[];
}
