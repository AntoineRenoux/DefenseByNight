import { Photo } from './photo';

export class User {
    id: number;
    username: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    birthDate: Date;
    created: Date;
    lastActive: Date;
    photoUrl: string;
    photos?: Photo[];
}
