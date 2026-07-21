import { Component, OnInit, OnDestroy, OnChanges, SimpleChanges, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EnrollmentFormComponent } from '../../components/enrollment-form/enrollment-form.component';
import { ReactiveEnrollmentFormComponent } from '../../components/reactive-enrollment-form/reactive-enrollment-form.component';

@Component({
  selector: 'app-student-profile',
  standalone: true,
  imports: [CommonModule, FormsModule, EnrollmentFormComponent, ReactiveEnrollmentFormComponent],
  template: `
    <h3>Student Profile</h3>
    <input [(ngModel)]="name" />
    <p>Interpolation: {{ name }}</p>
    <app-enrollment-form></app-enrollment-form>
    <app-reactive-enrollment-form></app-reactive-enrollment-form>
  `
})
export class StudentProfileComponent implements OnInit, OnDestroy, OnChanges {
  @Input() name = 'Student';

  ngOnInit(): void {}
  ngOnDestroy(): void {}
  ngOnChanges(changes: SimpleChanges): void {}
}
