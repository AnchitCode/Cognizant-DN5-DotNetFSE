import { createFeatureSelector, createSelector } from '@ngrx/store';
import { EnrollmentState } from '../reducers/enrollment.reducer';

export const selectEnrollmentState = createFeatureSelector<EnrollmentState>('enrollment');
export const selectEnrollmentLoading = createSelector(selectEnrollmentState, state => state.loading);
export const selectEnrollmentError = createSelector(selectEnrollmentState, state => state.error);
export const selectEnrollmentSuccess = createSelector(selectEnrollmentState, state => state.success);
