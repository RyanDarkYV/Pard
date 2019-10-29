import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { moduleRoutes } from './account/account-routing.module';
import { recordsModuleRoutes} from './records/records-routing.module';

import { HomeComponent } from './home/home.component';
import { DashboardFormComponent } from './records/dashboard-form/dashboard-form.component';


const routes: Routes = [
  { path: 'home', component: HomeComponent},
  { path: 'account', children: moduleRoutes},
  { path: 'records', component: DashboardFormComponent, children: recordsModuleRoutes},
  { path: '', redirectTo: '/home', pathMatch: 'full'},
  { path: '**', redirectTo: '/home'}

];

@NgModule({
  imports: [
    RouterModule.forRoot(
      routes
       // <-- debugging purposes only
    )
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule {}
