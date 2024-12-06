import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChartsService {
  constructor(private http: HttpClient) { }

  getExpensesByCategory(): Observable<{ categories: string[]; expenses: number[] }> {
    return this.http.get<{ categories: string[]; expenses: number[] }>('/api/expenses-by-category').pipe(
      catchError(() => {
        return of({
          categories: ['Food', 'Transport', 'Entertainment', 'Healthcare', 'Utilities'],
          expenses: [300, 150, 100, 200, 50]
        });
      })
    );
  }

  getExpensesBudgets(): Observable<any> {
    return this.http.get<any>('/api/expenses-budgets').pipe(
      catchError(() => {
        return of([
          {
            name: "Expenses",
            data: [1500, 1418, 1456, 1526, 1356, 1256],
            color: "#1A56DB",
          },
          {
            name: "Budgets",
            data: [643, 413, 765, 412, 1423, 1731],
            color: "#7E3BF2",
          },
        ]);
      })
    );
  }



}
