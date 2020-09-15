import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {StudentPageComponent} from './student-page.component';

const routes: Routes = [
  {
    path: '',
    component: StudentPageComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StudentPageRoutingModule { }
