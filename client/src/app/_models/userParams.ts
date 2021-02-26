import { User } from "./user";

export class UserParams {
    gender: string;
    pageNumber = 1;
    pageSize = 5;
    searchLocation = '';
    searchInstrument = '';
    searchExperience = '';
    sortBy = 'lastActive';

    constructor(user: User) {

    }
}