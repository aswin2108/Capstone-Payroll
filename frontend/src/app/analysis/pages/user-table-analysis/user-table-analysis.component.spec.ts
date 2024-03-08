import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserTableAnalysisComponent } from './user-table-analysis.component';

describe('UserTableAnalysisComponent', () => {
  let component: UserTableAnalysisComponent;
  let fixture: ComponentFixture<UserTableAnalysisComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UserTableAnalysisComponent]
    });
    fixture = TestBed.createComponent(UserTableAnalysisComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
