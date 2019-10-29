import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AgmCoreModule, GoogleMapsAPIWrapper } from '@agm/core';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './header/header.component';
import { AccountModule } from './account/account.module';
import { RecordsModule} from './records/records.module';
import { ConfigService } from './shared/utils/config.service';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent
  ],
  imports: [
    BrowserModule,
    AgmCoreModule.forRoot({apiKey: 'AIzaSyAir3eaRWB2pftqXuhenovK1XbSVCIjKr4'}),
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RecordsModule,
    AccountModule,
    AppRoutingModule
  ],
  providers: [
    ConfigService,
    GoogleMapsAPIWrapper
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
