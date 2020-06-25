import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { ConnexionComponent } from './connexion/connexion.component';
import { AccountComponent } from './account/account.component';
import { AuthGuard } from '../_guards/auth.guard';

const routes: Routes = [
    {
        path: 'account',
        canActivate: [AuthGuard],
        component: AccountComponent
    },
    {
        path: 'connexion',
        component: ConnexionComponent
    },
    {
        path: 'register',
        component : RegisterComponent
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [RouterModule]
})
export class UserRoutingModule { }
