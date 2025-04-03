import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CompanyOverview } from '../models/company-overview.model';
import { ShippingCompanyDto } from '../models/shipping-company-dto.model';
import { tick } from '@angular/core/testing';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
    //,'Authorization': 'my-auth-token'
  })
}
@Injectable({
  providedIn: 'root'
})
export class CruiserService {

  private apiUrl = "http://localhost:5000";

  constructor(private http: HttpClient) { }

  getCompanies() {
    return this.http.get<CompanyOverview[]>(`${this.apiUrl}/ShippingCompany/overview`);
  }
  
  getCompany(id: number) {
    return this.http.get<ShippingCompanyDto>(`${this.apiUrl}/ShippingCompany/${id}`);
  }

  updateCompany(company: ShippingCompanyDto) {
    const url = `${this.apiUrl}/ShippingCompany/${company.id}`;
    return this.http.put(url, company);
  }
}
