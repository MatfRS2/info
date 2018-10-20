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


namespace ProdavnicaKozmetikeTest{

    public class KorpaTests {

        [Fact]
        public void Ispravno_Dodaje_Elemente()
        {
        //Given
            Proizvod p1 = new Proizvod {ProizvodID = 1, Ime = "P1"};
            Proizvod p2 = new Proizvod {ProizvodID = 2, Ime = "P2"};

            Korpa korpa = new Korpa();
        
        //When
            korpa.DodajProizvod(p1, 1);
            korpa.DodajProizvod(p2, 2);
            KorpaElement[] elementi = korpa.listaProizvodaUKorpi.ToArray();

        
        //Then
            Assert.Equal(elementi.Length, 2);
            Assert.Equal(elementi[0].Proizvod.Ime, "P1");
            Assert.Equal(p2, elementi[1].Proizvod);
        }


        [Fact]
        public void Ispravno_Povecava_Kolicinu()
        {
        //Given
            Proizvod p1 = new Proizvod{ ProizvodID = 1, Ime = "P1" };
            Proizvod p2 = new Proizvod{ ProizvodID = 2, Ime = "P2" };

            Korpa korpa = new Korpa();
        
        //When
            korpa.DodajProizvod(p1, 1);
            korpa.DodajProizvod(p2, 2);
            korpa.DodajProizvod(p1, 10);
            KorpaElement[] element = korpa.listaProizvodaUKorpi.ToArray();
        
        //Then
            Assert.True(element.Length == 2);
            Assert.Equal(element[0].Kolicina, 11);
            Assert.Equal(element[1].Kolicina, 2);
        }


        [Fact]
        public void Moze_Da_Obrise_Proizvod()
        {
        //Given
            Proizvod p1 = new Proizvod{ ProizvodID = 1, Ime = "P1" };
            Proizvod p2 = new Proizvod{ ProizvodID = 2, Ime = "P2" };

            Korpa korpa = new Korpa();
        
        //When
            korpa.DodajProizvod(p1, 1);
            korpa.DodajProizvod(p2, 2);
            korpa.ObrisiProizvod(p1);
            KorpaElement[] element = korpa.listaProizvodaUKorpi.ToArray();
        
        //Then
            Assert.True(element.Length == 1);
            Assert.Equal(element[0].Proizvod, p2);
        }


        [Fact]
        public void Brise_Celu_Korpu()
        {
        //Given
            Proizvod p1 = new Proizvod{ ProizvodID = 1, Ime = "P1" };
            Proizvod p2 = new Proizvod{ ProizvodID = 2, Ime = "P2" };

            Korpa korpa = new Korpa();
        
        //When
            korpa.DodajProizvod(p1, 1);
            korpa.DodajProizvod(p2, 2);
            korpa.ObrisiKorpu();
            KorpaElement[] element = korpa.listaProizvodaUKorpi.ToArray();
        
        
        //Then
            Assert.True(element.Length == 0);
        }


    }

}