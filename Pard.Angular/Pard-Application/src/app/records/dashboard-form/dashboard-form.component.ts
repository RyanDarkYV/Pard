import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { RecordService } from '../services/records.services';
import { Record } from '../models/record';
import { RecordsStateService } from '../services/state.records.service';

@Component({
  selector: 'app-dashboard-form',
  templateUrl: './dashboard-form.component.html',
  styleUrls: ['./dashboard-form.component.css']
})
export class DashboardFormComponent implements OnInit {

  records: Record[];
  constructor(private router: Router,
              private recordService: RecordService,
              private stateService: RecordsStateService) { }

  ngOnInit() {
    this.recordService.getAllRecordsForUser()
    .subscribe(
      data => {
        this.records = data;
      }
    );
  }

  updateRecord(record: Record): void {
    this.stateService.setState(record);
    this.router.navigate(['update']);
  }

  createRecord(): void {
    this.router.navigate(['create']);
  }

}
