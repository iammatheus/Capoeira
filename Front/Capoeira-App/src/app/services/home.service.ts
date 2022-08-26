import { environment } from 'src/environments/environment.prod';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Evento } from '../models/Evento';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  baseURL = `${environment.apiURL}api/home`;
  constructor(private http: HttpClient) { }

  public getEventos(): Observable<Evento[]>{
    return this.http.get<Evento[]>(this.baseURL);
  }
}
