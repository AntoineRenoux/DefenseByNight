import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Archetype } from '../_models/archetype';

@Injectable({
  providedIn: 'root'
})
export class ArchetypesService {

  baseUrl = environment.apiUrl + 'archetype';

  constructor(private http: HttpClient) { }

  getAllArchetypes(): Observable<Archetype[]> {
    return this.http.get<Archetype[]>(this.baseUrl);
  }
}
