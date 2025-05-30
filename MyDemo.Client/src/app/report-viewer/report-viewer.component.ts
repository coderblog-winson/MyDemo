import { Component, OnDestroy } from '@angular/core';
import { PagesLoadedEvent } from 'ngx-extended-pdf-viewer';
import { ReportService } from '../services/report.service';

@Component({
  selector: 'report-viewer',
  templateUrl: './report-viewer.component.html',
  styleUrls: ['./report-viewer.component.scss'],
})
export class ReportViewerComponent implements OnDestroy {
  reportData: any;
  showViewer = false;

  constructor(private reportService: ReportService) {
    // subscribe the report data
    this.reportService.changeEmitted$.subscribe((reportData) => {
      console.log(
        `%c loading report data`,
        'font-size:13px; background:green; color:#ffffff;'
      );
      this.reportData = reportData;
      this.showViewer = true;
    });
  }

  public onPagesLoaded(pagecount: PagesLoadedEvent): void {
    const now = new Date().toLocaleTimeString();
    //console.log(`%c ${now} Loaded a PDF with ${pagecount.pagesCount} pages`, 'font-size:13px; background:pink; color:#bf2c9f;');
  }

  public onEvent(eventName: string, event: any): void {
    const now = new Date().toLocaleTimeString();
    //console.log(`%c ${eventName} event type:  ${event} `, 'font-size:13px; background:pink; color:#bf2c9f;');
  }

  //release the subject object
  ngOnDestroy() {}
}
