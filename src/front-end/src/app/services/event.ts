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
  private apiUrl = `${environment.apiBaseUrl}`;

  constructor(private http: HttpClient) {}

  getEvents(clubId: string): Observable<Event[]> {
    const getApiUrl = this.apiUrl + '/clubs/' + clubId + '/events'
    return this.http.get<Event[]>(getApiUrl);
  }

  createEvent(event: Event): Observable<Event> {
    const createApiUrl = this.apiUrl + '/clubs/' + event.clubId + '/events'
    return this.http.post<Event>(createApiUrl, event);
  }
}
