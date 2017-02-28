import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';

import { routing } from './app.routing';
import { Angular2DataTableModule } from 'angular2-data-table';
import { Http, RequestOptions } from '@angular/http';
import { Router } from '@angular/router';
import { HttpInterceptor } from './shared/httpInterceptor';
import { XHRBackend } from '@angular/http';

import { DataTableModule } from 'angular2-datatable';
import { ModalModule, DialogRef } from 'angular2-modal';
import { BootstrapModalModule } from 'angular2-modal/plugins/bootstrap';

import { PaginationComponent } from './pagination/pagination.component';

import { LocalStorageService, LOCAL_STORAGE_SERVICE_CONFIG } from 'angular-2-local-storage';

import { Cookie } from 'ng2-cookies/ng2-cookies';

import { ConfirmService } from './shared/services/otherServices/confirmService';
import { DropdownModule, TabViewModule, CheckboxModule, PanelModule, PaginatorModule, MultiSelectModule, DataTableModule as PrimeDataTableModule, SharedModule, CalendarModule } from 'primeng/primeng';
/* for pagination */
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
/* for tooltip */
import { TooltipModule } from 'ng2-tooltip';
import { ToastModule } from 'ng2-toastr/ng2-toastr';
/* for drag and drop grid rows */
import { DragulaModule } from 'ng2-dragula/ng2-dragula';

/* for for popver and toolip */
import { PopoverModule } from 'ngx-popover';
/* bootstrap components start */
import { AlertModule } from 'ng2-bootstrap/ng2-bootstrap';
import { UiSwitchModule } from 'angular2-ui-switch';
/* bootstrap components end */
import { InterceptorService } from 'ng2-interceptors';
import { CustomHttp } from './interceptor/customhttp';
import { PubSubService } from './interceptor/pub-service'
/*sp-app services*/
import { MasterService } from './shared/services/master/master.service';
import { AuthService } from './shared/services/otherServices/auth.service';
/*pipes */
import { OrderByPipe, FilterPipe, CurrencyPipe, VendorFilterPipe, AccountFilterPipe, OrderByTable } from './shared/pipe/orderby';
import { Modal } from 'angular2-modal';
import { ModalComponent, CloseGuard } from 'angular2-modal';
/* sp-app components */
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { ShowOnRowHover, FocusDirective, FocusMe, CurrencyFormatterDirective } from './shared/directive/showOnRowHover';
import { LoadingSpinnerComponent } from './shared/loading-spinner/loading-spinner.component';
import { DashboardViewComponent } from './dashboard/dashboard-view/dashboard-view.component';
import { logoutComponent } from './shared/logout-modal/logout-modal.component';
import { UserService } from './user/shared/user.service';

/* for master */
import { RequestTypeComponent } from './master/request-type/request-type.component';
import { TicketStatusComponent } from './master/ticket-status/ticket-status.component';
import { RequestTypeService } from './master/request-type/shared/request-type.service';
import { TicketStatusService } from './master/ticket-status/shared/ticket-status.service';
import { ClientComponent } from './master/client/client/client.component';
import { ClientListComponent } from './master/client/client-list/client-list.component';
import { ClientService } from './master/client/shared/client.service';

/* for role entry */

/* for Reset Password */
import { ResetPasswordService } from './reset-password/shared/reset-password.service';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { confirmationModalComponent } from './shared/confirmation-modal/confirmation-modal.component';
import { DateTimePickerDirective } from './shared/directive/date-picker.directive';

let localStorageServiceConfig = {
  prefix: 'my-app',
  storageType: 'sessionStorage'
};


export function interceptorFactory(xhrBackend: XHRBackend, requestOptions: RequestOptions) {
  let service = new InterceptorService(xhrBackend, requestOptions);
  // Add interceptors here with service.addInterceptor(interceptor)
  return service;
}


@NgModule({

  declarations: [
    DateTimePickerDirective,
    OrderByPipe,
    CurrencyPipe,
    AppComponent,
    LoginComponent,
    PaginationComponent,
    OrderByPipe,
    DashboardViewComponent,
    FilterPipe,
    VendorFilterPipe,
    AccountFilterPipe,
    ShowOnRowHover,
    FocusDirective,
    FocusMe,
    CurrencyFormatterDirective,
    ResetPasswordComponent,
    LoadingSpinnerComponent,
    logoutComponent,
    confirmationModalComponent,
    OrderByTable,
    RequestTypeComponent,
    TicketStatusComponent,
    ClientComponent,
    ClientListComponent
  ],
  entryComponents: [
    logoutComponent,
    confirmationModalComponent
  ],

  imports: [
    UiSwitchModule,
    BrowserModule,
    FormsModule,
    HttpModule,
    routing,
    DragulaModule,
    Angular2DataTableModule,
    SharedModule,
    AlertModule,
    CheckboxModule,
    PanelModule,
    TabViewModule,
    DataTableModule,
    DropdownModule,
    PrimeDataTableModule,
    MultiSelectModule,
    NgbModule.forRoot(),
    ModalModule.forRoot(),
    BootstrapModalModule,
    TooltipModule,
    ToastModule,
    PopoverModule
  ],
  providers: [
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    {
      provide: InterceptorService,
      useFactory: interceptorFactory,
      deps: [XHRBackend, RequestOptions]
    },
    {
      provide: Http,
      useFactory:
      (backend: XHRBackend, defaultOptions: RequestOptions, pubsub: PubSubService) => new CustomHttp(backend, defaultOptions, pubsub),
      deps: [XHRBackend, RequestOptions, PubSubService]
    },
    PubSubService,
    MasterService,
    LocalStorageService,
    Cookie,
    AuthService,
    ConfirmService,
    ResetPasswordService,
    UserService,
    RequestTypeService,
    TicketStatusService,
    ClientService,
    {
      provide: Http,
      useFactory: (xhrBackend: XHRBackend,
        requestOptions: RequestOptions,
        router: Router,
        pubsub: PubSubService,
        localStorageService: LocalStorageService) => new HttpInterceptor(xhrBackend,
          requestOptions,
          router,
          pubsub,
          localStorageService),

      deps: [XHRBackend, RequestOptions, Router, PubSubService, LocalStorageService],
    },
    {
      provide: LOCAL_STORAGE_SERVICE_CONFIG, useValue: localStorageServiceConfig
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }








