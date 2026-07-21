import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EnrollmentService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:3000/enrollments';

  enrollStudent(data: any): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }
}
