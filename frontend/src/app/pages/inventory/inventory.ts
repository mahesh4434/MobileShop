import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';

export interface InventoryDto {
  productId: number;
  productName: string;
  stockAvailable: number;
  reorderStock: number;
}

@Component({
  selector: 'app-inventory',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './inventory.html',
  styleUrl: './inventory.css',
})
export class Inventory implements OnInit {
  httpClient = inject(HttpClient);
  inventoryDto: InventoryDto[] = [];
  isSubmitting: boolean = false;

  // Initializing with null or empty strings to trigger 'required' validation
  inventoryData = {
    productId: null as any,
    productName: '',
    stockAvailable: null as any,
    reorderStock: null as any
  };

  private apiUrl = 'https://localhost:7273/api/inventory';

  ngOnInit(): void {
    this.getInventoryData();
  }

  getInventoryData(): void {
    this.httpClient
      .get<InventoryDto[]>(this.apiUrl)
      .subscribe({
        next: (data) => {
          this.inventoryDto = data;
        },
        error: (err) => console.error('Fetch error', err)
      });
  }

  // Acceptance of the 'form' argument fixes the TS2554 error
  onSubmit(form: NgForm) {
    if (form.invalid) {
      return;
    }

    this.isSubmitting = true;

    const httpOptions = {
      headers: new HttpHeaders({
        'Authorization': 'Mahesh-auth-token',
        'Content-Type': 'application/json'
      })
    };

    this.httpClient.post(this.apiUrl, this.inventoryData, httpOptions).subscribe({
      next: (v) => {
        this.getInventoryData();
      },
      error: (e) => {
        console.error('Submission error', e);
        this.isSubmitting = false;
      },
      complete: () => {
        alert('Data submitted successfully!');
        this.isSubmitting = false;
        this.resetForm(form);
      }
    });
  }

  resetForm(form: NgForm) {
    form.resetForm(); // Clears validation state and reset inputs
    this.inventoryData = {
      productId: null as any,
      productName: '',
      stockAvailable: null as any,
      reorderStock: null as any
    };
  }
}