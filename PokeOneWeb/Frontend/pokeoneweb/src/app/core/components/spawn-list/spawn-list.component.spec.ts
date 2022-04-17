import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpawnListComponent } from './spawn-list.component';

describe('SpawnListComponent', () => {
  let component: SpawnListComponent;
  let fixture: ComponentFixture<SpawnListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SpawnListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SpawnListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
