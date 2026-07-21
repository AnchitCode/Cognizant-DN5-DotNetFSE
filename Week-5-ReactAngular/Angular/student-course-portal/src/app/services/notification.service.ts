import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  showError(message: string): void {
    console.error(message);
  }

  showSuccess(message: string): void {
    console.log(message);
  }
}
