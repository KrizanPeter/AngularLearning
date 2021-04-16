import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";
import { FormsModule } from '@angular/forms'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HomeComponent } from './home/components/home/home.component';
import { RegistrationComponent } from './home/components/registration/registration.component';
import { GameBoardComponent } from './game/components/game-board/game-board.component';
import { CharacterInfoComponent } from './game/components/character-info/character-info.component';
import { ChatComponent } from './game/components/chat/chat.component';
import { CreateGameComponent } from './new-game/components/create-new-game/create-game/create-game.component';
import { JoinToGameComponent } from './new-game/components/join-to-game/join-to-game/join-to-game.component';
import { NewGameComponent } from './new-game/components/new-game-layout/new-game.component';
import { LeaderBoardComponent } from './ranks/components/leader-board/leader-board.component';
import { ToastrModule } from 'ngx-toastr';

import {MatTableModule} from '@angular/material/table';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    RegistrationComponent,
    GameBoardComponent,
    CharacterInfoComponent,
    ChatComponent,
    NewGameComponent,
    LeaderBoardComponent,
    CreateGameComponent,
    JoinToGameComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NgbModule,
    FormsModule,
    MatTableModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-right'
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
