import { Component } from '@angular/core';
import packageJson from '../../../../package.json';
import { environment } from '../../../environments/environment';

@Component({
    selector: 'pokeoneweb-app-version',
    imports: [],
    templateUrl: './app-version.html',
    styleUrl: './app-version.scss',
})
export class AppVersionComponent {
    appVersionText =
        environment.name !== 'local'
            ? `${environment.name} ${packageJson.version}`
            : environment.name;
    containerClass = `app-version-container-${environment.name}`;
}
