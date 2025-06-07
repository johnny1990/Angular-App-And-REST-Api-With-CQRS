// src/app/users/users.component.ts
import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../api.service';

@Component({
  selector: 'app-users',
  templateUrl: './top-users.component.html'
})
export class TopUsersComponent implements OnInit {
  users: any[] = [];
  message: string = '';

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.api.getTopSpendingUsers().subscribe({
      next: (res) => {
        this.users = res;
      },
      error: (err) => {
        this.message = 'Failed to load users: ' + err.message;
      }
    });
  }
}
