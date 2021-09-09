import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewGameComponent } from './new-game/components/new-game-layout/new-game.component';
import { HomeComponent } from './home/components/home/home.component';
import { LeaderBoardComponent } from './ranks/components/leader-board/leader-board.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'game', component: NewGameComponent, canActivate: [AuthGuard] },
  { path: 'ranks', component: LeaderBoardComponent, canActivate: [[AuthGuard]] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
