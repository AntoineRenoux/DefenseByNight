import { NgModule } from '@angular/core';
import { AnonymHomePageComponent } from './anonymHomePage/anonymHomePage.component';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';

@NgModule({
    imports: [
        CommonModule,
        DashboardRoutingModule
    ],
    declarations: [
        AnonymHomePageComponent,
        HomeComponent
    ]
})
export class DashboardModule { }