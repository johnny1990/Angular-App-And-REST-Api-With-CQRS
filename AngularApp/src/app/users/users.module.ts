// src/app/users/users.module.ts
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersRoutingModule } from './users-routing.module';
import { TopUsersComponent } from './top-users/top-users.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    TopUsersComponent
  ],
  imports: [
    CommonModule,
    UsersRoutingModule,
    HttpClientModule
  ]
})
export class UsersModule { }
