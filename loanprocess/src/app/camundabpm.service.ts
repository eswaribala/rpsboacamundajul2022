import { Injectable } from '@angular/core';
import { HttpClient ,HttpHeaders} from '@angular/common/http';
import {Observable, of, of as observableOf} from "rxjs/index";
import {EMPTY} from "rxjs/index";
import{pipe} from "rxjs/index";
import{throwError} from "rxjs/index";
import { ProcessDefinition } from './schemas/ProcessDefinition';
import { catchError, map, tap } from 'rxjs/operators';
import { Task } from './schemas/Task';

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

  postProcessInstance(processDefinitionKey:any, variables:any): Observable<any> {
    const endpoint = `${engineRestUrl}process-definition/key/${processDefinitionKey}/start`;
    return this.httpClient.post<any>(endpoint, variables).pipe(
      tap(processDefinitions=> console.log(`posted process instance`)),
      catchError(this.handleError('postProcessInstance', []))
    );
  }

  getProcessDefinitionTaskKey(processDefinitionKey:any): Observable<any> {
    const url = `${engineRestUrl}process-definition/key/${processDefinitionKey}/startForm`;
    return this.httpClient.get<any>(url).pipe(
      tap(form => console.log(`fetched formkey`)),
      catchError(this.handleError('getProcessDeifnitionFormKey', []))
    );
  }

  getTasks(): Observable<Task[]> {
    const endpoint = `${engineRestUrl}task?sortBy=created&sortOrder=desc&maxResults=10`;
    return this.httpClient.get<any>(endpoint).pipe(
      tap(form => console.log(`fetched tasks`)),
      catchError(this.handleError('getTasks', []))
    );
  }

  getTaskFormKey(taskId: String): Observable<any> {
    const endpoint = `${engineRestUrl}task/${taskId}/form`;
    return this.httpClient.get<any>(endpoint).pipe(
      tap(form => console.log(`fetched taskform`)),
      catchError(this.handleError('getTaskFormKey', []))
    );
  }

  getVariablesForTask(taskId: String, variableNames: String): Observable<any> {
    const endpoint = `${engineRestUrl}task/${taskId}/form-variables?variableNames=${variableNames}`;
    return this.httpClient.get<any>(endpoint).pipe(
      tap(form => console.log(`fetched variables`)),
      catchError(this.handleError('getVariablesForTask', []))
    );
  }

  postCompleteTask(taskId: String, variables: Object): Observable<any> {
    const endpoint = `${engineRestUrl}task/${taskId}/complete`;
    return this.httpClient.post<any>(endpoint, variables).pipe(
      tap(tasks => console.log(`posted complete task`)),
      catchError(this.handleError('postCompleteTask', []))
    );
  }

  getProcessDefinitions(): Observable<ProcessDefinition[]> {
    return this.httpClient.get<ProcessDefinition[]>(engineRestUrl + 'process-definition?latestVersion=true').pipe(
      tap(processDefinitions => console.log(`fetched processDefinitions`)),
      catchError(this.handleError('getProcessDefinitions', []))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumptioncd
      console.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

}
