import { createAction, props } from '@ngrx/store';

export const enroll = createAction('[Enrollment] Enroll Student', props<{ data: any }>());
export const enrollSuccess = createAction('[Enrollment] Enroll Success', props<{ result: any }>());
export const enrollFailure = createAction('[Enrollment] Enroll Failure', props<{ error: any }>());
