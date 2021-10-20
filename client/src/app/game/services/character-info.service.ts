import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { HeroAttribute } from 'src/app/_models/enums/enumsDtos';
import { IngameHeroDto } from 'src/app/_models/HeroDtos/ingameHeroDto';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CharacterInfoService {

  baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient) { }

  getHeroInformation(){
    return this.http.get<Observable<IngameHeroDto>>(this.baseUrl+"coregame/getheroinfo").pipe(
      tap((response: any)=>{
        console.log("Here is hero info");
        console.log(response);
      }));
  }

  upgradeHeroSkill(attribute: HeroAttribute){
    return this.http.post(this.baseUrl+"coregame/upgradeatribute?", attribute);
  }
}
