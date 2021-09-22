import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChooseHeroLayoutComponent } from './choose-hero-layout.component';

describe('ChooseHeroLayoutComponent', () => {
  let component: ChooseHeroLayoutComponent;
  let fixture: ComponentFixture<ChooseHeroLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChooseHeroLayoutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ChooseHeroLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
