import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError} from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Record } from '../models/record';
import { ConfigService } from '../../shared/utils/config.service';
import { BaseService } from '../../shared/services/base.service';

@Injectable()

export class RecordService extends BaseService {
    baseUrl = '';

    constructor(private http: HttpClient, private configService: ConfigService) {
        super();
        this.baseUrl = configService.getApiUrl();
    }

    getRecord(title: string): Observable<Record> {
        let headers = new HttpHeaders();
        headers = headers.append('Content-Type', 'application/json');
        headers = headers.append('Access-Control-Allow-Origin', '*');
        const authToken = localStorage.getItem('auth_token');
        headers = headers.append('Authorization', `Bearer ${authToken}`);

        return this.http.get<Record>(this.baseUrl + `/records/getrecord/${title}`, { headers });
    }

    getAllFinishedRecordsForUser(): Observable<Record[]> {
        let headers = new HttpHeaders();
        headers = headers.append('Content-Type', 'application/json');
        headers = headers.append('Access-Control-Allow-Origin', '*');
        const authToken = localStorage.getItem('auth_token');
        headers = headers.append('Authorization', `Bearer ${authToken}`);

        return this.http.get<Record[]>(this.baseUrl + `/records/GetAllFinishedRecordsForUser`, { headers });
    }

    getAllUnfinishedRecords(): Observable<Record[]> {
        let headers = new HttpHeaders();
        headers = headers.append('Content-Type', 'application/json');
        headers = headers.append('Access-Control-Allow-Origin', '*');
        const authToken = localStorage.getItem('auth_token');
        headers = headers.append('Authorization', `Bearer ${authToken}`);

        return this.http.get<Record[]>(this.baseUrl + `/records/getallunfinishedrecordsforuser`, { headers });
    }

    createRecord(record: Record) {
        let headers = new HttpHeaders();
        headers = headers.append('Content-Type', 'application/json');
        headers = headers.append('Access-Control-Allow-Origin', '*');
        const authToken = localStorage.getItem('auth_token');
        headers = headers.append('Authorization', `Bearer ${authToken}`);

        console.log(JSON.stringify(record));
        return this.http.post(this.baseUrl + '/records/create', record, { headers });
    }

    updateRecord(record: Record) {
        let headers = new HttpHeaders();
        headers = headers.append('Content-Type', 'application/json');
        headers = headers.append('Access-Control-Allow-Origin', '*');
        const authToken = localStorage.getItem('auth_token');
        headers = headers.append('Authorization', `Bearer ${authToken}`);

        return this.http.put(this.baseUrl + '/records/update', record, { headers });
    }

    softDeleteRecord(record: Record) {
        let headers = new HttpHeaders();
        headers = headers.append('Content-Type', 'application/json');
        headers = headers.append('Access-Control-Allow-Origin', '*');
        const authToken = localStorage.getItem('auth_token');
        headers = headers.append('Authorization', `Bearer ${authToken}`);

        return this.http.delete(this.baseUrl + `/records/delete?id=${record.id}`, { headers });
    }
}
