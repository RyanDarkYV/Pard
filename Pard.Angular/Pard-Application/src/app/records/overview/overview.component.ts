import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { RecordService } from '../services/records.services';
import { Record } from '../models/record';
import { Marker } from '../models/marker';
import { RecordsStateService } from '../services/state.records.service';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent implements OnInit {
  records: Record[];

  constructor(private router: Router,
              private recordService: RecordService,
              private stateService: RecordsStateService) { }

  ngOnInit() {
    this.recordService.getAllUnfinishedRecords()
    .subscribe(
      data => {
        console.log(JSON.stringify(data));
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
