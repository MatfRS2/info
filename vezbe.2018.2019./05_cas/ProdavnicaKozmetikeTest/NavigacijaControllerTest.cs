using System.Collections.Generic;
using System.Linq;
using Moq;
using ProdavnicaKozmetike.Models;
using ProdavnicaKozmetike.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using ProdavnicaKozmetike.Models.ViewModels;
using ProdavnicaKozmetike.Components;
using Microsoft.AspNetCore.Mvc.ViewComponents;


namespace ProdavnicaKozmetikeTest {

    public class NavigacijaControllerTest{

        [Fact]
        public void Can_Navigate_Kategory()
        {
        //Given
        Mock<IProizvodRepozitory> mock = new Mock<IProizvodRepozitory>();

        mock.Setup(m => m.Proizvodi).Returns((new Proizvod [] {
            new Proizvod {ProizvodID = 1, Ime = "Proizvod1", Kategorija = "Kat3"},
            new Proizvod {ProizvodID = 2, Ime = "Proizvod2", Kategorija = "Kat2"},
            new Proizvod {ProizvodID = 3, Ime = "Proizvod3", Kategorija = "Kat1"},
            new Proizvod {ProizvodID = 4, Ime = "Proizvod4", Kategorija = "Kat2"},
            new Proizvod {ProizvodID = 5, Ime = "Proizvod5", Kategorija = "Kat3"}}).AsQueryable<Proizvod>());

        NavigacijaViewComponent kompomenta = new NavigacijaViewComponent(mock.Object);
        
        //When
        List<string> kategorije = (((kompomenta.Invoke() as ViewViewComponentResult).ViewData.Model as
                                     IEnumerable<string>).ToList());

        
        //Then
        Assert.True(kategorije.Count == 3);
        Assert.Equal(kategorije[0], "Kat1");
        Assert.Equal(kategorije[1], "Kat2");
        Assert.Equal(kategorije[2], "Kat3");
        }

    }
}