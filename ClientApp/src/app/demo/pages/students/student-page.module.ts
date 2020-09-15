import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StudentPageRoutingModule } from './student-page-routing.module';
import { StudentPageComponent } from './student-page.component';
import {SharedModule} from '../../../theme/shared/shared.module';

@NgModule({
  declarations: [StudentPageComponent],
  imports: [
    CommonModule,
    StudentPageRoutingModule,
    SharedModule
  ]
})
export class StudentPageModule { }
