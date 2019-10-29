import { Location } from './location';

export class Record {
    id: string;
    title: string;
    description: string;
    isDone: boolean;
    addedAt: Date;
    finishedAt?: Date;
    userId: string;
    location?: Location;

    constructor(title: string, description: string, isDone: boolean) {
        this.title = title;
        this.description = description;
        this.isDone = isDone;
    }
}
