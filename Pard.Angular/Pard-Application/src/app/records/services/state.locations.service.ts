import { Injectable } from '@angular/core';

import { Location } from '../models/location';
@Injectable()


export class LocationsStateService {
    data: Location;

    setState(location: Location) {
        this.data = location;
    }

    getState(): Location {
        return this.data;
    }

    clearState(): void {
        this.data = null;
    }
}

