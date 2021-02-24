import { User } from "./user";

export class UserParams {
    gender: string;
    pageNumber = 1;
    pageSize = 5;
    searchLocation = 'Reunion';
    searchInstrument = 'Guitar';
    sortBy = 'lastActive';

    constructor(UserParams: User) {

    }
}