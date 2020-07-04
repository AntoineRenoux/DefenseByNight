import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Attribute } from '../_models/attribute';

@Injectable({
  providedIn: 'root'
})
export class AttributeService {

  baseUrl = environment.apiUrl + 'attribute';
  constructor(private http: HttpClient) { }

  getAll(): Observable<Attribute[]> {
    return this.http.get<Attribute[]>(this.baseUrl);
  }

}
