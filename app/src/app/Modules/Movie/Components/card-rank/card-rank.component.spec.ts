import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CardRankComponent } from './card-rank.component';

describe('CardRankComponent', () => {
  let component: CardRankComponent;
  let fixture: ComponentFixture<CardRankComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CardRankComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CardRankComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
