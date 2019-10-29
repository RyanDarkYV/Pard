import { Marker } from './marker';
export interface Location {
    id?: string;
    latitude: number;
    longitude: number;
    // tslint:disable-next-line:ban-types
    viewport?: Object;
    zoom: number;
    addressCity?: string;
    addressStreet?: string;
    addressState?: string;
    addressCountry?: string;
    marker?: Marker;
    recordId?: string;
}
