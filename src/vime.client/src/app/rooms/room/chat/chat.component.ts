import { Component, OnInit } from '@angular/core';
import { RoomRealtimeService } from '../../services/room-realtime.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent implements OnInit {
  mensajeInput: string = '';
  messages = [];
  constructor(private signalRService: RoomRealtimeService) {}
  ngOnInit(): void {
    //Recibir mensajes
    this.signalRService
      .receiveChatMessage()
      .subscribe(({ author, message }) => {
        this.messages.push({ author, message });
      });
  }

  sendMessage() {
    this.signalRService.sendMessage(this.mensajeInput);
  }
}
