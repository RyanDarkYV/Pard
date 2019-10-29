import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Record } from '../models/record';
@Injectable()


export class RecordsStateService {
    data: Record;

    setState(record: Record): void {
        this.data = record;
        console.log(JSON.stringify(this.data));
    }

    getState(): Record {
        console.log(JSON.stringify(this.data));
        return this.data;
    }

    clearState(): void {
        this.data = null;
    }
}

