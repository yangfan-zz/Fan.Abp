import { ModuleWithProviders, NgModule } from '@angular/core';
import { DOMAIN_CQRS_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class DomainCqrsConfigModule {
  static forRoot(): ModuleWithProviders<DomainCqrsConfigModule> {
    return {
      ngModule: DomainCqrsConfigModule,
      providers: [DOMAIN_CQRS_ROUTE_PROVIDERS],
    };
  }
}
