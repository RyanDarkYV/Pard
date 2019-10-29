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

export class UserService extends BaseService {
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

    logout() {
        localStorage.removeItem('auth_token');
        this.loggedIn = false;
        this.authNavStatusSource.next(false);
    }

    register(userRegistration: UserRegistration): Observable<UserRegistration> {
        let httpHeaders = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');

        return this.http.post(this.baseUrl + '/accounts/register', JSON.stringify(userRegistration), { headers: httpHeaders })
        .pipe(map(res => {
            return userRegistration;
        }))
        .pipe(catchError(this.handleError));
    }
}
