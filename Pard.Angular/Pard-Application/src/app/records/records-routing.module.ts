import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CreateFormComponent } from './create-form/create-form.component';
import { DashboardFormComponent } from './dashboard-form/dashboard-form.component';
import { UpdateFormComponent } from './update-form/update-form.component';
import { OverviewComponent } from './overview/overview.component';

export const recordsModuleRoutes: Routes = [
    { path: 'dashboard', component: DashboardFormComponent },
    { path: 'create', component: CreateFormComponent },
    { path: 'update', component: UpdateFormComponent},
    { path: 'overview', component: OverviewComponent}
];

@NgModule({
    imports: [
        RouterModule.forChild(recordsModuleRoutes)
    ],
    exports: [
        RouterModule
    ]
})

export class RecordsRoutingModule { }
