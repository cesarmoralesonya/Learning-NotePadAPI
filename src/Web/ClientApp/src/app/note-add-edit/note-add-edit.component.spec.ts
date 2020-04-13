import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NoteAddEditComponent } from './note-add-edit.component';

describe('NoteAddEditComponent', () => {
  let component: NoteAddEditComponent;
  let fixture: ComponentFixture<NoteAddEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NoteAddEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NoteAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
