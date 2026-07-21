import { Component, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { Store } from '@ngrx/store';
import { enroll } from '../../store/actions/enrollment.actions';

@Component({
  selector: 'app-enrollment-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <form #form="ngForm" (ngSubmit)="onSubmit(form)">
      <div>
        <label>Name</label>
        <input type="text" name="name" ngModel required #name="ngModel" />
        <div *ngIf="name.invalid && name.touched">Name is required.</div>
      </div>
      <button type="submit" [disabled]="form.invalid">Submit</button>
      <button type="button" (click)="resetForm(form)">Reset</button>
      <div *ngIf="successMessage">{{ successMessage }}</div>
    </form>
  `
})
export class EnrollmentFormComponent {
  @ViewChild('form') form!: NgForm;
  successMessage = '';
  private store = inject(Store);

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.store.dispatch(enroll({ data: form.value }));
      this.successMessage = 'Enrollment successful!';
    }
  }

  resetForm(form: NgForm): void {
    form.resetForm();
    this.successMessage = '';
  }
}
