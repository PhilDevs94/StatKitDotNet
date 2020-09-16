import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { StudentPageRoutingModule } from './student-page-routing.module';
import { StudentPageComponent } from './student-page.component';
import {SharedModule} from '../../../theme/shared/shared.module';

@NgModule({
  declarations: [StudentPageComponent],
  imports: [
    CommonModule,
    StudentPageRoutingModule,
    SharedModule,
    HttpClientModule,
  ]
})
export class StudentPageModule { }
