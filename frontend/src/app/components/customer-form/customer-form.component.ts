import { Component } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { CustomerService } from '../../core/services/customer.service';

@Component({
  selector: 'app-customer-form',
  templateUrl: './customer-form.component.html',
  styleUrl: './customer-form.component.scss',
})
export class CustomerFormComponent {
  customerForm: FormGroup = new FormGroup({});

  constructor(
    private fb: FormBuilder,
    private customerService: CustomerService
  ) {}

  ngOnInit() {
    this.customerForm = this.fb.group({
      firstName: new FormControl('', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50),
        Validators.pattern('[a-zA-Z ]*'),
      ]),
      lastName: new FormControl('', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50),
        Validators.pattern('[a-zA-Z ]*'),
      ]),
      email: new FormControl('', [Validators.required, Validators.email]),
    });
  }

  onSubmit() {
    // if (this.customerForm.valid)
    //   console.log(this.customerForm.value);
    // else
    //   console.log('Form is invalid');

    if (this.customerForm.valid) {
      const formData = this.customerForm.value;
      this.customerService.createCustomer(formData).subscribe({
        next: (response) => {
          console.log('Customer created successfully:', response);
          // Optionally, reset the form after successful submission
          this.customerForm.reset();
        },
        error: (error) => {
          console.error('Error creating customer:', error);
          // Handle error as needed
        },
      });
    }
  }
}
