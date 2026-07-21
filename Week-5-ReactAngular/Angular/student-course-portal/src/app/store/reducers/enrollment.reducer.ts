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
