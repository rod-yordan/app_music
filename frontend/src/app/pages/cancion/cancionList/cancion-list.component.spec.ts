import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CancionListComponent } from './cancion-list.component';

describe('CancionListComponent', () => {
  let component: CancionListComponent;
  let fixture: ComponentFixture<CancionListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CancionListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CancionListComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
