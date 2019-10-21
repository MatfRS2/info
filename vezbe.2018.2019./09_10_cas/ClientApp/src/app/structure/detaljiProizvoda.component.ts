import { Component } from '@angular/core';
import { Repozitorijum } from "../models/repozitorijum";
import { Proizvod } from "../models/proizvod.model";

@Component({
    selector: "proizvod-detalji",
    templateUrl: "detaljiProizvoda.component.html"
})
export class DetaljiProizvodaComponent {

    constructor(private repo: Repozitorijum) { }

    get proizvod(): Proizvod 
    {
        return this.repo.proizvod;
    }
}