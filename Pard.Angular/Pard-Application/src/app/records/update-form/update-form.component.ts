import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';

import { RecordService } from '../services/records.services';
import { RecordsStateService } from '../services/state.records.service';
import { Record } from '../models/record';
import { first } from 'rxjs/operators';
import { LocationsStateService } from '../services/state.locations.service';

@Component({
  selector: 'app-update-form',
  templateUrl: './update-form.component.html',
  styleUrls: ['./update-form.component.css']
})
export class UpdateFormComponent implements OnInit {

  editForm: FormGroup;
  record: Record;

  constructor(private formBuilder: FormBuilder,
              private router: Router,
              private recordService: RecordService,
              private stateService: RecordsStateService,
              private locationStateService: LocationsStateService) { }

  ngOnInit() {
    this.record = this.stateService.getState();
    this.locationStateService.setState(this.record.location);
    this.record.location = this.locationStateService.data;
    console.log(JSON.stringify(this.record));

    this.editForm = this.formBuilder.group({
      title: [this.record.title],
      description: [this.record.description],
      isDone: [this.record.isDone]
    });
  }

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnDestroy() {
    this.locationStateService.clearState();
    this.stateService.clearState();
  }

  onDashboard() {
    this.locationStateService.clearState();
    this.router.navigate(['dashboard']);
  }

  onSubmit() {
    this.record.title = this.editForm.value.title;
    this.record.description = this.editForm.value.description;
    this.record.isDone = this.editForm.value.isDone;
    this.record.location = this.locationStateService.data;

    this.recordService.updateRecord(this.record)
    .pipe(first())
    .subscribe(
      data => {
        this.locationStateService.clearState();
        this.router.navigate(['dashboard']);
    },
    error => {
      alert(error);
    });
  }
}
