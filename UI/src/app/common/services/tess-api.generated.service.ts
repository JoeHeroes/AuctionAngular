import { HttpClient } from '@angular/common/http';
import { Injectable, InjectionToken } from '@angular/core';
import { from as _observableFrom, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';





export const TESS_BASE_URL = new InjectionToken<string>('TESS_BASE_URL');

@Injectable({
    providedIn: 'root'
})
export class TessSystemApiClient {
    private http: HttpClient;
    private baseUrl: string;

    constructor(http: HttpClient) {
        this.http = http;
        this.baseUrl = "https://erik-api.topmotive.eu";
    }




    /**
    *  tmErik ESS
    * 
    * Health status
    * @return Sever health status
    */


    getHealthSever(url?: string) {

        let url_ = this.baseUrl + "/health";

        return this.http.get<any>(url_);
    }





}



