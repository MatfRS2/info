import { Component } from "@angular/core"
import { Repozitorijum } from "../models/repozitorijum"

@Component({
    selector: "kategorija-filter",
    templateUrl: "kategorijaFilter.component.html"
})
export class KategorijaFilterComponent{
    constructor(private repo:Repozitorijum) {}

    setKategorija(kat: string)
    {
        this.kategorija = kat;
        this.repo.getProizvodi(true, this.kategorija);
    }

    get kategorije(): string[]
    {
        return  this.repo.kategorije;
    }
    
    private kategorija: string;
}