import { Component } from '@angular/core';
import { Category } from '../models/category.model';
import { CategoryService } from '../services/category.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrl: './category.component.css'
})
export class CategoryComponent {
  selectedCategory: Category = { id: 0, description: "", name: "", icon:""}; 

  constructor(private categoryService: CategoryService, private router: Router) { }

  categories: Category[] = [];

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryService.getCategories().subscribe({
      next: (data) => {
        this.categories = data;
      },
      error: (err) => {
        console.error('Error loading categorys:', err);
      },
    });
  }
  openUpdateCategoryModal(category: Category): void {
    this.selectedCategory = { ...category };
  }
  openReadCategoryModal(category: Category): void {
    this.selectedCategory = { ...category };
  }
  openDeleteCategoryModal(category: Category): void {
    this.selectedCategory = { ...category };
  }

  closeModal(): void {
    this.selectedCategory = { id: 0, description: "", name: "", icon: "" }; 
  }


  updateCategory(): void {
    if (this.selectedCategory) {
      this.categoryService.updateCategory(this.selectedCategory.id, this.selectedCategory).subscribe({
        next: () => {
          console.log('category updated successfully');
          this.closeModal();
          this.loadCategories();
        },
        error: (err) => {
          console.error('Error updating category:', err);
        },
      });
    }
  }

  createCategory(): void {
    if (this.selectedCategory) {
      this.categoryService.createCategory(this.selectedCategory).subscribe({
        next: () => {
          console.log('category created successfully');
          this.closeModal();
          this.loadCategories();
        },
        error: (err) => {
          console.error('Error creating category:', err);
        },
      });
    }
  }

  deleteCategory(): void {
    if (this.selectedCategory) {
      this.categoryService.deleteCategory(this.selectedCategory.id).subscribe({
        next: () => {
          console.log('category deleted successfully');
          this.closeModal();
          this.loadCategories();
        },
        error: (err) => {
          console.error('Error deleting category:', err);
        },
      });
    }
  }

}
