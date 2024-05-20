import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../../core/services/customer.service';
import { Customer } from '../../core/models/customer.model';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrl: './customer-list.component.scss',
})
export class CustomerListComponent implements OnInit {
  customers: Customer[] = [];
  errorMessage = '';
  searchText: string = '';
  //id set as default
  searchCriteria: string = 'id';

  constructor(private customerService: CustomerService) {}

  ngOnInit(): void {
    this.getCustomers();
  }

  getCustomers() {
    this.customerService.getCustomers().subscribe({
      next: (data) => {
        this.customers = data;
      },
      error: (error) => {
        this.errorMessage = 'There was an error retrieving the customer data';
        console.error('Error:', error);
      },
    });
  }

  search(): void {
    this.applySearchFilter();
  }

  reset(): void {
    this.searchText = '';
    this.getCustomers();
  }

  applySearchFilter(): void {
    if (this.searchCriteria === 'id') {
      this.customerService.getCustomerById(this.searchText)
      .subscribe({
        next: (data) => {
          this.customers = [data];
        },
        error: (error) => {
          console.error('Error searching customers:', error);
        }
      });
      return;
    }

    this.customerService.getCustomerByCriteria(this.searchCriteria, this.searchText)
      .subscribe({
        next: (data) => {
          this.customers = data;
        },
        error: (error) => {
          console.error('Error searching customers:', error);
        }
      });
      return;
  }
}
