import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ShippingCompaniesComponent } from './components/shipping-companies/shipping-companies.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { EditShippingCompanyComponent } from './components/edit-shipping-company/edit-shipping-company.component';
import { ShippingCompanyComponent } from './components/shipping-company/shipping-company.component';
import { ShipStatsPanelComponent } from './components/ship-stats-panel/ship-stats-panel.component';

@NgModule({
  declarations: [
    AppComponent,
    ShippingCompaniesComponent,
    EditShippingCompanyComponent,
    ShippingCompanyComponent,
    ShipStatsPanelComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
