import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { TodoService } from '../../services/todo.service';
import { CommonModule } from '@angular/common';
import { ITodo } from '../../model/interface/todo';

@Component({
  selector: 'app-todo-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './todo-list.component.html',
  styleUrl: './todo-list.component.css',
})
export class TodoListComponent {
  @Input() userId: number | null = null;
  todos: ITodo[] = [];
  selectedTodoId: number | null = null;
  @Output() todoSelected = new EventEmitter<ITodo>();
  @Output() todoCreated = new EventEmitter<void>();

  todoService = inject(TodoService);

  addTodo(): void {
    this.todoCreated.emit();
  }

  selectTodo(todo: ITodo): void {
    this.selectedTodoId = todo.id;
    this.todoSelected.emit(todo);
  }

  ngOnChanges(): void {
    if (this.userId) {
      this.todoService.getTodos(this.userId).subscribe((todos) => {
        this.todos = todos;
      });
    }
  }
}
