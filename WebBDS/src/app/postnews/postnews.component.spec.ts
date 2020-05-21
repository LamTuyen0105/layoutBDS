/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PostnewsComponent } from './postnews.component';

describe('PostnewsComponent', () => {
  let component: PostnewsComponent;
  let fixture: ComponentFixture<PostnewsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PostnewsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PostnewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
