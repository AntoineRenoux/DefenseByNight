import { Component, OnInit } from '@angular/core';
import { ReferencesService } from 'src/app/_services/references.service';

@Component({
  selector: 'app-not-logged-home',
  templateUrl: './not-logged-home.component.html',
  styleUrls: ['./not-logged-home.component.css']
})
export class NotLoggedHomeComponent implements OnInit {

  logo: any;

  constructor(private ressourceService: ReferencesService) { }

  ngOnInit() {
    this.ressourceService.getReference('LOGO').subscribe(logo => {
      this.logo = logo.value;
    });
  }

}
