import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { RoomService } from '../services/room.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-room',
  templateUrl: './create-room.component.html',
  styleUrl: './create-room.component.css'
})
export class CreateRoomComponent {
  constructor(private roomService: RoomService, private router: Router){}
  createRoomForm = new FormGroup({
    title: new FormControl<string>(''),
    videoUrl: new FormControl<string>(''),
  });
  isLoading : boolean = false;
  errors : string = '';

  onSubmit() {
    const title = this.createRoomForm.value.title;
    const videourl = this.createRoomForm.value.videoUrl;
    this.isLoading = true;

    this.roomService.createRoom(title ?? '', videourl ?? '').subscribe({
      next: (response) => {
        console.log(response);
        this.isLoading = false;
        this.router
          .navigateByUrl('/rooms/' + response.roomId, { skipLocationChange: true, replaceUrl: true })
          .then(() => {
            this.router.navigate([this.router.url]);
          });
      },
      error: (err) => {
        console.error('Error en la solicitud', err);
        this.errors = err.error.message;
        this.isLoading = false;
      },
    });
}
}
