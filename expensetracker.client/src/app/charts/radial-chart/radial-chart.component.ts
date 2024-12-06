import { Component, Input, OnInit } from '@angular/core';
import { ChartsService } from '../charts.service';

@Component({
  selector: 'radial-area-chart',
  templateUrl: './radial-chart.component.html',
  styleUrls: ['./radial-chart.component.css']
})
export class RadialChartComponent implements OnInit {
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

  setupChart(data: any): void {
    this.chartOptions = {
      series: [70],
      colors: ["#1C64F2"],
        chart: {
        height: "380px",
        width: "100%",
          type: "radialBar",
            sparkline: {
        enabled: true,
      },
    },
    plotOptions: {
      radialBar: {
        track: {
          background: '#E5E7EB',
        },
        dataLabels: {
          show: false,
        },
        hollow: {
          margin: 0,
            size: "22%",
        }
      },
    },
    labels: ["Expenses by Budget"],
      legend: {
      show: true,
        position: "bottom",
          fontFamily: "Inter, sans-serif",
    },
  };
  }
}
