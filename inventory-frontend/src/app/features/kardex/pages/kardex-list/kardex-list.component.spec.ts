import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KardexListComponent } from './kardex-list.component';

describe('KardexListComponent', () => {
  let component: KardexListComponent;
  let fixture: ComponentFixture<KardexListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KardexListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KardexListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
