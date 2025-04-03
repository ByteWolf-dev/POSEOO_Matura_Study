import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShippingCompaniesComponent } from './components/shipping-companies/shipping-companies.component';
import { ShippingCompanyComponent } from './components/shipping-company/shipping-company.component';
import { EditShippingCompanyComponent } from './components/edit-shipping-company/edit-shipping-company.component';

const routes: Routes = [
  {path: 'shipping-companies', component: ShippingCompaniesComponent},
  {path: '', redirectTo: '/shipping-companies', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
