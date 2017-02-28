import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import { TicketStatusItem } from '../../ticket-status/shared/ticket-status.model'
import { ApiUrl } from '../../../config.component';
@Injectable()
export class TicketStatusService {
  constructor(private http: Http) {

  }
  public saveTicketStatus(ticketStatusItem: TicketStatusItem): Promise<any> {
    if (ticketStatusItem.id != 0) {
      return this.http
        .put(ApiUrl.baseUrl + 'TicketStatus/', ticketStatusItem)
        .toPromise()
        .then((res) => res.json() as any)
        .catch(err => err);
    } else {
      return this.http
        .post(ApiUrl.baseUrl + 'TicketStatus/', ticketStatusItem)
        .toPromise()
        .then((res) => res.json() as any)
        .catch(err => err);
    }
  }
  public getTicketStatus(): Promise<any> {
    return this.http
      .get(ApiUrl.baseUrl + 'TicketStatus/list')
      .toPromise()
      .then((res) => res.json())
      .catch(err => err);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error);
    return Promise.reject(error.message || error);
  }
}
