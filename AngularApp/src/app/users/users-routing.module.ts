// src/app/users/users-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TopUsersComponent } from './top-users/top-users.component';

const routes: Routes = [
  { path: '', component: TopUsersComponent } // <-- Displayed at /users
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
