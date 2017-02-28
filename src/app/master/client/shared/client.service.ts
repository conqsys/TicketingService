import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import { ClientItem } from '../../client/shared/client.model';
import { ApiUrl } from '../../../config.component';
@Injectable()
export class ClientService {
  constructor(private http: Http) {

  }
  public saveClientItem(clientItem: ClientItem): Promise<any> {
    if (clientItem.id != 0) {
      return this.http
        .put(ApiUrl.baseUrl + 'Client/', clientItem)
        .toPromise()
        .then((res) => res.json() as any)
        .catch(err => err);
    } else {
      return this.http
        .post(ApiUrl.baseUrl + 'Client/', clientItem)
        .toPromise()
        .then((res) => res.json() as any)
        .catch(err => err);
    }
  }
  public getClientItems(): Promise<ClientItem[]> {
    return this.http
      .get(ApiUrl.baseUrl + 'Client/list')
      .toPromise()
      .then((res) => res.json() as ClientItem[])
      .catch(err => err);
  }
  public getClientByID(ClientId: number): Promise<ClientItem> {
    return this
      .http
      .get(ApiUrl.baseUrl + 'Client/' + ClientId)
      .toPromise()
      .then(response => response.json() as ClientItem)
      .catch(err => err);
  }

  // private handleError(error: any): Promise<any> {
  //   console.error('An error occurred', error);
  //   return Promise.reject(error.message || error);
  // }
}
