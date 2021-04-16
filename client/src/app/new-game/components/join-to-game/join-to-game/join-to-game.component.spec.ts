import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JoinToGameComponent } from './join-to-game.component';

describe('JoinToGameComponent', () => {
  let component: JoinToGameComponent;
  let fixture: ComponentFixture<JoinToGameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JoinToGameComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JoinToGameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
