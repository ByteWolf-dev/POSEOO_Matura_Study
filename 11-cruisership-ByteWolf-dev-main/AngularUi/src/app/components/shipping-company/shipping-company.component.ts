import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { ShippingCompanyDto } from 'src/app/models/shipping-company-dto.model';
import { CruiserShipDto } from 'src/app/models/cruiser-ship-dto.model';
import { CruiserService } from 'src/app/services/cruiser.service';

@Component({
  selector: 'app-shipping-company',
  templateUrl: './shipping-company.component.html',
  styleUrls: ['./shipping-company.component.css'],
})
export class ShippingCompanyComponent implements OnInit {

  id: number = 0;
  company: ShippingCompanyDto = {
    id : 0,
    name : '',
  }

  constructor(private route: ActivatedRoute, private cruiserService: CruiserService, private router: Router) {}

  ngOnInit(): void {
    //TODO get id from route and load company
    this.route.params.subscribe(params => {
      this.id = +params['id'];
      //TODO
    });
  }

  load(): void {
    this.cruiserService.getCompany(this.id).subscribe({
      next: data=>{
      },
      error: error=>{
        console.error('Error loading company:', error);
      }
    });
  }

  /*
  editCompany(company: ShippingCompanyDto): void {
    this.router.navigate(['/edit-shipping-company',company.id]);
  } 
  */ 
}
