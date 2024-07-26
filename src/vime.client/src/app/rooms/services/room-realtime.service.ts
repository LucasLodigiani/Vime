import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { UserService } from '../../users/services/user.service';

@Injectable({
  providedIn: 'root',
})
export class RoomRealtimeService {
  private hubConnection: signalR.HubConnection;
  roomConnectedId: string;

  //Definir conexion, incluir jwt
  constructor(private userService: UserService) {
    const token = this.userService.getToken();

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.apiUrl}/hub`, {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
        accessTokenFactory: () => token,
      })
      .build();
  }

  //Conectarse al hub
  startConnection(): Observable<void> {
    return new Observable<void>((observer) => {
      this.hubConnection
        .start()
        .then(() => {
          console.log('Connection established with SignalR hub');
          observer.next();
          observer.complete();
        })
        .catch((error) => {
          console.error('Error connecting to SignalR hub:', error);
          observer.error(error);
        });
    });
  }

  //Entrar a una room
  joinToRoom(roomId: string): void {
    this.hubConnection.invoke('JoinToRoom', roomId);
    this.roomConnectedId = roomId;
    console.log('joined to room');
  }
  //--------------Video Player------------------
  receiveVideoEvent(): Observable<string> {
    return new Observable<string>((observer) => {
      this.hubConnection.on('ReceiveVideoEvent', (message: string) => {
        observer.next(message);
      });
    });
  }

  sendVideoEvent(videoEvent: string) {
    this.hubConnection.invoke(
      'SendVideoEvent',
      videoEvent,
      this.roomConnectedId
    );
  }
  //--------------Chat--------------------
  sendMessage(message: string): void {
    this.hubConnection.invoke('SendMessage', message);
  }

  receiveChatMessage(): Observable<{ author: string; message: string }> {
    return new Observable<{ author: string; message: string }>((observer) => {
      this.hubConnection.on(
        'ReceiveChatMessage',
        (author: string, message: string) => {
          console.log(message);
          observer.next({ author, message }); // Emitir un objeto con las propiedades author y message
        }
      );
    });
  }

  //ReceiveChatMessage
}
