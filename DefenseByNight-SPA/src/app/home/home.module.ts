import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeRoutingModule } from './home-routing.module';
import { NotLoggedHomeComponent } from './not-logged-home/not-logged-home.component';
import { HomeComponent } from './home.component';


@NgModule({
    imports: [
        CommonModule,
        HomeRoutingModule
    ],
    declarations: [
        NotLoggedHomeComponent,
        HomeComponent
    ]
})
export class HomeModule { }
