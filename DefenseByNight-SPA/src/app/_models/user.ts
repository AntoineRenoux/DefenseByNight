import { Photo } from './photo';
import { Health } from './health';

export class User {
    id: string;
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    birthDate: Date;
    createdDate: Date;
    lastActive: Date;
    address: string;
    city: string;
    zipcode: string;
    photo: Photo;
    health: Health;
}
