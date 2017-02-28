import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { DashboardViewComponent } from './dashboard/dashboard-view/dashboard-view.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';

import { RequestTypeComponent } from './master/request-type/request-type.component';
import { TicketStatusComponent } from './master/ticket-status/ticket-status.component';
import { ClientComponent } from './master/client/client/client.component';
import { ClientListComponent } from './master/client/client-list/client-list.component';

const APP_ROUTES: Routes = [

  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },

  // { path: 'attachment', component: AttachmentComponent },
  { path: 'dashboard', component: DashboardViewComponent },
  // routing for reset password
  { path: 'requestType', component: RequestTypeComponent },
  { path: 'ticketStatus', component: TicketStatusComponent },
  { path: 'client/:ClientId', component: ClientComponent },
  { path: 'clients', component: ClientListComponent },

];

export const routing = RouterModule.forRoot(APP_ROUTES);
