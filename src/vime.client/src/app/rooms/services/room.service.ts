import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class RoomService {
  constructor(private http: HttpClient) {}

  getRooms(): Observable<any> {
    return this.http.get<any>(`${environment.apiUrl}/api/GetRooms`);
  }

  getRoom(roomId: number): Observable<any> {
    return this.http.get<any>(`${environment.apiUrl}/api/GetRoom/${roomId}`);
  }

  createRoom(title: string, videoUrl: string): Observable<any> {
    const body = {
      Title: title,
      VideoUrl: videoUrl,
    };
    return this.http.post<any>(`${environment.apiUrl}/api/CreateRoom`, body);
  }
}
