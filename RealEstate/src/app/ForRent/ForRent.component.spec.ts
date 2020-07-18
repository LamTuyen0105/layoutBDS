/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ForRentComponent } from './ForRent.component';

describe('ForRentComponent', () => {
  let component: ForRentComponent;
  let fixture: ComponentFixture<ForRentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ForRentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ForRentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
