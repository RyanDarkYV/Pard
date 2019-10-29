import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RecaptchaModule, RecaptchaFormsModule } from 'ng-recaptcha';

import { SharedModule } from '../shared/modules/shared.module';
import { RegistrationFormComponent } from './registration-form/registration-form.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { AccountRoutingModule} from './account-routing.module';

import { UserService } from '../shared/services/user.service';
import { RoleService } from '../shared/services/roles.service';

@NgModule({
  declarations: [
    RegistrationFormComponent,
    LoginFormComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    SharedModule,
    AccountRoutingModule,
    RecaptchaModule,
    RecaptchaFormsModule
  ],
  providers: [
    UserService,
    RoleService
  ]
})
export class AccountModule { }
