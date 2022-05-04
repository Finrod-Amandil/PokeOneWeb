import { Component, OnInit, Output } from '@angular/core';
import { environment } from 'src/environments/environment';
import packageJson from '../../../../../package.json';

@Component({
  selector: 'pokeone-app-version',
  templateUrl: './app-version.component.html',
  styleUrls: ['./app-version.component.scss']
})
export class AppVersionComponent implements OnInit {
  @Output() appVersionText = "";
  containerClass = "app-version-container";

  constructor() { 
    this.appVersionText = environment.stage + " " + packageJson.version;
  }

  ngOnInit(): void {
    this.containerClass += "-" + environment.stage;   
  }

}
