import { Injectable, OnDestroy } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { config } from 'src/assets/config';

@Injectable({
  providedIn: 'root',
})
export class ReportService implements OnDestroy {
  constructor(protected http: HttpClient) {}

  // Observable string sources
  private emitChangeSource = new Subject<any>();
  // Observable string streams
  changeEmitted$ = this.emitChangeSource.asObservable();

  // Set the report data
  setData(reportData: any) {
    this.emitChangeSource.next(reportData);
  }

  ngOnDestroy() {
    // complete and release the subject
    this.emitChangeSource.complete();
  }

  /**
   * Generate the posting reports
   * @returns
   */
  GenerateReport(): Observable<any> {
    var url = config.apiUrl + '/report/generate';
    console.log('Generating report from URL:', url);
    return this.http.get(url, { responseType: 'blob' });
  }
}
