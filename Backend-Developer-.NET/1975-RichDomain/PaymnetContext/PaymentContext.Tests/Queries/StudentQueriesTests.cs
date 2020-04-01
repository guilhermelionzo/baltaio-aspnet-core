using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentQueriesTests
    {
        //Red, Green, Factor

        private IList<Student> _studentsTest;
        
        public StudentQueriesTests()
        {   
            Name name;
            Document document;
            Email email;
            Student student;

            for(var i=0;i<=10;i++)
            {   
                name=new Name("Aluno","fulano");
                document=new Document("1235456"+i.ToString(),EDocumentType.CPF);
                email=new Email("guigui"+i.ToString()+"@com");
                student= new Student(name,document,email);

                _studentsTest.Add(student);
            }
        }

        [TestMethod]
        public void ShouldReturnNullWhenDocumentNotExists()
        {
            var exp = StudentQueries.GetStudentInfo("1234123545");
            var studn = _studentsTest.AsQueryable().Where(exp).FirstOrDefault(); 

            Assert.AreEqual(null,studn);
        }

        [TestMethod]
        public void ShouldReturnStudentWhenDocumentExists()
        {
            var exp = StudentQueries.GetStudentInfo("1234123545");
            var studn = _studentsTest.AsQueryable().Where(exp).FirstOrDefault(); 

            Assert.AreNotEqual(null,studn);
        }

    }
}
