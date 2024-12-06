import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';


import {
  FontAwesomeModule,
  FaIconLibrary,
} from '@fortawesome/angular-fontawesome';
import { faArrowRightFromBracket } from '@fortawesome/free-solid-svg-icons';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { SidebarComponent } from './shared/sidebar/sidebar.component';
import { ExpenseListComponent } from './expense/expense-list/expense-list.component';
import { SharedLayoutComponent } from './shared/shared-layout.component';
import { DarkModeToggleComponent } from './shared/dark-mode-toggle/dark-mode-toggle.component';
import { AuthService } from './services/auth.service';
import { TokenInterceptor } from './services/token.interceptor';
import { FormsModule } from '@angular/forms';
import { DonutChartComponent } from './charts/donut-chart/donut-chart.component';
import { RadialChartComponent } from './charts/radial-chart/radial-chart.component';
import { AreaChartComponent } from './charts/area-chart/area-chart.component';
import { NgApexchartsModule } from 'ng-apexcharts';


@NgModule({
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  declarations: [AppComponent, LoginComponent, RegisterComponent, NavbarComponent, SidebarComponent, ExpenseListComponent, SharedLayoutComponent, DashboardComponent, DonutChartComponent, RadialChartComponent, AreaChartComponent],
  imports: [
    BrowserModule, HttpClientModule, FormsModule, NgApexchartsModule, AppRoutingModule, FontAwesomeModule, DarkModeToggleComponent
  ],
  providers: [
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})

export class AppModule {
  constructor(library: FaIconLibrary) {
    library.addIcons(faArrowRightFromBracket);
  }
}
