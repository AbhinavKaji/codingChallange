import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { TodoService } from '../../services/todo.service';
import { IUser } from '../../model/interface/user';
import { ITodo } from '../../model/interface/todo';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-todo-details',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './todo-details.component.html',
  styleUrl: './todo-details.component.css',
})
export class TodoDetailsComponent {
  @Input() todo!: ITodo;
  @Input() users: IUser[] = [];
  constructor(private todoService: TodoService) {}

  saveTodo(): void {
    if (this.todo.id != 0) {
      this.todoService.updateTodo(this.todo).subscribe({
        next: () => {
          alert('Todo updated successfully!');
        },
        error: () => {
          alert('Error udating todo!');
        },
      });
    } else {
      this.todo.dateCreated = new Date();
      this.todoService.createTodo(this.todo).subscribe({
        next: () => {
          alert('Todo created successfully!');
        },
        error: () => {
          alert('Error creating todo!');
        },
      });
    }
  }
  deleteTodo(): void {
    if (this.todo.id != 0) {
      this.todoService.deleteTodo(this.todo.id).subscribe({
        next: () => {
          alert('Todo deleted successfully!');
        },
        error: () => {
          alert('Error deleting todo!');
        },
      });
    }
    this.todo = {
      id: 0,
      userId: 0,
      name: '',
      complete: false,
      dateCompleted: new Date(),
      dateCreated: new Date(),
    };
  }
}
