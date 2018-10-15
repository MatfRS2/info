using System.Collections.Generic;
using System.Linq;
using Moq;
using ProdavnicaKozmetike.Models;
using ProdavnicaKozmetike.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using ProdavnicaKozmetike.Models.ViewModels;


namespace ProdavnicaKozmetikeTest {

    
    public class ProizvodControllerTest{

        /* ovaj test se radi nakon koraka 23
        [Fact]
        public void Can_Paginate()
        {
        //Given
        Mock<IProizvodRepozitory> mock = new Mock<IProizvodRepozitory>();
        
        mock.Setup(m => m.Proizvodi).Returns(( new Proizvod [] {
            new Proizvod {ProizvodID = 1, Ime = "Proizvod1"},
            new Proizvod {ProizvodID = 2, Ime = "Proizvod2"},
            new Proizvod {ProizvodID = 3, Ime = "Proizvod3"},
            new Proizvod {ProizvodID = 4, Ime = "Proizvod4"},
            new Proizvod {ProizvodID = 5, Ime = "Proizvod5"}}).AsQueryable<Proizvod>());

        ProizvodController controller = new ProizvodController(mock.Object);
        controller.VelicinaStrane = 3;

        //When
        IEnumerable<Proizvod> rezultat = 
            controller.SpisakProizvoda(2).ViewData.Model as IEnumerable<Proizvod>;
        
        //Then
        Proizvod[] prod = rezultat.ToArray();
        Assert.True(prod.Length == 2);
        Assert.Equal(prod[0].Ime, "Proizvod4");
        Assert.Equal(prod[1].Ime, "Proizvod5");
        }
        */


        [Fact]
        public void Can_Pagnate()
        {
        //Given
        Mock<IProizvodRepozitory> mock = new Mock<IProizvodRepozitory>();
        
        mock.Setup(m => m.Proizvodi).Returns(( new Proizvod [] {
            new Proizvod {ProizvodID = 1, Ime = "Proizvod1"},
            new Proizvod {ProizvodID = 2, Ime = "Proizvod2"},
            new Proizvod {ProizvodID = 3, Ime = "Proizvod3"},
            new Proizvod {ProizvodID = 4, Ime = "Proizvod4"},
            new Proizvod {ProizvodID = 5, Ime = "Proizvod5"}}).AsQueryable<Proizvod>());

        ProizvodController controller = new ProizvodController(mock.Object);
        controller.VelicinaStrane = 3;
        
        //When
        SpisakProizvodaViewModel rezultat = 
            controller.SpisakProizvoda(2).ViewData.Model as SpisakProizvodaViewModel;
        
        //Then
        Proizvod[] prod = rezultat.Proizvodi.ToArray();
        Assert.True(prod.Length == 2);
        Assert.Equal(prod[0].Ime, "Proizvod4");
        Assert.Equal(prod[1].Ime, "Proizvod5");
        }

    }
    



}