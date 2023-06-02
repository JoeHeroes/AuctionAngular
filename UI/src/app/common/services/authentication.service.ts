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

    return this.http.patch<any>(url_, data);
  }


  public getUserInfo(id: number): Observable<any> {

    let url_ = "https://localhost:7257/Account/userInfo/" + id;

    return this.http.get<any>(url_);
  }

  public loggedUserId() {

    let token: any = sessionStorage.getItem("token");

    const header = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    });

    return this.http.get<any>("https://localhost:7257/Account/current", { headers: header });
  }

  public getRoles(): Observable<any> {

    let url_ = "https://localhost:7257/Account/roles";

    return this.http.get<any>(url_);
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
  name: string;
  sureName: string;
  nationality: string;
  phone: string;
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
  name: string;
  sureName: string;
  nationality: string;
  phone: string;
  dateofbirth: Date;
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