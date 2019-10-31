import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { ArchiveService } from '../services/archive.service';
import { Record } from '../models/record';
import { RecordsStateService } from '../services/state.records.service';

@Component({
  selector: 'app-archive',
  templateUrl: './archive.component.html',
  styleUrls: ['./archive.component.css']
})
export class ArchiveComponent implements OnInit {
  records: Record[];
  constructor(private router: Router,
              private archiveService: ArchiveService,
              private stateService: RecordsStateService) { }

  ngOnInit() {
    this.archiveService.getArchivedRecords()
    .subscribe(
      data => {
        this.records = data;
      }
    );
  }

  deleteRecord(record: Record): void {
    if (confirm('Are you sure you want to remove this record?\nYou can\'t undo this action')) {
      this.archiveService.DeleteRecord(record)
      .subscribe();
      const index = this.records.indexOf(record);
      if (index > -1) {
        this.records.splice(index, 1);
      }
    }  else { }
  }

  restoreRecord(record: Record): void {
    if (confirm('Are you sure you want to restore this record?')) {
      this.archiveService.restoreRecord(record)
      .subscribe();
      const index = this.records.indexOf(record);
      if (index > -1) {
        this.records.splice(index, 1);
      }
    }  else { }
  }

}
