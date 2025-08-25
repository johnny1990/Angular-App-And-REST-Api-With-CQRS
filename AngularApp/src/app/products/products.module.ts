import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductFormComponent } from './product-form/product-form.component';
import { ProductReportComponent } from './product-report/product-report.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ProductEditComponent } from './product-edit/product-edit.component';

const routes: Routes = [
  { path: '', component: ProductListComponent },
  { path: 'add', component: ProductFormComponent },
  { path: 'report', component: ProductReportComponent },
  { path: 'edit/:id', component: ProductEditComponent }
];

@NgModule({
  declarations: [
    ProductListComponent,
    ProductFormComponent,
    ProductReportComponent,
    ProductEditComponent
  ],
  imports: [CommonModule, FormsModule, RouterModule.forChild(routes)],
})
export class ProductsModule {}
