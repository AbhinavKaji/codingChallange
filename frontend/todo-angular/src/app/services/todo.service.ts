import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ITodo } from '../model/interface/todo';

@Injectable({
  providedIn: 'root',
})
export class TodoService {
  private apiUrl = 'http://localhost:5086/api';
  //constructor(private http: HttpClient) {}
  http = inject(HttpClient);

  getUsers(): Observable<any> {
    return this.http.get(`${this.apiUrl}/User`);
  }

  getTodos(userId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/Todos/user/${userId}`);
  }

  createTodo(todo: ITodo): Observable<any> {
    return this.http.post(`${this.apiUrl}/Todos`, todo);
  }

  updateTodo(todo: ITodo): Observable<any> {
    return this.http.put(`${this.apiUrl}/Todos`, todo);
  }

  deleteTodo(todoId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/Todos?id=${todoId}`);
  }
}
