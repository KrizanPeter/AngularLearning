import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';
import { Connections } from 'src/app/_conf/connections';
import { PickHeroDto } from 'src/app/_models/HeroDtos/PickHeroDto';

@Injectable({
  providedIn: 'root'
})
export class HeroService {
  _con = new Connections()

  constructor(private http: HttpClient) { }

  pickHero(pickHeroDto: PickHeroDto) {
    return this.http.post(this._con.baseUrl+"coregame/pickhero", pickHeroDto).pipe(
      tap((response) =>{
        console.log(response);
      })
    )
  }
}
