
import { Proizvod } from "./proizvod.model";


export class Ocena {
    constructor(
        public ocenaId?: number,
        public vrednost?: number,
        public proizvod?: Proizvod) { }
}