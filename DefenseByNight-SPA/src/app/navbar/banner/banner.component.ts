import { Component, OnInit } from '@angular/core';
import { ReferencesService } from 'src/app/_services/references.service';

@Component({
  selector: 'app-banner',
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.css']
})
export class BannerComponent implements OnInit {

  public banner: any;

  constructor(private refService: ReferencesService) { }

  ngOnInit() {
    this.refService.getReference('BANNER').subscribe((res) => {
      this.banner = res.value;
    });
  }

}
