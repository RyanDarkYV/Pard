import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginFormComponent } from './login-form/login-form.component';
import { RegistrationFormComponent } from './registration-form/registration-form.component';

export const moduleRoutes: Routes = [
    { path: 'login', component: LoginFormComponent },
    { path: 'register', component: RegistrationFormComponent }
];

@NgModule({
    imports: [
        RouterModule.forChild(moduleRoutes)
    ],
    exports: [
        RouterModule
    ]
})

export class AccountRoutingModule { }
