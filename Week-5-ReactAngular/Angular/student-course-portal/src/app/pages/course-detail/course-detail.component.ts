import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CourseService } from '../../services/course.service';
import { Course } from '../../models/course';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-course-detail',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div *ngIf="course">
      <h2>{{ course.title }}</h2>
      <p>{{ course.description }}</p>
      <p>Instructor: {{ course.instructor }}</p>
    </div>
  `
})
export class CourseDetailComponent implements OnInit {
  course?: Course;
  private route = inject(ActivatedRoute);
  private courseService = inject(CourseService);

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.courseService.getCourse(id).subscribe(data => this.course = data);
    }
  }
}
