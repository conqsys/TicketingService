import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import { RequestTypeItem } from '../../request-type/shared/request-type.model'
import { ApiUrl } from '../../../config.component';
@Injectable()
export class RequestTypeService {
  constructor(private http: Http) {

  }
  public saveRequestTypeItems(requestTypeItem: RequestTypeItem): Promise<any> {
    if (requestTypeItem.id != 0) {
      return this.http
        .put(ApiUrl.baseUrl + 'RequestType/', requestTypeItem)
        .toPromise()
        .then((res) => res.json() as any)
        .catch(err => err);
    } else {
      return this.http
        .post(ApiUrl.baseUrl + 'RequestType/', requestTypeItem)
        .toPromise()
        .then((res) => res.json() as any)
        .catch(err => err);
    }
  }
  public getRequestTypeItems(): Promise<any> {
    return this.http
      .get(ApiUrl.baseUrl + 'RequestType/list')
      .toPromise()
      .then((res) => res.json())
      .catch(err => err);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error);
    return Promise.reject(error.message || error);
  }
}
