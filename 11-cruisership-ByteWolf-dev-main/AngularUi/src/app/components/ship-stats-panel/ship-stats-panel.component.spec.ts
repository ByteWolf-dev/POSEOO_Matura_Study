import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShipStatsPanelComponent } from './ship-stats-panel.component';

describe('ShipStatsPanelComponent', () => {
  let component: ShipStatsPanelComponent;
  let fixture: ComponentFixture<ShipStatsPanelComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ShipStatsPanelComponent]
    });
    fixture = TestBed.createComponent(ShipStatsPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
