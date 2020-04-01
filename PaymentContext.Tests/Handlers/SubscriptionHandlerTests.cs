using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        //Red, Green, Factor
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());

            var command = new CreateBoletoSubscriptionCommand();
            command.BarCode = "123456789";

            command.FirstName = "Bruce";
            command.LastName = "Wayne";
            command.Document = "99999999999";
            command.Email = "hello@guilherme.com2";
            command.BoletoNumber = "123456789";
            command.BarCode = "123465987";
            command.PaymentNumber = "1233123";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Payer = "Wayne Corp";
            command.PayerDocument = "121123";
            command.PayerDocumentType = EDocumentType.CNPJ;
            command.PayerEmail = "batman@dc.com";
            command.Street = "asd";
            command.Number = "asd";
            command.Neighborhood = "asd";
            command.City = "asd";
            command.State = "asd";
            command.Country = "asdad";
            command.ZipCode = "asd";

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }

    }
}
