import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Record } from '../models/record';
@Injectable()


export class RecordsStateService {
    data: Record;

    setState(record: Record): void {
        this.data = record;
    }

    getState(): Record {
        return this.data;
    }

    clearState(): void {
        this.data = null;
    }
}

