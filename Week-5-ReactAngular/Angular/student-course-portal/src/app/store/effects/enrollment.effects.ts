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
