import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RootComponentComponent } from './root-component.component';

describe('RootComponentComponent', () => {
  let component: RootComponentComponent;
  let fixture: ComponentFixture<RootComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RootComponentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RootComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
