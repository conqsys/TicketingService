import { Component, OnInit, Directive, Input, Output, EventEmitter } from '@angular/core';
import { RequestTypeItem } from '../request-type/shared/request-type.model';
import { Router, ActivatedRoute } from '@angular/router';
import { RequestTypeService } from '../request-type/shared/request-type.service';
@Component({
  selector: 'app-request-type',
  templateUrl: './request-type.component.html'
})

export class RequestTypeComponent {
  private requestTypeItem: RequestTypeItem;
  private requestTypeItems: Array<RequestTypeItem> = new Array<RequestTypeItem>();
  private errormsg: string = "";
  private ErrorDiv: string = "";
  constructor(private activatedRoute: ActivatedRoute,
    private requestTypeService: RequestTypeService,
    private router: Router, ) {
  }
  ngOnInit(): void {
    this.getRequestTypeItems();
  }
  public addRequestTypeItem() {
    this.ErrorDiv = "";
    this.requestTypeItems.forEach(element => {
      if (element.id == 0) {
        if (element.name != "") {
          this.errormsg = "save Request Type";
          alert("Please save Request Type first");
          return;
        }
        this.errormsg = "Name can't blank";
        this.ErrorDiv = "red";
        return;
      } else { this.errormsg = ""; }
    })
    if (this.errormsg != "Name can't blank" && this.errormsg != "save Request Type") {
      this.requestTypeItem = new RequestTypeItem();
      this.requestTypeItems.splice(this.requestTypeItems.length, 0, this.requestTypeItem);
    }
  }

  public saveRequestTypeItems(RequestTypeItem: any) {
    if (RequestTypeItem.id == 0) {
      RequestTypeItem.enabled = true;
    }
    if (RequestTypeItem.name == "") {
      this.ErrorDiv = "red";
      return;
    }
    this.requestTypeService.saveRequestTypeItems(RequestTypeItem).then(result => {
      this.getRequestTypeItems();
    });
  }

  public getRequestTypeItems() {
    this.requestTypeService.getRequestTypeItems().then(result => {
      if (result.status != 0) {
        this.requestTypeItems = result;
        this.requestTypeItems.forEach(element => {
          element.isedit = false;
          return element;
        });
      }
    });
  }

}
