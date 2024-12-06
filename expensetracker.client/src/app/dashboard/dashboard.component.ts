import { CommonModule } from "@angular/common";
import { Component, OnInit } from '@angular/core';

@Component({
  selector: "expense-dashboard",
  templateUrl: `./dashboard.component.html`,
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {

    ngOnInit(): void {
        throw new Error("Method not implemented.");
    }
  
}
