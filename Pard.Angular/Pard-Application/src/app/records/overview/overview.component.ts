import { Component, OnInit, ViewChild, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { MapsAPILoader, AgmMap, GoogleMapsAPIWrapper, AgmFitBounds, AgmInfoWindow } from '@agm/core';

import { RecordService } from '../services/records.services';
import { Record } from '../models/record';
import { Location } from '../models/location';
import { RecordsStateService } from '../services/state.records.service';

declare var google: any;

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent implements OnInit {
  public location: Location = {
    latitude: 50.4501,
    longitude: 30.5234,
    marker: {
      latitude: 50.4501,
      longitude: 30.5234,
      draggable: false
    },
    zoom: 5
  };

  records: Record[];

  @ViewChild(AgmMap) map: AgmMap;
  constructor(private router: Router,
              private zone: NgZone,
              private recordService: RecordService,
              private stateService: RecordsStateService) {
    this.zone = zone;
    }

  ngOnInit() {
    this.recordService.getAllUnfinishedRecords()
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

  onMouseOver(infoWindow, $event: MouseEvent) {
    infoWindow.open();
}

  onMouseOut(infoWindow, $event: MouseEvent) {
      infoWindow.close();
  }

  markerClicked(record: Record){
    this.stateService.setState(record);
    this.router.navigate(['update']);
  }
}
