import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { HeroAttribute } from 'src/app/_models/enums/enumsDtos';
import { IngameHeroDto } from 'src/app/_models/HeroDtos/ingameHeroDto';
import { GameService } from 'src/app/_services/gameservice/game.service';
import { CharacterInfoService } from '../../services/character-info.service';

@Component({
  selector: 'app-character-info',
  templateUrl: './character-info.component.html',
  styleUrls: ['./character-info.component.scss']
})
export class CharacterInfoComponent implements OnInit {
  heroAttributeArmor = HeroAttribute.ARMOR;
  heroAttributeDmg = HeroAttribute.DMG;
  heroAttributeHp = HeroAttribute.HP;

  heroInformation$ : Observable<IngameHeroDto>;
  constructor( private toastr: ToastrService, 
  private characterService: CharacterInfoService,
  private gameService: GameService) {
    
   }

  ngOnInit(): void {
    this.getCharacterInfo();
  }

  getCharacterInfo(){
    this.heroInformation$ = this.characterService.getHeroInformation();
  }

  upgradeHeroSkill(skill: HeroAttribute){
    console.log(skill);
    this.characterService.upgradeHeroSkill(skill).subscribe((result)=>{
      this.ngOnInit();
    }, error =>{
      this.toastr.error(error.error);
    });
  }

}
