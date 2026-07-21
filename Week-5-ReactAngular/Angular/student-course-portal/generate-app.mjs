import fs from 'fs/promises';
import path from 'path';

const files = {
  'src/app/services/course.service.ts': `
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, catchError, retry } from 'rxjs/operators';
import { Course } from '../models/course';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  private http = inject(HttpClient);
  private notification = inject(NotificationService);
  private apiUrl = 'http://localhost:3000/courses';

  getCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(this.apiUrl).pipe(
      retry(2),
      catchError(err => {
        this.notification.showError('Failed to fetch courses');
        throw err;
      })
    );
  }

  getCourse(id: string): Observable<Course> {
    return this.http.get<Course>(`${this.apiUrl}/${id}`).pipe(
      catchError(err => {
        this.notification.showError('Course not found');
        throw err;
      })
    );
  }

  addCourse(course: Course): Observable<Course> {
    return this.http.post<Course>(this.apiUrl, course);
  }

  updateCourse(id: string, course: Course): Observable<Course> {
    return this.http.put<Course>(`${this.apiUrl}/${id}`, course);
  }

  deleteCourse(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
`,
  'src/app/services/notification.service.ts': `
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
`,
  'src/app/services/enrollment.service.ts': `
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
`,
  'src/app/store/actions/enrollment.actions.ts': `
import { createAction, props } from '@ngrx/store';

export const enroll = createAction('[Enrollment] Enroll Student', props<{ data: any }>());
export const enrollSuccess = createAction('[Enrollment] Enroll Success', props<{ result: any }>());
export const enrollFailure = createAction('[Enrollment] Enroll Failure', props<{ error: any }>());
`,
  'src/app/store/reducers/enrollment.reducer.ts': `
import { createReducer, on } from '@ngrx/store';
import * as EnrollmentActions from '../actions/enrollment.actions';

export interface EnrollmentState {
  loading: boolean;
  error: any;
  success: boolean;
}

export const initialState: EnrollmentState = {
  loading: false,
  error: null,
  success: false
};

export const enrollmentReducer = createReducer(
  initialState,
  on(EnrollmentActions.enroll, state => ({ ...state, loading: true, error: null, success: false })),
  on(EnrollmentActions.enrollSuccess, state => ({ ...state, loading: false, success: true })),
  on(EnrollmentActions.enrollFailure, (state, { error }) => ({ ...state, loading: false, error }))
);
`,
  'src/app/store/selectors/enrollment.selectors.ts': `
import { createFeatureSelector, createSelector } from '@ngrx/store';
import { EnrollmentState } from '../reducers/enrollment.reducer';

export const selectEnrollmentState = createFeatureSelector<EnrollmentState>('enrollment');
export const selectEnrollmentLoading = createSelector(selectEnrollmentState, state => state.loading);
export const selectEnrollmentError = createSelector(selectEnrollmentState, state => state.error);
export const selectEnrollmentSuccess = createSelector(selectEnrollmentState, state => state.success);
`,
  'src/app/store/effects/enrollment.effects.ts': `
import { Injectable, inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { EnrollmentService } from '../../services/enrollment.service';
import * as EnrollmentActions from '../actions/enrollment.actions';
import { catchError, map, switchMap } from 'rxjs/operators';
import { of } from 'rxjs';

@Injectable()
export class EnrollmentEffects {
  private actions$ = inject(Actions);
  private enrollmentService = inject(EnrollmentService);

  enroll$ = createEffect(() =>
    this.actions$.pipe(
      ofType(EnrollmentActions.enroll),
      switchMap(action =>
        this.enrollmentService.enrollStudent(action.data).pipe(
          map(result => EnrollmentActions.enrollSuccess({ result })),
          catchError(error => of(EnrollmentActions.enrollFailure({ error })))
        )
      )
    )
  );
}
`,
  'src/app/interceptors/loading.interceptor.ts': `
import { HttpInterceptorFn } from '@angular/common/http';
import { finalize } from 'rxjs/operators';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    finalize(() => {
    })
  );
};
`,
  'src/app/interceptors/auth.interceptor.ts': `
import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const cloned = req.clone({
    setHeaders: {
      Authorization: 'Bearer dummy-token'
    }
  });
  return next(cloned);
};
`,
  'src/app/interceptors/error.interceptor.ts': `
import { HttpInterceptorFn } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError(err => {
      return throwError(() => err);
    })
  );
};
`,
  'src/app/app.config.ts': `
import { ApplicationConfig, provideZoneChangeDetection, isDevMode } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideStore } from '@ngrx/store';
import { provideEffects } from '@ngrx/effects';
import { provideStoreDevtools } from '@ngrx/store-devtools';

import { routes } from './app.routes';
import { loadingInterceptor } from './interceptors/loading.interceptor';
import { authInterceptor } from './interceptors/auth.interceptor';
import { errorInterceptor } from './interceptors/error.interceptor';
import { enrollmentReducer } from './store/reducers/enrollment.reducer';
import { EnrollmentEffects } from './store/effects/enrollment.effects';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withInterceptors([loadingInterceptor, authInterceptor, errorInterceptor])),
    provideStore({ enrollment: enrollmentReducer }),
    provideEffects([EnrollmentEffects]),
    provideStoreDevtools({ maxAge: 25, logOnly: !isDevMode() })
  ]
};
`,
  'src/app/app.routes.ts': `
import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { CourseListComponent } from './pages/course-list/course-list.component';
import { StudentProfileComponent } from './pages/student-profile/student-profile.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'courses', component: CourseListComponent, canActivate: [authGuard] },
  { 
    path: 'course/:id', 
    loadComponent: () => import('./pages/course-detail/course-detail.component').then(m => m.CourseDetailComponent) 
  },
  { path: 'profile', component: StudentProfileComponent },
  { path: '**', component: NotFoundComponent }
];
`,
  'src/app/app.component.html': `
<app-header></app-header>
<main style="padding: 1rem;">
  <router-outlet></router-outlet>
</main>
`,
  'src/app/app.component.ts': `
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './components/header/header.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {}
`,
  'src/app/components/header/header.component.ts': `
import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  template: `
    <header style="padding: 1rem; background: #eee;">
      <h2>Student Course Portal</h2>
      <nav style="display: flex; gap: 1rem;">
        <a routerLink="/" routerLinkActive="active" [routerLinkActiveOptions]="{exact: true}">Home</a>
        <a routerLink="/courses" routerLinkActive="active">Courses</a>
        <a routerLink="/profile" routerLinkActive="active">Profile</a>
      </nav>
    </header>
  `
})
export class HeaderComponent {}
`,
  'src/app/pages/home/home.component.ts': `
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  standalone: true,
  template: '<h3>Welcome to Student Course Portal</h3>'
})
export class HomeComponent {}
`,
  'src/app/pages/course-list/course-list.component.ts': `
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
`,
  'src/app/components/course-card/course-card.component.ts': `
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
`,
  'src/app/directives/highlight.directive.ts': `
import { Directive, ElementRef, Renderer2, HostListener } from '@angular/core';

@Directive({
  selector: '[appHighlight]',
  standalone: true
})
export class HighlightDirective {
  constructor(private el: ElementRef, private renderer: Renderer2) {}

  @HostListener('mouseenter') onMouseEnter(): void {
    this.renderer.setStyle(this.el.nativeElement, 'backgroundColor', 'yellow');
  }

  @HostListener('mouseleave') onMouseLeave(): void {
    this.renderer.removeStyle(this.el.nativeElement, 'backgroundColor');
  }
}
`,
  'src/app/pipes/credit-label.pipe.ts': `
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'creditLabel',
  standalone: true
})
export class CreditLabelPipe implements PipeTransform {
  transform(value: number): string {
    return `${value} Credits`;
  }
}
`,
  'src/app/pages/course-detail/course-detail.component.ts': `
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
`,
  'src/app/pages/not-found/not-found.component.ts': `
import { Component } from '@angular/core';

@Component({
  selector: 'app-not-found',
  standalone: true,
  template: '<h2>404 - Page Not Found</h2>'
})
export class NotFoundComponent {}
`,
  'src/app/guards/auth.guard.ts': `
import { CanActivateFn } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  return true;
};
`,
  'src/app/guards/can-deactivate.guard.ts': `
import { CanDeactivateFn } from '@angular/router';

export interface CanComponentDeactivate {
  canDeactivate: () => boolean;
}

export const canDeactivateGuard: CanDeactivateFn<CanComponentDeactivate> = (component) => {
  return component.canDeactivate ? component.canDeactivate() : true;
};
`,
  'src/app/components/enrollment-form/enrollment-form.component.ts': `
import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';

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

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.successMessage = 'Enrollment successful!';
    }
  }

  resetForm(form: NgForm): void {
    form.resetForm();
    this.successMessage = '';
  }
}
`,
  'src/app/components/reactive-enrollment-form/reactive-enrollment-form.component.ts': `
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
`,
  'src/app/pages/student-profile/student-profile.component.ts': `
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
`,
  'src/app/services/course.service.spec.ts': `
import { TestBed } from '@angular/core/testing';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';
import { CourseService } from './course.service';
import { Course } from '../models/course';

describe('CourseService', () => {
  let service: CourseService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        CourseService,
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });
    service = TestBed.inject(CourseService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should fetch courses', () => {
    const dummyCourses: Course[] = [{ id: '1', title: 'Test', description: 'Desc', credits: 3, instructor: 'John', isAvailable: true }];
    service.getCourses().subscribe(courses => {
      expect(courses.length).toBe(1);
      expect(courses).toEqual(dummyCourses);
    });
    const req = httpMock.expectOne('http://localhost:3000/courses');
    expect(req.request.method).toBe('GET');
    req.flush(dummyCourses);
  });
});
`,
  'src/app/pages/home/home.component.spec.ts': `
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HomeComponent } from './home.component';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HomeComponent]
    }).compileComponents();
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
`
};

async function main() {
  for (const [filePath, content] of Object.entries(files)) {
    const fullPath = path.resolve(filePath);
    await fs.mkdir(path.dirname(fullPath), { recursive: true });
    await fs.writeFile(fullPath, content.trim() + 'n');
    console.log(`Generated ${filePath}`);
  }
}

main().catch(console.error);
