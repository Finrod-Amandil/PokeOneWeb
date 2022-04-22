import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlacedItemListComponent } from './placed-item-list.component';

describe('PlacedItemListComponent', () => {
  let component: PlacedItemListComponent;
  let fixture: ComponentFixture<PlacedItemListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlacedItemListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlacedItemListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
