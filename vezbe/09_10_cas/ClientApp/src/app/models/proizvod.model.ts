import { Proizvodjac } from "./proizvodjac.model"
import { Ocena } from "./ocena.model";

export class Proizvod {
    constructor(
        public proizvodID?: number,
        public ime?: string,
        public kategorija?: string,
        public opis?: string,
        public cena?: number,
        public proizvodjac?: Proizvodjac,
        public ocene?: Ocena[] ) { }
}