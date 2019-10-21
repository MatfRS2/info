import { Component, Input } from '@angular/core';
import { Repozitorijum } from "./models/repozitorijum"
import { Proizvod } from "./models/proizvod.model"

import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'kreiraj-novi',
  templateUrl: "kreirajNovi.component.html"
})
export class KreirajNoviComponent {

    constructor(private repo:Repozitorijum, private route:Router) {}

    sacuvaj(regForm:NgForm)
    {

    }
       
}