import { Component } from '@angular/core';
import { Expense } from '../../models/expense.model';
import { Router } from '@angular/router';
import { ExpenseService } from '../../services/expense.service';
import { Category } from '../../models/category.model';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-expense-manager',
  templateUrl: './expense-list.component.html',
  styleUrls: ['./expense-list.component.css'],
})
export class ExpenseListComponent {
  selectedExpense: Expense = { id: 0, categoryId: 0, description: "", category: {}, amount: 0, expenseDate: new Date() }; // Add default values for fields

  constructor(private expenseService: ExpenseService, private categoryService: CategoryService, private router: Router) { }

  expenses: Expense[] = [];
  categories: Category[] = [];

  ngOnInit(): void {
    this.loadExpenses();
    this.loadCategories();
  }

  loadExpenses(): void {
    this.expenseService.getExpenses().subscribe({
      next: (data) => {
        this.expenses = data;
        if (this.expenses.length > 0) {
          this.selectedExpense = this.expenses[0];
        }
      },
      error: (err) => {
        console.error('Error loading expenses:', err);
      },
    });
  }

  loadCategories(): void {
    this.categoryService.getCategories().subscribe({
      next: (data) => {
        this.categories = data;
      },
      error: (err) => {
        console.error('Error loading expenses:', err);
      },
    });
  }
  openUpdateExpenseModal(expense: Expense): void {
    this.selectedExpense = { ...expense }; 
  }
  openReadExpenseModal(expense: Expense): void {
    this.selectedExpense = { ...expense };
  }
  openDeleteExpenseModal(expense: Expense): void {
    this.selectedExpense = { ...expense };
  }

  closeModal(): void {
    this.selectedExpense = { id: 0, categoryId: 0, description: "", category: {}, amount: 0, expenseDate: new Date() };
  }


  updateExpense(): void {
    if (this.selectedExpense) {
      this.expenseService.updateExpense(this.selectedExpense.id,this.selectedExpense).subscribe({
        next: () => {
          console.log('Expense updated successfully');
          this.closeModal();
          this.loadExpenses();
        },
        error: (err) => {
          console.error('Error updating expense:', err);
        },
      });
    }
  }

  createExpense(): void {
    if (this.selectedExpense) {
      this.expenseService.createExpense(this.selectedExpense).subscribe({
        next: () => {
          console.log('Expense created successfully');
          this.closeModal();
          this.loadExpenses();
        },
        error: (err) => {
          console.error('Error creating expense:', err);
        },
      });
    }
  }

  deleteExpense(): void {
    if (this.selectedExpense) {
      this.expenseService.deleteExpense(this.selectedExpense.id).subscribe({
        next: () => {
          console.log('Expense deleted successfully');
          this.closeModal();
          this.loadExpenses();
        },
        error: (err) => {
          console.error('Error deleting expense:', err);
        },
      });
    }
  }


}
