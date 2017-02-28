import { Component, OnInit, Directive, Input, Output, EventEmitter } from '@angular/core';
import { TicketStatusItem } from '../ticket-status/shared/ticket-status.model';
import { TicketStatusService } from '../ticket-status/shared/ticket-status.service';
import { Router, ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-ticket-status',
  templateUrl: './ticket-status.component.html'
})

export class TicketStatusComponent {
  // private ticketStatusItems: Array< TicketStatusItem>;
  private ticketStatusItem: TicketStatusItem;
  private ticketStatusItems: Array<TicketStatusItem> = new Array<TicketStatusItem>();
  private errormsg: string = "";
  private ErrorDiv: string = "";
  constructor(private activatedRoute: ActivatedRoute,
    private ticketStatusService: TicketStatusService,
    private router: Router) {
  }
  ngOnInit(): void {
    this.getTicketStatus();
  }
  addTicketStatusItem(): void {
    this.ErrorDiv = "";
    this.ticketStatusItems.forEach(element => {
      if (element.id == 0) {
        if (element.name != "") {
          this.errormsg = "save Ticket Status";
          alert("Please save Ticket Status first");
          return;
        }
        this.errormsg = "Name can't be blank"
        this.ErrorDiv = "red";
        return false;
      } else { this.errormsg = ""; }
    })
    if (this.errormsg != "Name can't be blank" && this.errormsg != "save Ticket Status") {
      this.ticketStatusItem = new TicketStatusItem();
      this.ticketStatusItems.splice(this.ticketStatusItems.length, 0, this.ticketStatusItem);
    }
  }
  // saveTicketStatus() {
  //   this.ticketStatusItems;
  // }
  private saveTicketStatus(TicketStatusItem: any) {
    if (TicketStatusItem.id == 0) {
      TicketStatusItem.enabled = true;
    }
    if (TicketStatusItem.name == "") {
     this.ErrorDiv = "red";
      return;
    }
    this.ticketStatusService.saveTicketStatus(TicketStatusItem).then(result => {
      this.getTicketStatus();
    });
  }
  private getTicketStatus() {
    this.ticketStatusService.getTicketStatus().then(result => {
      if (result) {
        this.ticketStatusItems = result;
        this.ticketStatusItems.forEach(element => {
          element.isedit = false;
          return element;
        });
      }
    });
  }


}
