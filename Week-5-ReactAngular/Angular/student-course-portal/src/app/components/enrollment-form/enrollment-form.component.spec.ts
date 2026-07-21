import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EnrollmentFormComponent } from './enrollment-form.component';
import { provideMockStore, MockStore } from '@ngrx/store/testing';
import { FormsModule } from '@angular/forms';
import { enroll } from '../../store/actions/enrollment.actions';

describe('EnrollmentFormComponent', () => {
  let component: EnrollmentFormComponent;
  let fixture: ComponentFixture<EnrollmentFormComponent>;
  let store: MockStore;
  const initialState = { loading: false, error: null, success: false };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EnrollmentFormComponent, FormsModule],
      providers: [provideMockStore({ initialState })]
    }).compileComponents();
    
    store = TestBed.inject(MockStore);
    fixture = TestBed.createComponent(EnrollmentFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should dispatch enroll action on valid submit', () => {
    vi.spyOn(store, 'dispatch');
    component.form.setValue({ name: 'Test User' });
    component.onSubmit(component.form);
    expect(store.dispatch).toHaveBeenCalledWith(enroll({ data: { name: 'Test User' } }));
  });
});
