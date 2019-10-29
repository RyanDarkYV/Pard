import { Injectable } from '@angular/core';

@Injectable()
export class ConfigService {
    apiUrl: string;

    constructor() {
        this.apiUrl = 'http://localhost:5066/api';
    }

    getApiUrl() {
        return this.apiUrl;
    }
}
