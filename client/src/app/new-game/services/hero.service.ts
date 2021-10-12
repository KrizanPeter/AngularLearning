import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';
import { PickHeroDto } from 'src/app/_models/HeroDtos/PickHeroDto';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HeroService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  pickHero(pickHeroDto: PickHeroDto) {
    return this.http.post(this.baseUrl+"coregame/pickhero", pickHeroDto).pipe(
      tap((response) =>{
        console.log(response);
      })
    )
  }
}
