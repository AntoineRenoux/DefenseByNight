import { Component, OnInit } from '@angular/core';
import { ReferencesService } from 'src/app/_services/references.service';

@Component({
  selector: 'app-anonymhomepage',
  templateUrl: './anonymHomePage.component.html',
  styleUrls: ['./anonymHomePage.component.css']
})
export class AnonymHomePageComponent implements OnInit {

  logo: any;

  constructor(private ressourceService: ReferencesService) { }

  ngOnInit() {
    this.ressourceService.getReference('LOGO').subscribe(logo => {
      this.logo = logo.value;
    });
  }

}
