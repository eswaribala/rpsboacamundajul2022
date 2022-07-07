import { Injectable } from '@angular/core';
import { HttpClient ,HttpHeaders} from '@angular/common/http';
import {Observable,of as observableOf} from "rxjs/index";
import {EMPTY} from "rxjs/index";
import{pipe} from "rxjs/index";
import{throwError} from "rxjs/index";
import { catchError } from 'rxjs/operators';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  ' multipart/form-data;boundary=something'
  })
};

const engineRestUrl = 'http://localhost:7070/engine-rest/';
@Injectable({
  providedIn: 'root'
})
export class CamundaBPMService {

  constructor(private httpClient:HttpClient) { }

  deployProcess(fileToUpload: File): Observable<any> {
    const endpoint = `${engineRestUrl}deployment/create`;
    const formData = new FormData();

    formData.append('fileKey', fileToUpload, fileToUpload.name);

    return this.httpClient.post(endpoint, formData);
  }
}
