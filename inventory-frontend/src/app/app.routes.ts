import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
    {
	  path: '',
	  redirectTo: 'login',
	  pathMatch: 'full'
	},

	{
	  path: 'login',
	  loadComponent: () => import('./features/auth/pages/login/login.component').then(m => m.LoginComponent)
	},

	{
	  path: 'purchase',
		canActivate: [authGuard],
	  loadComponent: () => import('./features/purchase/pages/purchase-form/purchase-form.component').then(m => m.PurchaseFormComponent)
	},

	{
	  path: 'sales',
	  canActivate: [authGuard],
	  loadComponent: () => import('./features/sales/pages/sales-form/sales-form.component').then(m => m.SalesFormComponent)
	},

	{
	  path: 'kardex',
	  canActivate: [authGuard],
	  loadComponent: () => import('./features/kardex/pages/kardex-list/kardex-list.component').then(m => m.KardexListComponent)
	}
];
