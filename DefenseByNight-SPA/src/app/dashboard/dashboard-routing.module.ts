import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AnonymHomePageComponent } from './anonymHomePage/anonymHomePage.component';

const routes: Routes = [
    {
        path: '',
        component: AnonymHomePageComponent
    }
];

@NgModule({
 imports: [RouterModule.forChild(routes)],
 exports: [RouterModule]
})
export class DashboardRoutingModule { }