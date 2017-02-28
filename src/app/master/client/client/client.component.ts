import { Component, OnInit, Directive, Input, Output, EventEmitter } from '@angular/core';
import { ClientItem } from '../../client/shared/client.model';
import { Router, ActivatedRoute } from '@angular/router';
import { ClientService } from '../../client/shared/client.service';
@Component({
  selector: 'app-client',
  templateUrl: './client.component.html'
})

export class ClientComponent {
  private clientItem: ClientItem = new ClientItem();
  // private clientItems: Array<ClientItem> = new Array<ClientItem>();
  private errormsg: string = "";
  private btnText: string = "";
  private ClientId: number = 0;
  constructor(private activatedRoute: ActivatedRoute,
    private clientService: ClientService,
    private router: Router) {
  }
  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.ClientId = +params['ClientId']; // (+) converts string 'id' to a number
    });
    if (this.ClientId && this.ClientId !== 0) {
      this.getClientByID(this.ClientId);
      this.btnText = 'Update Client';
    } else {
      this.btnText = 'Save Client';
    }
    // this.getClients();
  }
  // private validateEmail(email: string) {
  //   var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

  //   return re.test(email);
  // }


  private saveClient() {
    this.clientItem.clientAcronym = this.clientItem.clientAcronym.toUpperCase();
    // if (this.clientItem.loginEnabled == false) {
    //   this.clientItem.userEmail = "";
    //   this.clientItem.userName = "";
    // }
    if (this.clientItem.clientName == '') {
      alert("'Please fill Client Name'");
      return;
    }
    if (this.clientItem.clientAcronym == '') {
      alert("Please fill Client Acronym");
      return;
    }
    if (this.clientItem.description == '') {
      alert("Please fill Description");
      return;
    }
    // if (this.clientItem.loginEnabled && this.clientItem.userName == '') {
    //   alert("Please fill User Name");
    //   return;
    // }
    // if (this.clientItem.loginEnabled) {
    //   if (this.validateEmail(this.clientItem.userEmail)) {
    //   } else {
    //     alert('Please enter correct email.');
    //     return;
    //   }
    // }
    if (this.clientItem.id == 0) {
      this.clientItem.enabled = true;
    }
    this.clientService.saveClientItem(this.clientItem).then(result => {
      if (result) {
        this.router.navigate(['/clients']);
      }
    });
  }
  private getClientByID(ClientId: number): void {
    this.clientService
      .getClientByID(ClientId)
      .then(result => {
        this.clientItem = result;
      });
  }

  Cancel() {
    this.router.navigate(['/clients']);
  }

}
