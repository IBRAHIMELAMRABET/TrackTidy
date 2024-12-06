import { Component, Input, OnInit } from '@angular/core';
import { ChartsService } from '../charts.service';

@Component({
  selector: 'app-donut-chart',
  templateUrl: './donut-chart.component.html',
  styleUrls: ['./donut-chart.component.css']
})
export class DonutChartComponent implements OnInit {
  chartOptions: any;

  constructor(private chartsService: ChartsService) { }

  ngOnInit(): void {
    this.fetchChartData();
  }

  fetchChartData(): void {
    this.chartsService.getExpensesByCategory().subscribe((data) => {
      this.setupChart(data);
    });
  }

  setupChart(data: { categories: string[]; expenses: number[] }): void {
    this.chartOptions = {
      series: data.expenses,
      colors: ['#1C64F2', '#16BDCA', '#FDBA8C', '#E74694'],
      chart: {
        height: 320,
        width: '100%',
        type: 'donut'
      },
      plotOptions: {
        pie: {
          donut: {
            size: '80%',
            labels: {
              show: true,
              total: {
                showAlways: true,
                show: true,
                label: 'Total Expenses',
                formatter: function (w: any) {
                  return `$${w.globals.seriesTotals.reduce((a: number, b: number) => a + b, 0)}`;
                }
              }
            }
          }
        }
      },
      labels: data.categories,
      legend: {
        position: 'bottom',
        fontFamily: 'Inter, sans-serif'
      }
    };
  }
}
