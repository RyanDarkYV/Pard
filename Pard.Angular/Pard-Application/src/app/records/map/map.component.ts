import { Component, OnInit, Input, ViewChild, NgZone } from '@angular/core';
import { MapsAPILoader, AgmMap} from '@agm/core';
import { GoogleMapsAPIWrapper } from '@agm/core';

import { Marker } from '../models/marker';
import { Location } from '../models/location';
import { LocationsStateService } from '../services/state.locations.service';

declare var google: any;

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {

  geocoder: any;
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

  @ViewChild(AgmMap) map: AgmMap;
  constructor(public mapsApiLoader: MapsAPILoader,
              private zone: NgZone,
              private wrapper: GoogleMapsAPIWrapper,
              private locationStateService: LocationsStateService) {
    this.mapsApiLoader = mapsApiLoader;
    this.zone = zone;
    this.wrapper = wrapper;
    this.mapsApiLoader.load().then(() => {
      this.geocoder = new google.maps.Geocoder();
    });
  }

  ngOnInit() {
    if (this.locationStateService.data != null) {
      this.location = this.locationStateService.data;
      this.location.marker.latitude = this.location.latitude;
      this.location.marker.longitude = this.location.longitude;
      this.location.addressCity = this.location.addressCity;
      this.location.addressCountry = this.location.addressCountry;
      this.location.addressState = this.location.addressState;
      this.location.addressStreet = this.location.addressStreet;
      this.map.triggerResize();
    }
    this.location.marker.draggable = true;
  }

  updateOnMap() {
    let fullAddress: string = this.location.addressStreet || '';
    if ( this.location.addressCity) { fullAddress = fullAddress + ' ' + this.location.addressCity; }
    if (this.location.addressState) { fullAddress = fullAddress + ' ' + this.location.addressState; }
    if (this.location.addressCountry) { fullAddress = fullAddress + ' ' + this.location.addressCountry; }

    this.findLocation(fullAddress);
  }

  findLocation(fullAddress) {
    if (this.geocoder) { this.geocoder = new google.maps.Geocoder(); }
    this.geocoder.geocode({
      'address': fullAddress
    }, (results, status) => {
      if (status == google.maps.GeocoderStatus.OK) {
        for (var i = 0; i < results[0].address_components.length; i++) {
          let types = results[0].address_components[i].types;

          if (types.indexOf('locality') != -1) {
            this.location.addressCity = results[0].address_components[i].long_name;
          }

          if (types.indexOf('country') != -1) {
            this.location.addressCountry = results[0].address_components[i].long_name;
          }

          if (types.indexOf('administrative_area_level_1') != -1) {
            this.location.addressState = results[0].address_components[i].long_name;
          }
        }

        if (results[0].geometry.location) {
          this.location.longitude = results[0].geometry.location.lng();
          this.location.latitude = results[0].geometry.location.lat();
          this.location.marker.longitude = results[0].geometry.location.lng();
          this.location.marker.latitude = results[0].geometry.location.lat();
          this.location.marker.draggable = true;
          this.location.viewport = results[0].geometry.viewport;
        }
        this.map.triggerResize();
        this.locationStateService.data = this.location;
      } else {
        alert('Search produced no results.');
      }
    }
    );
  }

  markerDragEnd(m: any, $event: any) {
    this.location.marker.latitude = m.coords.lat;
    this.location.marker.longitude = m.coords.lng;
    this.findAddressByCoordinates();
   }

   findAddressByCoordinates() {
    this.geocoder.geocode({
      'location': {
        lat: this.location.marker.latitude,
        lng: this.location.marker.longitude
      }
    }, (results, status) => {
      this.decomposeAddressComponents(results);
    });
  }

  decomposeAddressComponents(addressArray) {
    if (addressArray.length == 0) { return false; }
    let address = addressArray[0].address_components;
    for(let element of address) {
      if (element.length == 0 && !element['types']) { continue }

      if (element['types'].indexOf('street_number') > -1) {
        this.location.addressStreet = element['long_name'];
        continue;
      }

      if (element['types'].indexOf('route') > -1) {
        this.location.addressStreet += ', ' + element['long_name'];
        continue;
      }

      if (element['types'].indexOf('locality') > -1) {
        this.location.addressCity = element['long_name'];
        continue;
      }

      if (element['types'].indexOf('administrative_area_level_1') > -1) {
        this.location.addressState = element['long_name'];
        continue;
      }

      if (element['types'].indexOf('country') > -1) {
        this.location.addressCountry = element['long_name'];
        continue;
      }

    }
    this.locationStateService.data = this.location;
  }
}
