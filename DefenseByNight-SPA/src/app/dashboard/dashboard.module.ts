import { NgModule } from '@angular/core';
import { AnonymHomePageComponent } from './anonymHomePage/anonymHomePage.component';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { CommonModule } from '@angular/common';

@NgModule({
    imports: [
        CommonModule,
        DashboardRoutingModule
    ],
    declarations: [
        AnonymHomePageComponent
    ]
})
export class DashboardModule { }