import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditShippingCompanyComponent } from './edit-shipping-company.component';

describe('EditShippingCompanyComponent', () => {
  let component: EditShippingCompanyComponent;
  let fixture: ComponentFixture<EditShippingCompanyComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditShippingCompanyComponent]
    });
    fixture = TestBed.createComponent(EditShippingCompanyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
