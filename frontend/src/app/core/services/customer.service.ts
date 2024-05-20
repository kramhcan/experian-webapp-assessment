import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer.model';
import { CustomHttpClientService } from './custom-http-client.service';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http: CustomHttpClientService) { }

  getCustomers(): Observable<Customer[]> {
    return this.http.get('');
  }

  getCustomerById(id: string): Observable<Customer> {
    return this.http.get(`byId/${id}`);
  }

  getCustomerByCriteria(criteriaType: string, criteriaValue: string): Observable<Customer[]> {
    return this.http.get(`byCriteria?criteriaType=${criteriaType}&criteriaValue=${criteriaValue}`);
  }

  createCustomer(customer: Customer): Observable<Customer> {
    return this.http.post('', customer);
  }
}
