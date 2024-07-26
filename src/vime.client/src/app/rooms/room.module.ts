import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowseComponent } from './browse/browse.component';
import { MaterialModule } from '../material/material.module';
import {
  provideHttpClient,
  withInterceptorsFromDi,
} from '@angular/common/http';
import { RoomComponent } from './room/room.component';
import { RouterModule } from '@angular/router';
import { VideoplayerComponent } from './room/videoplayer/videoplayer.component';
import { PlyrModule } from '@atom-platform/ngx-plyr';
import { ChatComponent } from './room/chat/chat.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProviderpipePipe } from './pipes/providerpipe.pipe';
import { CreateRoomComponent } from './create-room/create-room.component';


@NgModule({
  declarations: [BrowseComponent, RoomComponent, VideoplayerComponent, ChatComponent, ProviderpipePipe, CreateRoomComponent],
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    PlyrModule,
  ],
  providers: [provideHttpClient(withInterceptorsFromDi())],
})
export class RoomModule {}
