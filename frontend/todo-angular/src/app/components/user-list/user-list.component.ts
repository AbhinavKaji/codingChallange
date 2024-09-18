import { CommonModule } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { TodoService } from '../../services/todo.service';
import { IUser } from '../../model/interface/user';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css',
})
export class UserListComponent implements OnInit {
  users: IUser[] = [];
  selectedUserId: number | null = null;
  @Output() userSelected = new EventEmitter<number>();
  constructor(private todoService: TodoService) {}

  ngOnInit(): void {
    this.getAllUser();
  }

  getAllUser() {
    this.todoService.getUsers().subscribe((users) => {
      this.users = users;
    });
  }
  selectUser(userId: number): void {
    this.selectedUserId = userId;
    this.userSelected.emit(userId);
  }
}
