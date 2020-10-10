using System;
using Xunit;
using Prodavnica.Models;

namespace XUnitTestProdavnica
{
    public class UnitTest1
    {
        [Fact]
        public void MoguDaMenjamImeProizvodu()
        {
            //Arrange
            var p = new Proizvod { Ime = "zvucnici", Cena = 100M };

            //Act
            p.Ime = "slusalice";

            //Assert
            Assert.Equal("slusalica", p.Ime);
        }
    }
}
