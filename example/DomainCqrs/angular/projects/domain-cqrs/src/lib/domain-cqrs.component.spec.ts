import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { DomainCqrsComponent } from './domain-cqrs.component';

describe('DomainCqrsComponent', () => {
  let component: DomainCqrsComponent;
  let fixture: ComponentFixture<DomainCqrsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ DomainCqrsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DomainCqrsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
