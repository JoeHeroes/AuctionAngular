import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class TokenService {

  private authChangeSub = new Subject<boolean>()
  public authChanged = this.authChangeSub.asObservable();

  public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this.authChangeSub.next(isAuthenticated);
  }
  public clear() {
    sessionStorage.removeItem("token");
    this.sendAuthStateChangeNotification(false);
  }
  public isLoggedIn() {
    return sessionStorage.getItem("token") != null;
  }

  public set(token: string) {
    sessionStorage.setItem("token", token);
    this.sendAuthStateChangeNotification(true);
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
  surename: string;
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
  name: string;
  surename: string;
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