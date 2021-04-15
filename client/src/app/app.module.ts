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
import { NewGameComponent } from './game/components/new-game/new-game.component';
import { LeaderBoardComponent } from './ranks/components/leader-board/leader-board.component';

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
    LeaderBoardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NgbModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
