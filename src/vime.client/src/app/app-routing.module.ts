import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './users/login/login.component';
import { BrowseComponent } from './rooms/browse/browse.component';
import { RoomComponent } from './rooms/room/room.component';
import { CreateRoomComponent } from './rooms/create-room/create-room.component';
import { RegisterComponent } from './users/register/register.component';
import { AuthGuard } from './guards/AuthGuard';

const routes: Routes = [
  { path: 'rooms/create', component: CreateRoomComponent, canActivate: [AuthGuard] },
  { path: 'rooms/browse', component: BrowseComponent },
  { path: 'auth/login', component: LoginComponent},
  { path: 'auth/register', component: RegisterComponent },
  { path: 'rooms/:id', component: RoomComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: 'rooms/browse', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {enableTracing: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
