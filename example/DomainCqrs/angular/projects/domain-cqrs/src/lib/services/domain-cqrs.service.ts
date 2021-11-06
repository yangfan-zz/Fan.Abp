import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class DomainCqrsService {
  apiName = 'DomainCqrs';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/DomainCqrs/sample' },
      { apiName: this.apiName }
    );
  }
}
