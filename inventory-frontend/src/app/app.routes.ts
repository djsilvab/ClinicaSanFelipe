import { Routes } from '@angular/router';

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
	  loadComponent: () => import('./features/purchase/pages/purchase-form/purchase-form.component').then(m => m.PurchaseFormComponent)
	},

	{
	  path: 'sales',
	  loadComponent: () => import('./features/sales/pages/sales-form/sales-form.component').then(m => m.SalesFormComponent)
	},

	{
	  path: 'kardex',
	  loadComponent: () => import('./features/kardex/pages/kardex-list/kardex-list.component').then(m => m.KardexListComponent)
	}
];
