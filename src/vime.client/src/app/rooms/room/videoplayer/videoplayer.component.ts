import { Component, ViewChild, Input, OnInit } from '@angular/core';
import { PlyrComponent } from '@atom-platform/ngx-plyr';
import { RoomRealtimeService } from '../../services/room-realtime.service';

@Component({
  selector: 'app-videoplayer',
  templateUrl: './videoplayer.component.html',
  styleUrl: './videoplayer.component.css',
})
export class VideoplayerComponent {
  constructor(private signalRService: RoomRealtimeService) {}
  @Input() videoUrl: string;

  receivedVideoEvent: string;

  // get the component instance to have access to plyr instance
  @ViewChild(PlyrComponent, { static: true })
  plyr: PlyrComponent;

  player: Plyr;

  videoSources: Plyr.Source[] = [
    {
      src: 'initializeVideoSource',
      provider: 'youtube',
    },
  ];

  /*
  This event is bugged, don't use
  played(event: Plyr.PlyrEvent) {
    console.log('played', event);
    this.signalRService.sendVideoEvent('played');
  }
  */
  play(): void {
    this.player.play();
  }

  paused(event: Plyr.PlyrEvent) {
    console.log('paused', event);
    this.signalRService.sendVideoEvent('paused');
  }

  updateTime(event: Plyr.PlyrEvent) {
    console.log('test');
    const timestamp = event.timeStamp;
    this.signalRService.sendVideoEvent('updateTime' + timestamp);
  }

  ngAfterViewInit() {
    //Establezco el video obtenido del componente padre en el reproductor.
    console.log(this.videoUrl);
    this.player.source = {
      type: 'video',
      sources: [
        {
          src: this.videoUrl,
          provider: 'youtube',
        },
      ],
    };
    //Reproduzco los eventos recibidos del hub en el reproductor del video. To Do: Incluir el resto de eventos.
    this.signalRService.receiveVideoEvent().subscribe((message) => {
      this.receivedVideoEvent = message;
      if (this.receivedVideoEvent === 'paused') {
        this.player.pause();
      }
    });
  }
}
