import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { AuthGuard } from '../_guards/auth.guard';

const routes: Routes = [
    {
        path: 'create',
        canActivate: [AuthGuard],
        component: CreateComponent
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [RouterModule]
})
export class CharacterRoutingModule { }
