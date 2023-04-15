import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private authChangeSub = new Subject<boolean>()
  public authChanged = this.authChangeSub.asObservable();
  constructor(private http: HttpClient) { }

  public loginUser(user: UserAuthenticationDto) {
    return this.http.post<any>("https://localhost:7257/Account/login", user);
  }

  public registerUser(user: UserRegisterDto) {
    return this.http.post<any>("https://localhost:7257/Account/register", user);
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

export interface AuthResponseDto {
  isAuthSuccessful: boolean;
  errorMessage: string;
  token: string;
}