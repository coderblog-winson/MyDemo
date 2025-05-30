import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { ReportService } from '../services/report.service';
import { LoadingService } from '../services/loading.service';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss'],
})
export class ReportComponent implements OnInit, OnDestroy {
  private destroySubject = new Subject();

  constructor(
    private reportService: ReportService,
    private loadingService: LoadingService
  ) {}

  ngOnInit() {
    this.loadingService.start();
    this.reportService.GenerateReport().subscribe({
      next: (response) => {
        // Handle the PDF blob response
        this.reportService.setData(response);
      },
      error: (error) => {
        console.error('Error generating report:', error);
      },
      complete: () => {
        this.loadingService.stop();
      },
    });
  }

  //release the subject object
  ngOnDestroy() {
    // emit a value with the takeUntil notifier
    this.destroySubject.next(true);
    // complete the subject
    this.destroySubject.complete();
  }
}
