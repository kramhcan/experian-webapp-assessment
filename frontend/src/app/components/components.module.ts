import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ComponentsRoutingModule } from './components-routing.module';

import { ReactiveFormsModule } from '@angular/forms';
import { CustomerFormComponent } from './customer-form/customer-form.component';
import { CustomerListComponent } from './customer-list/customer-list.component';


@NgModule({
  declarations: [
    CustomerFormComponent,
    CustomerListComponent
  ],
  imports: [
    CommonModule,
    ComponentsRoutingModule,
    ReactiveFormsModule
  ]
})
export class ComponentsModule { }
