import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Course } from '../../models/course';
import { HighlightDirective } from '../../directives/highlight.directive';
import { CreditLabelPipe } from '../../pipes/credit-label.pipe';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-course-card',
  standalone: true,
  imports: [CommonModule, HighlightDirective, CreditLabelPipe, RouterLink],
  template: `
    <div [ngClass]="{'available': course.isAvailable, 'unavailable': !course.isAvailable}" style="border: 1px solid #ccc; padding: 1rem; border-radius: 8px; width: 200px;">
      <h4 appHighlight>{{ course.title }}</h4>
      <p [ngSwitch]="course.isAvailable">
        <span *ngSwitchCase="true">Available</span>
        <span *ngSwitchCase="false">Not Available</span>
      </p>
      <p [ngStyle]="{'font-weight': 'bold'}">{{ course.credits | creditLabel }}</p>
      <button (click)="courseSelected.emit(course)">Select</button>
      <a [routerLink]="['/course', course.id]" style="display: block; margin-top: 0.5rem;">View Details</a>
    </div>
  `,
  styles: [`
    .available { background-color: #e6ffe6; }
    .unavailable { background-color: #ffe6e6; }
  `]
})
export class CourseCardComponent {
  @Input() course!: Course;
  @Output() courseSelected = new EventEmitter<Course>();
}
