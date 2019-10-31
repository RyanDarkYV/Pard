import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AgmCoreModule, GoogleMapsAPIWrapper } from '@agm/core';

import { SharedModule } from '../shared/modules/shared.module';
import { CreateFormComponent } from './create-form/create-form.component';
import { UpdateFormComponent } from './update-form/update-form.component';
import { DashboardFormComponent } from './dashboard-form/dashboard-form.component';
import { RecordsRoutingModule } from './records-routing.module';
import { RecordService} from './services/records.services';
import { RecordsStateService } from './services/state.records.service';
import { MapComponent } from './map/map.component';
import { LocationsStateService } from './services/state.locations.service';
import { OverviewComponent } from './overview/overview.component';
import { ArchiveComponent } from './archive/archive.component';
import { ArchiveService } from './services/archive.service';

@NgModule({
  declarations: [
    CreateFormComponent,
    UpdateFormComponent,
    DashboardFormComponent,
    MapComponent,
    OverviewComponent,
    ArchiveComponent
  ],
  imports: [
    CommonModule,
    AgmCoreModule,
    RecordsRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule
  ],
  providers: [
    RecordService,
    ArchiveService,
    RecordsStateService,
    LocationsStateService
  ]
})
export class RecordsModule { }
