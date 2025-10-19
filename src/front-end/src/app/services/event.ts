import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface Event {
  id?: string;
  clubId: string;
  title: string;
  description: string;
  scheduledAt: string;
}

@Injectable({ providedIn: 'root' })
export class EventService {
  private apiUrl = `${environment.apiBaseUrl}/events`;

  constructor(private http: HttpClient) {}

  getEvents(): Observable<Event[]> {
    return this.http.get<Event[]>(this.apiUrl);
  }

  createEvent(event: Event): Observable<Event> {
    return this.http.post<Event>(this.apiUrl, event);
  }
}
