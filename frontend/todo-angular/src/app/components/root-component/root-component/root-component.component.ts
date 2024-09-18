import { Component } from '@angular/core';
import { TodoDetailsComponent } from '../../todo-details/todo-details.component';
import { TodoListComponent } from '../../todo-list/todo-list.component';
import { UserListComponent } from '../../user-list/user-list.component';
import { ITodo } from '../../../model/interface/todo';
import { IUser } from '../../../model/interface/user';
import { TodoService } from '../../../services/todo.service';

@Component({
  selector: 'app-root-component',
  standalone: true,
  imports: [TodoListComponent, TodoDetailsComponent, UserListComponent],
  templateUrl: './root-component.component.html',
  styleUrl: './root-component.component.css',
})
export class RootComponentComponent {
  selectedUserId: number = 0;
  selectedTodo!: ITodo;
  users: IUser[] = [];
  constructor(private todoService: TodoService) {}
  onUserSelected(userId: number): void {
    this.selectedUserId = userId;
    this.selectedTodo = {
      id: 0,
      userId: 0,
      name: '',
      complete: false,
      dateCompleted: new Date(),
      dateCreated: new Date(),
    };
  }

  ngOnInit(): void {
    this.loadUsers();
  }
  loadUsers(): void {
    this.todoService.getUsers().subscribe((users) => {
      this.users = users;
    });
  }
  onTodoSelected(todo: ITodo): void {
    this.selectedTodo = todo;
  }
  onTodoCreated(): void {
    this.selectedTodo = {
      id: 0,
      userId: 0,
      name: '',
      complete: false,
      dateCompleted: new Date(),
      dateCreated: new Date(),
    };
  }
}
