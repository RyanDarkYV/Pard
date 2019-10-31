import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Record } from '../models/record';
import { ConfigService } from '../../shared/utils/config.service';
import { BaseService } from '../../shared/services/base.service';

@Injectable()

export class ArchiveService extends BaseService {
    baseUrl = '';

    constructor(private http: HttpClient, private configService: ConfigService) {
        super();
        this.baseUrl = configService.getApiUrl();
    }

    restoreRecord(record: Record) {
        let headers = new HttpHeaders();
        headers = headers.append('Content-Type', 'application/json');
        headers = headers.append('Access-Control-Allow-Origin', '*');
        const authToken = localStorage.getItem('auth_token');
        headers = headers.append('Authorization', `Bearer ${authToken}`);

        const body = {
            id: record.id
        };

        return this.http.put(this.baseUrl + '/archive/restore', body, { headers });
    }

    getArchivedRecords(): Observable<Record[]> {
        let headers = new HttpHeaders();
        headers = headers.append('Content-Type', 'application/json');
        headers = headers.append('Access-Control-Allow-Origin', '*');
        const authToken = localStorage.getItem('auth_token');
        headers = headers.append('Authorization', `Bearer ${authToken}`);

        return this.http.get<Record[]>(this.baseUrl + `/archive/get`, { headers });
    }

    DeleteRecord(record: Record) {
        let headers = new HttpHeaders();
        headers = headers.append('Content-Type', 'application/json');
        headers = headers.append('Access-Control-Allow-Origin', '*');
        const authToken = localStorage.getItem('auth_token');
        headers = headers.append('Authorization', `Bearer ${authToken}`);

        return this.http.delete(this.baseUrl + `/archive/delete?id=${record.id}`, { headers });
    }
}
