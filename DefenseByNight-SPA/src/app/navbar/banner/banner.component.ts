import { Component, OnInit } from '@angular/core';
import { ReferencesService } from 'src/app/_services/references.service';

export function showBanner() {
  this.visible = true;
}

export function hideBanner() {
  this.visible = false;
}

@Component({
  selector: 'app-banner',
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.css']
})
export class BannerComponent implements OnInit {

  public banner: any;
  public visible = true;

  constructor(private refService: ReferencesService) { }

  ngOnInit() {
    this.refService.getBanner().subscribe((res) => {
      this.banner = res.value;
    });
  }

}
