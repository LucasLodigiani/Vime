import { Component, OnInit } from '@angular/core';
import { RoomService } from '../services/room.service';
import { UserService } from '../../users/services/user.service';

@Component({
  selector: 'app-browse',
  templateUrl: './browse.component.html',
  styleUrl: './browse.component.css',
})
export class BrowseComponent implements OnInit {
  constructor(
    private roomService: RoomService,
    private userService: UserService
  ) {}
  rooms: any;
  isLoading: boolean = true;
  errors: string = '';
  isAuthenticated: boolean;
  ngOnInit(): void {
    this.roomService.getRooms().subscribe({
      next: (response: any) => {
        console.log(response);
        this.rooms = response;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error en la solicitud', err);
        this.errors = err.error.message;
        this.isLoading = false;
      },
    });
    this.userService.userData$.subscribe((userData) => {
      this.isAuthenticated = userData.isAuthenticated;
    });
  }
}
