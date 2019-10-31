import { Component, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

import { Record } from '../models/record';
import { RecordService } from '../services/records.services';
import { LocationsStateService } from '../services/state.locations.service';

@Component({
  selector: 'app-create-form',
  templateUrl: './create-form.component.html',
  styleUrls: ['./create-form.component.css']
})
export class CreateFormComponent implements OnInit {

  createForm: FormGroup;
  record: Record;

  constructor(private formBuilder: FormBuilder,
              private router: Router,
              private recordService: RecordService,
              private locationStateService: LocationsStateService) { }

  ngOnInit() {
    this.createForm = this.formBuilder.group({
      title: [''],
      description: [''],
      isDone: [false]
    });
  }

  toOverview() {
    this.locationStateService.clearState();
    this.router.navigate(['overview']);
  }

  onSubmit() {
    this.record = this.createForm.value;
    this.record.location = this.locationStateService.data;
    console.log(JSON.stringify(this.record));
    console.log(JSON.stringify(this.record.location));

    this.recordService.createRecord(this.record).subscribe(data => {
      this.locationStateService.clearState();
      this.router.navigate(['overview']);
    });
  }

}
