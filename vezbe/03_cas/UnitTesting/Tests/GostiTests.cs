using System;
using Xunit;
using primer4.Models;
using primer4.Controllers;
using Moq;
﻿using Microsoft.AspNetCore.Mvc;

namespace Tests{

    public class  GostiTests{

        [Fact]
        public void MozeDaPromeniIme()
        {
        //Given
        var p = new Gosti{ Ime = "Danijela", Email = "daca@matf.rs"};
        
        //When
        p.Ime = "Daca";
        
        //Then
        Assert.Equal(p.Ime, "Daca");
        }


        [Fact]
        public void MozeDaPromeniTelefon()
        {
        //Given
        var p = new Gosti{ Ime = "Danijela", Telefon = "066"};
        
        //When
        p.Telefon = "067";
        
        //Then
        Assert.Equal(p.Telefon, "067");
        }


        [Fact]
        public void SpisakGostijuJednomPristupaRepozitorijumu()
        {
        //Given
        var mock = new Mock<IRepozitorijum>();

        mock.SetupGet( m => m.svi_gosti).Returns(new [] {new Gosti {Ime = "Daca", Email = "daca@matf.bg.ac.rs"}} );

        var controller = new HomeController(mock.Object);
        
        //When
        var rezultat = controller.SpisakGostiju() as ViewResult;
        
        //Then
        mock.VerifyGet(m => m.svi_gosti, Times.Once);
        }

    }

}