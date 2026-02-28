import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CancionEditarComponent } from './cancion-editar.component';

describe('CancionEditarComponent', () => {
  let component: CancionEditarComponent;
  let fixture: ComponentFixture<CancionEditarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CancionEditarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CancionEditarComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
