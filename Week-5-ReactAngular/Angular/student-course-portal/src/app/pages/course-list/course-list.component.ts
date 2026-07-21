import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CourseService } from '../../services/course.service';
import { CourseCardComponent } from '../../components/course-card/course-card.component';
import { Course } from '../../models/course';

@Component({
  selector: 'app-course-list',
  standalone: true,
  imports: [CommonModule, CourseCardComponent],
  template: `
    <h3>Course List</h3>
    <div style="display: flex; gap: 1rem; flex-wrap: wrap;">
      <app-course-card *ngFor="let course of courses; trackBy: trackById" [course]="course" (courseSelected)="onCourseSelected($event)"></app-course-card>
    </div>
  `
})
export class CourseListComponent implements OnInit {
  courses: Course[] = [];
  private courseService = inject(CourseService);

  ngOnInit(): void {
    this.courseService.getCourses().subscribe(data => this.courses = data);
  }

  trackById(index: number, course: Course): string {
    return course.id;
  }

  onCourseSelected(course: Course): void {
    console.log(course);
  }
}
