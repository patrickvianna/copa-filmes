import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import { HttpHeaders } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class BaseService {
  private serviceUrl = 'http://localhost:54652/api/'; 

  requestOptions = new RequestOptions();
  
  constructor(private http: Http, private toastr: ToastrService) {
    this.requestOptions.headers = new Headers({
      'Content-Type':  'application/json'
    })
  }
  // ,
  //     'Access-Control-Allow-Origin': '*',
  //     'Access-Control-Allow-Methods': 'GET, POST, OPTIONS, PUT, PATCH, DELETE',
  //     'Access-Control-Allow-Headers': 'X-Requested-With,content-type'

  public post(url: string, data: any) {
    return new Promise((resolve, reject) => {
      this.http.post(this.serviceUrl + url, data, this.requestOptions).subscribe((result: any) => {
        resolve(result.json());
      },
      err => {
        this.serverError(err.status);
        console.log(err);
        reject(err);
      });
    });
  }
  
  public put(url: string, data: any) {
    return new Promise((resolve, reject) => {
      this.http.put(this.serviceUrl + url, data, this.requestOptions).subscribe((result: any) => {
        resolve(result.json());
      },
      err => {
        this.serverError(err.status);
        console.log(err);
        reject(err);
      });
    });
  }

  public delete(url: string) {
    return new Promise((resolve, reject) => {
      this.http.delete(this.serviceUrl + url).subscribe((result: any) => {
        resolve(result.json());
      },
      err => {
        this.serverError(err.status);
        console.log(err);
        reject(err);
      });
    });
  }

  public get(url: string) {
    return new Promise((resolve, reject) => {
      this.http.get(this.serviceUrl + url).subscribe((result: any) => {
        resolve(result.json());
      },
      err => {
        this.serverError(err.status);
        console.log(err);
        reject(err);
      });
    });
  }

  private serverError(status: number) {
    if(status == 0 || status == 500)
      this.toastr.error("Ops, houve algum erro no servidor")
  }
}
