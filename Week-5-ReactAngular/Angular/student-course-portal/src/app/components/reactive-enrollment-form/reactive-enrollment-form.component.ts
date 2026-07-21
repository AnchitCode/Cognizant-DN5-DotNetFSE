import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule, AbstractControl, ValidationErrors } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { delay } from 'rxjs/operators';

@Component({
  selector: 'app-reactive-enrollment-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <div>
        <label>Email</label>
        <input formControlName="email" />
        <div *ngIf="form.get('email')?.errors?.['required']">Email is required.</div>
        <div *ngIf="form.get('email')?.errors?.['emailTaken']">Email is already taken.</div>
      </div>
      <div formArrayName="skills">
        <div *ngFor="let skill of skills.controls; let i = index">
          <input [formControlName]="i" />
        </div>
        <button type="button" (click)="addSkill()">Add Skill</button>
      </div>
      <button type="submit" [disabled]="form.invalid || form.pending">Submit</button>
    </form>
  `
})
export class ReactiveEnrollmentFormComponent {
  private fb = inject(FormBuilder);

  form = this.fb.group({
    email: ['', [Validators.required], [this.asyncValidator]],
    skills: this.fb.array([this.fb.control('')])
  });

  get skills(): FormArray {
    return this.form.get('skills') as FormArray;
  }

  addSkill(): void {
    this.skills.push(this.fb.control(''));
  }

  onSubmit(): void {
    console.log(this.form.value);
  }

  asyncValidator(control: AbstractControl): Observable<ValidationErrors | null> {
    return of(control.value === 'test@test.com' ? { emailTaken: true } : null).pipe(delay(500));
  }
}
