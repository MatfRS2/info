using System.Linq;

namespace ProdavnicaKozmetike.Models{

    public interface IProizvodRepozitory
    {
        IQueryable<Proizvod> Proizvodi { get; }

        void SacuvajProizvod(Proizvod proizvod);

        Proizvod BrisiProizvod(int proizvodID);
    }

}