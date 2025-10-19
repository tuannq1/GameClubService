import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';

export interface Club {
  id?: string;
  name: string;
  description: string;
}

@Injectable({ providedIn: 'root' })
export class ClubService {
  private readonly apiUrl = `${environment.apiBaseUrl}/clubs`;

  constructor(private http: HttpClient) {}

  getClubs(): Observable<Club[]> {
    return this.http.get<Club[]>(this.apiUrl).pipe(
      catchError(this.handleError)
    );
  }

  createClub(club: Club): Observable<Club> {
    return this.http.post<Club>(this.apiUrl, club).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse) {
    console.error('ClubService error:', error);
    if (error.status === 0) {
      console.error('A network error occurred:', error.error);
    } else {
      console.error(`Backend returned code ${error.status}, body: `, error.error);
    }
    return throwError(() => new Error('Something went wrong; please try again later.'));
  }
}
