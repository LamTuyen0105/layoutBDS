/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MasteradsComponent } from './Masterads.component';

describe('MasteradsComponent', () => {
  let component: MasteradsComponent;
  let fixture: ComponentFixture<MasteradsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MasteradsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MasteradsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
