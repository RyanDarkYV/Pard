import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UserRegistration } from '../../shared/models/user.registration';
import { UserService } from '../../shared/services/user.service';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.css']
})
export class RegistrationFormComponent implements OnInit {

  errors: string;
  isRequesting: boolean;
  isCaptcha: boolean = false;
  submitted: boolean = false;

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
  }

  public resolved(captchaResponse: string) {
    console.log(`Resolved captcha with response: ${captchaResponse}`);
    this.isCaptcha = true;
  }
  registerUser({value, valid}: {value: UserRegistration, valid: boolean}) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';
    let body: UserRegistration = new UserRegistration(value.email, value.login, value.password, value.firstName, value.lastName);

    if (valid) {
      this.userService.register(body)
      .pipe(finalize(() => this.isRequesting = false))
      .subscribe( res => {
        if (res) {
          this.router.navigate(['/login'], {queryParams: {brandNew: true, userName: value.login }});
        }
      },
      error => this.errors = error);
    }
  }

}
