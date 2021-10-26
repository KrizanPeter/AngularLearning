import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { time } from 'console';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { HeroAttribute } from 'src/app/_models/enums/enumsDtos';
import { IngameHeroDto } from 'src/app/_models/HeroDtos/ingameHeroDto';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CharacterInfoService {

  characterInfoThread = new BehaviorSubject<IngameHeroDto>({} as IngameHeroDto)
  characterInfo$ = this.characterInfoThread.asObservable();

  baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient) { }

  getHeroInformation(){
    this.http.get<IngameHeroDto>(this.baseUrl+"coregame/getheroinfo").subscribe((response) =>{
        this.characterInfoThread.next(response);
      })

      return this.characterInfo$;
  }

  upgradeHeroSkill(attribute: HeroAttribute){
     let result =  this.http.post(this.baseUrl+"coregame/upgradeatribute?", attribute);
     return result;
  }
}
