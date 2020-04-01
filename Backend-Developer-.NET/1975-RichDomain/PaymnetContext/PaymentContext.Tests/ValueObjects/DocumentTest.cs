using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class DocumentTests
    {   
        //Red, Green, Factor
        [TestMethod]
        public void ShouldReturnErrorWhenCNPJIsInvalid()
        {   
            var doc = new Document("123", EDocumentType.CNPJ);
            Assert.IsTrue(doc.Invalid);            
        }
        
        [TestMethod]
        public void ShouldReturnSuccessWhenCNPJIsInvalid()
        {   
            var doc = new Document("34110468000150", EDocumentType.CNPJ);
            Assert.IsTrue(doc.Valid);            
            
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {   
            var doc = new Document("123", EDocumentType.CPF);
            Assert.IsTrue(doc.Invalid);           
        }
        
        [TestMethod]
        public void ShouldReturnSuccessWhenCPFIsInvalid()
        {   
            var doc = new Document("34225545806", EDocumentType.CPF);
            Assert.IsTrue(doc.Valid);               
        }
    }
}
