import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute} from '@angular/router';
import { Subscription } from 'rxjs';

import { Credentials } from '../../shared/models/credentials';
import { UserService } from '../../shared/services/user.service';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit, OnDestroy {
  private subscription: Subscription;

  brandNew: boolean;
  isCaptcha: boolean = false;
  errors: string;
  isRequesting: boolean;
  submitted: boolean;
  credentials: Credentials = { userName: '', password: ''};
  constructor(private userService: UserService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.subscription = this.activatedRoute.queryParams.subscribe(
      (param: any) => {
         this.credentials.userName = param['userName'];
      });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
  
  public resolved(captchaResponse: string) {
    console.log(`Resolved captcha with response: ${captchaResponse}`);
    this.isCaptcha = true;
  }

  login({value, valid}: { value: Credentials, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';
    let body: Credentials = new Credentials(value.userName, value.password);

    if (valid) {
      this.userService.login(body)
      .pipe(finalize(() => this.isRequesting = false))
      .subscribe(res => {
        if (res) {
          this.router.navigate(['records/dashboard']);
        }
      },
      error => this.errors = error);
    }
  }
}
