import { Component } from '@angular/core';
import { Repozitorijum } from "./models/repozitorijum"
import { Proizvod } from "./models/proizvod.model"

@Component({
  selector: 'proizvod',
  templateUrl: './proizvod.component.html',
  styleUrls: ['./app.component.css']
})
export class ProizvodComponent {

  constructor(private repo:Repozitorijum) {}

  get proizvod(): Proizvod {
    return this.repo.proizvod;
  }

  get proizvodi(): Proizvod[]
  {
    return this.repo.proizvodi;
  }

  selectProizvod(id: number)
  {
    this.repo.getProizvod(id);
  }
}