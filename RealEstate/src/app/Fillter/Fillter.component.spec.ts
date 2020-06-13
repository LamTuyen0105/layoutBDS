/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { FillterComponent } from './Fillter.component';

describe('FillterComponent', () => {
  let component: FillterComponent;
  let fixture: ComponentFixture<FillterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FillterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FillterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
