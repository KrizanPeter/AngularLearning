import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HeroType } from 'src/app/_models/enums/enumsDtos';
import { PickHeroDto } from 'src/app/_models/HeroDtos/PickHeroDto';
import { HeroService } from '../../services/hero.service';

@Component({
  selector: 'app-choose-hero-layout',
  templateUrl: './choose-hero-layout.component.html',
  styleUrls: ['./choose-hero-layout.component.scss']
})
export class ChooseHeroLayoutComponent implements OnInit {

  pickHeroWarlockDto = {heroType: HeroType.Warlock} as PickHeroDto;
  pickHeroThiefDto = {heroType: HeroType.Thief} as PickHeroDto;
  pickHeroSwordsmankDto = {heroType: HeroType.Swordsman} as PickHeroDto;
  pickHeroOracleDto = {heroType: HeroType.Oracle} as PickHeroDto;

  constructor(private heroService: HeroService, private router:Router, private toastr: ToastrService){}

  ngOnInit(): void {
  }

  pickHero(pickHeroDto: PickHeroDto){
    this.heroService.pickHero(pickHeroDto).subscribe(response=>{
      this.router.navigateByUrl('/ingame');
    }, error =>{
      console.log(error);
      this.toastr.error(error.error);
    });
  }
}
