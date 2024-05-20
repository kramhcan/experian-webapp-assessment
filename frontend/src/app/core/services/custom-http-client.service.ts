import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CustomHttpClientService {
  private _apiUrl = 'http://localhost:5010/api/Customer';
  private _apiKey = '1234567890';

  constructor(private http: HttpClient) {}

  private request(
    method: string,
    endpoint: string,
    body?: any
  ): Observable<any> {
    const headers = new HttpHeaders({
      'x-api-key': this._apiKey,
      'access-control-allow-origin': "http://localhost:4200"
    });
    // console.log('Requesting');
    return this.http.request(method, `${this._apiUrl}/${endpoint}`, {
      body,
      headers,
      responseType: 'json',
    });
  }

  get(endpoint: string, body: any = {}): Observable<any> {
    // console.log('GETing');
    return this.request('GET', endpoint, body);
  }

  post(endpoint: string, body: any): Observable<any> {
    // console.log('POSTing');
    return this.request('POST', endpoint, body);
  }
}
