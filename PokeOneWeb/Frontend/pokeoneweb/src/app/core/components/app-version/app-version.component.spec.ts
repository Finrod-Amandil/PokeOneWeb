import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppVersionComponent } from './app-version.component';
import { environment } from 'src/environments/environment';
import packageJson from '../../../../../package.json';

describe('AppVersionComponent', () => {
  let component: AppVersionComponent;
  let fixture: ComponentFixture<AppVersionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AppVersionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppVersionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should initialize values', () => {
    expect(component.appVersionText).toEqual(environment.stage + " " + packageJson.version);
    expect(component.containerClass).toMatch("app-version-container-" + environment.stage);
  });
});
