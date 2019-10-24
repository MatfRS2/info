import { Proizvod } from "./proizvod.model";

export class Proizvodjac {
    constructor(
        public proizvodjacId?: number,
        public ime?: string,
        public grad?: string,
        public drzava?: string,
        public proizvodi?: Proizvod[] ) { }
}