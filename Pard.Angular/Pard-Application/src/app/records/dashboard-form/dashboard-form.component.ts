import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { RecordService } from '../services/records.services';
import { Record } from '../models/record';
import { RecordsStateService } from '../services/state.records.service';
import { FilterPipe } from '../filters/filter.pipe';

@Component({
  selector: 'app-dashboard-form',
  templateUrl: './dashboard-form.component.html',
  styleUrls: ['./dashboard-form.component.css']
})
export class DashboardFormComponent implements OnInit {

  public searchString: string;
  records: Record[];
  constructor(private router: Router,
              private recordService: RecordService,
              private stateService: RecordsStateService) { }

  ngOnInit() {
    this.recordService.getAllFinishedRecordsForUser()
    .subscribe(
      data => {
        this.records = data;
      }
    );
  }

  deleteRecord(record: Record): void {
    if (confirm('Are you sure you want to remove this record?')) {
      this.recordService.softDeleteRecord(record)
      .subscribe();
      const index = this.records.indexOf(record);
      if (index > -1) {
        this.records.splice(index, 1);
      }
    }  else { }
  }

  updateRecord(record: Record): void {
    this.stateService.setState(record);
    this.router.navigate(['update']);
  }

  createRecord(): void {
    this.router.navigate(['create']);
  }

}
