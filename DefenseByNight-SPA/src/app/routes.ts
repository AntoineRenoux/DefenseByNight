import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './_guards/auth.guard';

import { HomeComponent } from './home/home.component';
import { CharacterComponent } from './character/character.component';
import { RulesComponent } from './rules/rules.component';
import { RegisterComponent } from './nav/register/register.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: 'register', component: RegisterComponent,
        //loadChildren: () => import('./analysis/analysis.module').then(m => m.AnalysisModule)
    },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'character', component: CharacterComponent },
            { path: 'rules', component: RulesComponent },
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
