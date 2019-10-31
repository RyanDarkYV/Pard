import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SpinnerComponent } from '../../spinner/spinner.component';
import { FilterPipe } from 'src/app/records/filters/filter.pipe';


@NgModule({
  imports:      [CommonModule],
  declarations: [SpinnerComponent, FilterPipe],
  exports:      [SpinnerComponent, FilterPipe],
  providers:    []
})
export class SharedModule { }
