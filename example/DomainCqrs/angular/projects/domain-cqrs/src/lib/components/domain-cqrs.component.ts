import { Component, OnInit } from '@angular/core';
import { DomainCqrsService } from '../services/domain-cqrs.service';

@Component({
  selector: 'lib-domain-cqrs',
  template: ` <p>domain-cqrs works!</p> `,
  styles: [],
})
export class DomainCqrsComponent implements OnInit {
  constructor(private service: DomainCqrsService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
