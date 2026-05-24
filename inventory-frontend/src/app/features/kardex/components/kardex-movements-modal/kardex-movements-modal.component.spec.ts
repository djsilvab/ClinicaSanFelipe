import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KardexMovementsModalComponent } from './kardex-movements-modal.component';

describe('KardexMovementsModalComponent', () => {
  let component: KardexMovementsModalComponent;
  let fixture: ComponentFixture<KardexMovementsModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KardexMovementsModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KardexMovementsModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
