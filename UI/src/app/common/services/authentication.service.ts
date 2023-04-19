import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient) { }

  public loginUser(data: UserAuthenticationDto): Observable<any> {

    let url_ = "https://localhost:7257/Account/login";

    return this.http.post<any>(url_, data);
  }

  public registerUser(data: UserRegisterDto): Observable<any> {

    let url_ = "https://localhost:7257/Account/register";

    return this.http.post<any>(url_, data);
  }

  public restartPassword(data: RestartPasswordDto): Observable<any> {

    let url_ = "https://localhost:7257/Account/restart";

    return this.http.post<any>(url_, data);
  }


  public editProfile(data: EditProfileDto): Observable<any> {

    let url_ = "https://localhost:7257/Account/edit";

    return this.http.post<any>(url_, data);
  }


  public getUserInfo(id: number): Observable<any> {

    let url_ = "https://localhost:7257/Account/userInfo/" + id;

    return this.http.get<any>(url_);
  }


  public loggedUserId() {

    let token: any = localStorage.getItem("token");

    const header = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    });

    /*
     let url_ = this.baseUrl + "/authentication/token";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            context: httpContext,
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };
    */

    return this.http.get<any>("https://localhost:7257/Account/current", { headers: header });
  }
}



export interface UserAuthenticationDto {
  email: string;
  password: string;
}

export interface UserRegisterDto {
  email: string;
  password: string;
  confirmpassword: string;
  firstname: string;
  lastname: string;
  nationality: string;
  dateofbirth: Date;
  roleid: number;
}

export interface RestartPasswordDto {
  email: string;
  oldPassword: string;
  newPassword: string;
  confirmNewPassword: string;
}


export interface EditProfileDto {
  userId: number;
  firstName: string;
  lastName: string;
  nationality: string;
}

export interface AuthResponseDto {
  isAuthSuccessful: boolean;
  errorMessage: string;
  token: string;
}


export interface TecsJwtPayload {
  name: string,
  unique_name: string
}