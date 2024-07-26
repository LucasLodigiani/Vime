import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { RoomRealtimeService } from '../services/room-realtime.service';
import { ActivatedRoute } from '@angular/router';
import { RoomService } from '../services/room.service';
import { UserService } from '../../users/services/user.service';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrl: './room.component.css',
})
export class RoomComponent implements OnInit{
  videoUrl: string;
  currentUserUsername: string;
  userIsLeader: boolean;
  constructor(
    private signalRService: RoomRealtimeService,
    private roomService: RoomService,
    private userService: UserService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const roomId = this.route.snapshot.paramMap.get('id');

    //Obtengo los datos del usuario actual.
    this.userService.userData$.subscribe((userData) => {
      this.currentUserUsername = userData.username;
    });
    //Obtengo la room y el url del video a reproducir.
    this.roomService.getRoom(parseInt(roomId)).subscribe({
      next: (response: any) => {
        this.videoUrl = response.videoUrl;
      },
      error: (err) => {
        console.error('Error en la solicitud', err);
      },
    });
    //Me conecto a la sala.
    this.signalRService.startConnection().subscribe(() => {
      this.signalRService.joinToRoom(roomId);
    });
  }

}
