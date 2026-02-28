import { TestBed } from '@angular/core/testing';

import { GeneroCancionService } from './genero-cancion.service';

describe('GeneroCancionService', () => {
  let service: GeneroCancionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GeneroCancionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
