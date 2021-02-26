import { Instrument } from './instrument';
import { Photo } from './photo';

export interface Member {
    id: number;
    username: string;
    photoUrl: string;
    age: number;
    knownAs: string;
    created: Date;
    lastActive: Date;
    gender: string;
    introduction: string;
    searchingFor: string;
    background: string;
    city: string;
    country: string;
    experienceLevel: string;
    photos: Photo[];
    preferredInstruments: Instrument[];
}

