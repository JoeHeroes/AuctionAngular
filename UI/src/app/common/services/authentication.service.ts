import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private authChangeSub = new Subject<boolean>()
  public authChanged = this.authChangeSub.asObservable();
  constructor(private http: HttpClient) { }

  public loginUser(data: UserAuthenticationDto) {
    return this.http.post<any>("https://localhost:7257/Account/login", data);
  }

  public registerUser(data: UserRegisterDto) {
    return this.http.post<any>("https://localhost:7257/Account/register", data);
  }

  public restartPassword(data: RestartPasswordDto) {
    return this.http.post<any>("https://localhost:7257/Account/restart", data);
  }


  public editProfile(data: EditProfileDto) {
    return this.http.post<any>("https://localhost:7257/Account/edit", data);
  }


  public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this.authChangeSub.next(isAuthenticated);
  }
  public loggedUserId() {

    let token: any = localStorage.getItem("token");

    const header = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    });

    return this.http.get<any>("https://localhost:7257/Account/current", { headers: header });
  }

  public logout() {
    localStorage.removeItem("token");
    this.sendAuthStateChangeNotification(false);
  }

  public isLoggedIn() {
    return localStorage.getItem("token") != null;
  }


  public getUserInfo(id: number): Observable<any> {

    let url_ = "https://localhost:7257/Account/userInfo/" + id;

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