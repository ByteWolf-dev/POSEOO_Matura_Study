import { Component, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { CruiserShipDto } from 'src/app/models/cruiser-ship-dto.model';

@Component({
  selector: 'app-ship-stats-panel',
  templateUrl: './ship-stats-panel.component.html',
  styleUrls: ['./ship-stats-panel.component.css']
})
export class ShipStatsPanelComponent implements OnChanges {
  @Input() ships: CruiserShipDto[] = [];
  @Output() shipSelected = new EventEmitter<CruiserShipDto>();

  averageYear: number | null = null;
  totalPassengers: number = 0;
  averageCrew: number | null = null;
  shipsWithRemarks: number = 0;

  ngOnChanges(): void {
    this.calculateStats();
  }

  calculateStats(): void {
    if (!this.ships || this.ships.length === 0) {
      this.averageYear = null;
      this.totalPassengers = 0;
      this.averageCrew = null;
      this.shipsWithRemarks = 0;
      return;
    }

    const validYears = this.ships.map(s => s.yearOfConstruction).filter(y => !!y);
    this.averageYear = validYears.length ? Math.round(validYears.reduce((a, b) => a + b, 0) / validYears.length) : null;

    this.totalPassengers = this.ships
      .map(s => s.passengers ?? 0)
      .reduce((a, b) => a + b, 0);

    const crews = this.ships.map(s => s.crew).filter(c => c !== undefined && c !== null) as number[];
    this.averageCrew = crews.length ? Math.round(crews.reduce((a, b) => a + b, 0) / crews.length) : null;

    this.shipsWithRemarks = this.ships.filter(s => !!s.remark).length;
  }

  selectShip(ship: CruiserShipDto): void {
    this.shipSelected.emit(ship);
  }
}
