import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Student } from 'src/app/models/student';
import { StudentPageService } from './student-page.service';
import {ToastService} from '../../../theme/shared/components/toast/toast.service';

@Component({
  selector: 'app-student-page',
  templateUrl: './student-page.component.html',
  styleUrls: ['./student-page.component.scss'],
  providers: [StudentPageService,ToastService]
})
export class StudentPageComponent implements OnInit {
  student = new Student();
  isShow = false;
  studentList: Student[];
  constructor(
    private http: HttpClient,
    private _studentService: StudentPageService,
    public _toastService: ToastService
  ) { }
  ngOnInit() {
    this.getListStudent();
  }

  getListStudent(): void {
    this._studentService.getStudentList()
      .subscribe(
        value => {
          this.studentList = value;
          console.log(this.studentList);
        },
        error => {
          console.log(<any>error)
        }
      );
  }

  deleteStudent(event: Event) : void {
    let elementId: string = (event.target as Element).id;
    this._studentService.deleteStudent(elementId).subscribe(
      data => {
        if(data.deleted == "Success") {
          this.getListStudent();
        }
        console.log(data);
      }
    );
  }
  createStudent(): void {
    this._studentService.CreateStudent(this.student).subscribe(
      data => {
        this.isShow = true;
        this.getListStudent();
        console.log('POST is done');
        console.log('response from server:' + data);
      }
    );
  }
  closeButtonClick() :void {
    this.isShow = false;
  }
}