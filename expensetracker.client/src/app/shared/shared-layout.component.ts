import { Component } from "@angular/core";

@Component({
  selector: 'app-shared-layout',
  template: `
    <div class="h-screen bg-gray-50 dark:bg-gray-900">
      <app-navbar></app-navbar>
      <div class="antialiased bg-gray-50 dark:bg-gray-900">
      <main class="p-4 md:ml-64 h-auto pt-20 bg-gray-50 dark:bg-gray-900 ">
        <app-sidebar></app-sidebar>
        <router-outlet></router-outlet>
      </main>
      </div>
    </div>
  `
})
export class SharedLayoutComponent { }
