import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ExactLoginComponent } from './exact-login/exact-login.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
import { CustomersListComponent } from './_admin/customers/customers-list/customers-list.component';
import { ProductsListComponent } from './products/products-list/products-list.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'products', component: ProductsListComponent},
            { path: 'messages', component: MessagesComponent},
        ]
    },
    {
        path: 'admin',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'customers', component: CustomersListComponent},
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'},
];
