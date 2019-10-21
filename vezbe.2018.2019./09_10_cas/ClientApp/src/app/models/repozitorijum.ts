import { Proizvod } from "./proizvod.model";
import { Injectable } from "@angular/core";
import { Http, RequestMethod, Request, Response } from "@angular/http";
import { Observable } from "rxjs/Observable"
import "rxjs/add/operator/map"

const proizvodUrl = "/api/proizvodi"
const kategorijeUrl = "/api/proizvodi/kategorije"

@Injectable()
export class Repozitorijum {
    constructor(private http: Http)
    {
        this.getProizvodi(true);
        this.getKategorije();
    }

    getProizvod(id : number)
    {
        this.sendRequest(RequestMethod.Get, proizvodUrl + "/" + id)
            .subscribe(response =>  this.proizvodData = response);
    }

    getProizvodi(related = false, kat:string = null)
    {
        let url = proizvodUrl + "?related=" + related;

        if (kat)
        {
            url += "&kategorija=" + kat;
        }
        
        this.sendRequest(RequestMethod.Get, url)
            .subscribe(response => this.proizvodiData = response);
    }

    private sendRequest(verb: RequestMethod, url: string, data?: any)
        : Observable<any>
    {
        return this.http.request(
            new Request({
                method: verb,
                url: url,
                body: data
            })).map(response => response.json());
    }

    get proizvod() : Proizvod
    {
        return this.proizvodData;
    }

    get proizvodi() : Proizvod[]
    {
        return this.proizvodiData;
    }

    getKategorije()
    {
        this.sendRequest(RequestMethod.Get, kategorijeUrl)
            .subscribe(response => this.kategorijeData = response);
    }

    get kategorije() : string[]
    {
        return this.kategorijeData;
    }

    saveProizvod(proizvod) {  
        this.sendRequest(RequestMethod.Post, proizvodUrl, proizvod);
    } 
    
    private proizvodData: Proizvod;
    private proizvodiData: Proizvod[];
    private kategorijeData: string[];
}

