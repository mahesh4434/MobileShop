import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Inventory } from './pages/inventory/inventory';
import { Customer } from './pages/customer/customer';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Online shop');
}
