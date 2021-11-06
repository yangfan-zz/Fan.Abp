import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { DomainCqrsComponent } from './components/domain-cqrs.component';
import { DomainCqrsRoutingModule } from './domain-cqrs-routing.module';

@NgModule({
  declarations: [DomainCqrsComponent],
  imports: [CoreModule, ThemeSharedModule, DomainCqrsRoutingModule],
  exports: [DomainCqrsComponent],
})
export class DomainCqrsModule {
  static forChild(): ModuleWithProviders<DomainCqrsModule> {
    return {
      ngModule: DomainCqrsModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<DomainCqrsModule> {
    return new LazyModuleFactory(DomainCqrsModule.forChild());
  }
}
