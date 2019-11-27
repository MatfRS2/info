using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;
using Prodavnica.Controllers;
using Prodavnica.Models;
using System.Linq;

namespace XUnitTestProdavnica
{
    public class ProizvodControllerTests
    {
        [Fact]
        public void Moze_Podeli_na_Strane()
        {
            //Arange
            Mock<IProizvodRepozitorijum> mock = new Mock<IProizvodRepozitorijum>();

            mock.Setup(m => m.Proizvodi).Returns(new Proizvod[]
            {
                new Proizvod{ProizvodId = 1, Ime = "P1"},
                new Proizvod{ProizvodId = 2, Ime = "P2"},
                new Proizvod{ProizvodId = 3, Ime = "P3"},
                new Proizvod{ProizvodId = 4, Ime = "P4"},
                new Proizvod{ProizvodId = 5, Ime = "P5"},
            }.AsQueryable<Proizvod>());

            ProizvodController proizvodController = new ProizvodController(mock.Object);
            proizvodController.VelicinaStrane = 3;

            //Act
            IEnumerable<Proizvod> rezultat = proizvodController.Spisak(2).ViewData.Model
                as IEnumerable<Proizvod>;

            //Assert
            Proizvod[] nizProizvoda = rezultat.ToArray();
            Assert.True(nizProizvoda.Length == 2);
            Assert.Equal("P4", nizProizvoda[0].Ime);
            Assert.Equal("P5", nizProizvoda[1].Ime);

        }
    }
}
