/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { NeedToRentComponent } from './NeedToRent.component';

describe('NeedToRentComponent', () => {
  let component: NeedToRentComponent;
  let fixture: ComponentFixture<NeedToRentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NeedToRentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NeedToRentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
