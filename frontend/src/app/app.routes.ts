import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { Inventory } from './pages/inventory/inventory';
import { Customer } from './pages/customer/customer';
import { Bill } from './pages/bill/bill';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: Home },
    { path: 'inventory', component: Inventory },
    { path: 'customer', component: Customer },
    { path: 'bill', component: Bill }
];
