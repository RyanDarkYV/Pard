import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';
import { BehaviorSubject } from 'rxjs';

import { ConfigService } from '../utils/config.service';


import { Credentials } from '../models/credentials';
import { UserRegistration } from '../models/user.registration';
import { BaseService } from '../services/base.service';
import { map, filter, switchMap, catchError } from 'rxjs/operators';

@Injectable()

export class RoleService extends BaseService {
    baseUrl: string = '';

    private authNavStatusSource = new BehaviorSubject<boolean>(false);
    authNavStatus$ = this.authNavStatusSource.asObservable();

    private loggedIn: boolean = false;

    constructor(private http: HttpClient, private configService: ConfigService) {
        super();
        this.loggedIn = !!localStorage.getItem('auth_token');
        this.authNavStatusSource.next(this.loggedIn);
        this.baseUrl = configService.getApiUrl();
    }

    login(credentials: Credentials) {
        let httpHeaders = new HttpHeaders();
        httpHeaders.append('Content-Type', 'application/json');
        httpHeaders.append('Access-Control-Allow-Origin', '*');

        return this.http.post(this.baseUrl + '/auth/login', credentials, { headers: httpHeaders } )
        .pipe(map(res => {
            localStorage.setItem('auth_token', res['auth_token']);
            this.loggedIn = true;
            this.authNavStatusSource.next(true);
            return true;
        })).pipe(catchError(this.handleError));
    }
    getAdminData(): Observable<string> {
        let headers = new HttpHeaders();
        headers = headers.append('Content-Type', 'application/json');
        headers = headers.append('Access-Control-Allow-Origin', '*');
        let authToken = localStorage.getItem('auth_token');
        headers = headers.append('Authorization', `Bearer ${authToken}`);

        return this.http.get<string>(this.baseUrl + `/admin/get`, { headers });
    }

    getSuperAdminData(): Observable<string> {
        let headers = new HttpHeaders();
        headers = headers.append('Content-Type', 'application/json');
        headers = headers.append('Access-Control-Allow-Origin', '*');
        let authToken = localStorage.getItem('auth_token');
        headers = headers.append('Authorization', `Bearer ${authToken}`);

        return this.http.get<string>(this.baseUrl + `/super/get`, { headers });
    }
}
