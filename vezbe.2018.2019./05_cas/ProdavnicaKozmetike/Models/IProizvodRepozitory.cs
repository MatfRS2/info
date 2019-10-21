using System.Linq;

namespace ProdavnicaKozmetike.Models{

    public interface IProizvodRepozitory
    {
        IQueryable<Proizvod> Proizvodi { get; }
    }

}