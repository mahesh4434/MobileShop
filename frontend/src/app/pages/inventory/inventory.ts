import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms'; // Add this

export interface InventoryDto {
  productId: number;
  productName: string;
  stockAvailable: number;
  reorderStock: number;
}

@Component({
  selector: 'app-inventory',
  standalone: true, // Ensure standalone is true if using imports here
  imports: [FormsModule, CommonModule], // Add FormsModule here
  templateUrl: './inventory.html',
  styleUrl: './inventory.css',
})
export class Inventory {

  httpClient = inject(HttpClient);
  inventoryDto: InventoryDto[] = [];

  inventoryData = {
    productId: 0,
    productName: '',
    stockAvailable: 0,
    reorderStock: 0
  };

  ngOnInit(): void {
    const apiUrl = 'https://localhost:7273/api/inventory';

    this.httpClient
      .get<InventoryDto[]>(apiUrl)
      .subscribe({
        next: (data: InventoryDto[]) => {
          this.inventoryDto = data;
          console.log('Inventory data fetched successfully', data);
        },
        error: (err) => {
          console.error('Error fetching inventory data', err);
        }
      });
  }


  onSubmit() {
    debugger;
    let apiUrl = 'https://localhost:7273/api/inventory';
    let httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Mahesh-auth-token',
        'Content-Type': 'application/json'
      })
    };
    this.httpClient.post(apiUrl, this.inventoryData, httpOptions).subscribe(

      {
        next: v => console.log('Inventory data submitted successfully', v),
        error: e => console.error('Error submitting inventory data', e),
        complete: () => {
          alert('Inventory data submission process completed.' + JSON.stringify(this.inventoryData));
        }
      }
    );
  }
}