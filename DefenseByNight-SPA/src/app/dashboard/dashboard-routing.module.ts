import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AnonymHomePageComponent } from './anonymHomePage/anonymHomePage.component';
import { AuthGuard } from '../_guards/auth.guard';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
    {
        path: 'home',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        component: HomeComponent
    },
    {
        path: 'anonyme',
        component: AnonymHomePageComponent
    },
    {
        path: '',
        pathMatch: 'full',
        redirectTo: '/anonyme'
    }
];

@NgModule({
 imports: [RouterModule.forChild(routes)],
 exports: [RouterModule]
})
export class DashboardRoutingModule { }
