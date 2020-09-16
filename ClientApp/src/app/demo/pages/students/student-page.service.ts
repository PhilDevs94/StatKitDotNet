import 'rxjs/Rx';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Student } from '../../../models/student';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class StudentPageService {
    constructor(private _http: HttpClient) {

    }

    CreateStudent(student: Student) {
        return this._http.post<Student>(
            "http://localhost:53397/odata/StudentsDto", 
            student,
            {headers: new HttpHeaders({
                'Content-Type':  'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': 'true',
                'Access-Control-Allow-Headers': 'Content-Type',
                'Access-Control-Allow-Methods': 'GET,PUT,POST,DELETE'
            })}
        )
        .map(res => {
            console.log(res);   
        })
        .catch(err => this.handleError(err));
    }
    getStudentList(): any {
       return this._http.get(
        "http://localhost:53397/odata/StudentsDto",
        {headers: new HttpHeaders({
            'Content-Type':  'application/json',
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Credentials': 'true',
            'Access-Control-Allow-Headers': 'Content-Type',
            'Access-Control-Allow-Methods': 'GET,PUT,POST,DELETE'
        })}
       )
       .map(res => res)
        .catch(this.handleError);
    }
    deleteStudent(id: string) : any {
        debugger;
        return this._http.delete(
            "http://localhost:53397/odata/StudentsDto("+id+")",
            {headers: new HttpHeaders({
                'Content-Type':  'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': 'true',
                'Access-Control-Allow-Headers': 'Content-Type',
                'Access-Control-Allow-Methods': 'GET,PUT,POST,DELETE'
            })}
        ).map(res => res)
        .catch(this.handleError);
    }
    private handleError(error: Response) {
        console.log(error);
        return Observable.throwError(error || 'Server error');
    }
}