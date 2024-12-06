import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DarkModeService {
  private isDarkMode = new BehaviorSubject<boolean>(false);
  isDarkMode$ = this.isDarkMode.asObservable();

  constructor() {
    this.checkInitialMode();
  }

  private checkInitialMode() {
    const storedMode = localStorage.getItem('darkMode');
    const systemPrefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    if (storedMode !== null) {
      this.setDarkMode(JSON.parse(storedMode));
    } else {
      this.setDarkMode(systemPrefersDark);
    }
  }

  toggleDarkMode() {
    const newMode = !this.isDarkMode.value;
    this.setDarkMode(newMode);
  }

  setDarkMode(isDark: boolean) {
    this.isDarkMode.next(isDark);
    localStorage.setItem('darkMode', JSON.stringify(isDark));

    if (isDark) {
      document.documentElement.classList.add('dark');
    } else {
      document.documentElement.classList.remove('dark');
    }
  }
}
