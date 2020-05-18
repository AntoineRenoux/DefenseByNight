import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { ConnexionComponent } from './connexion/connexion.component';

const routes: Routes = [
    {
        path: 'register',
        component : RegisterComponent
    },
    {
        path: 'connexion',
        component: ConnexionComponent
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [RouterModule]
})
export class UserRoutingModule { }
