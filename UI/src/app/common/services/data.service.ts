import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class DataService {
  userId!: number;
  userName!: string;
  userSureName!: string;

  constructor() { }
}