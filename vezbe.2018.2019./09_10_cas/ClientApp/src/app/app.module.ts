import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';

import { ModelModule } from "./models/model.module"

import { KategorijaFilterComponent } from "./structure/kategorijaFilter.component"
import { DetaljiProizvodaComponent } from "./structure/detaljiProizvoda.component"
import { KreirajNoviComponent } from "./kreirajNovi.component"
import { ProizvodComponent } from "./proizvod.component"

@NgModule({
  declarations: [AppComponent, KategorijaFilterComponent, DetaljiProizvodaComponent,
                KreirajNoviComponent, ProizvodComponent],
  imports: [BrowserModule, FormsModule, HttpModule, ModelModule,
    RouterModule.forRoot([
      { path: '', component: ProizvodComponent, pathMatch: 'full' },
      { path: 'kreiraj', component: KreirajNoviComponent },
    ])],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
