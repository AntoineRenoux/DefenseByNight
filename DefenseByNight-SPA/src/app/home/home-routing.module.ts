import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from '../_guards/auth.guard';
import { HomeComponent } from './home.component';
import { NotLoggedHomeComponent } from './not-logged-home/not-logged-home.component';

const routes: Routes = [
    {
        path: 'anonymous',
        component: NotLoggedHomeComponent
    },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        component: HomeComponent
    }
];

@NgModule({
 imports: [RouterModule.forChild(routes)],
 exports: [RouterModule]
})
export class HomeRoutingModule { }
