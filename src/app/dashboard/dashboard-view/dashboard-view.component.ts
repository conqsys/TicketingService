import { Component, OnInit, } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';
import { BaseComponent } from '../../base.component';
import { LocalStorageService } from 'angular-2-local-storage';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { ToastsManager } from 'ng2-toastr/ng2-toastr';
import { Interceptor, InterceptedRequest, InterceptedResponse } from 'ng2-interceptors';
import { PubSubService } from '../../interceptor/pub-service';
import { LoadingSpinnerComponent } from '../../shared/loading-spinner/loading-spinner.component';
import { UserService } from '../../user/shared/user.service';
import { MasterService } from '../../shared/services/master/master.service';
@Component({
  selector: 'sp-dashboard',
  templateUrl: './dashboard-view.component.html',
})


export class DashboardViewComponent extends BaseComponent implements OnInit, Interceptor {

  private companyId: number = 0;
  private invoiceState: number = 1;
  private totalItems: number = 0;
  private invoiceForValidApprovalCount = 0;
  private showLoader: boolean;
  constructor(
    private http: Http,
    private userService: UserService,
    private masterService: MasterService,
    localStorageService: LocalStorageService,
    router: Router,
    private activatedRoute: ActivatedRoute,
    private location: Location,
    public toastr: ToastsManager,
    public pubsub: PubSubService
  ) {
    super(localStorageService, router);
    this.companyId = 0;
    this.invoiceState = 1;
    this.totalItems = 0;
    this.invoiceForValidApprovalCount = 0;
    // this.getSessionDetails();

  }
  ngOnInit() {
    this.pubsub.beforeRequest.subscribe(data => this.showLoader = true);
    this.pubsub.afterRequest.subscribe(data => this.showLoader = false);
  }

  // private getSessionDetails(): void {
  //   this.userService
  //     .getUserPermissions().then((result) => {
  //       if (result) {

  //         this.sessionDetails = this.userService.getSessionDetails();
  //         if (this.sessionDetails.userId != null) {


  //         } else {
  //           let link = ['/login'];
  //           this.router.navigate(link);
  //         }
  //       }
  //     });
  // }



}