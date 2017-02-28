import { Component, OnInit, Directive, Input, Output, EventEmitter } from '@angular/core';
import { ClientItem } from '../../client/shared/client.model';
import { Router, ActivatedRoute } from '@angular/router';
import { ClientService } from '../../client/shared/client.service';
@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html'
})

export class ClientListComponent {
  private clientItem: ClientItem;
  private clientItems: Array<ClientItem> = new Array<ClientItem>();
  private errormsg: string = "";
  constructor(private activatedRoute: ActivatedRoute,
    private clientService: ClientService,
    private router: Router) {
  }
  ngOnInit(): void {
    this.getClients();
  }
  editClient(ClientId: number): void {
    this.router.navigate(['/client', ClientId]);
  }
  addClientItem(): void {
    this.router.navigate(['/client', 0]);
  }

  // private saveClientItem(clientItem: any) {
  //   if (clientItem.name == "") {
  //     alert("Request Type Name can't be blank")
  //     return;
  //   }
  //   this.clientService.saveClientItems(clientItem).then(result => {
  //     this.getClients();
  //   });
  // }

  private getClients() {
    this.clientService.getClientItems().then(result => {
      if (result) {
        this.clientItems = result;
      }
    });
  }

}
