import { Category } from "./category.model";

export interface Expense {
  id: number;
  amount: number;
  expenseDate: Date;
  categoryId: number;
  category: Category|any;
  description: string;
  notes?: string[];
}
