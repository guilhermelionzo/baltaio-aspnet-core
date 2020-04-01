using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable, 
    IHandler<CreateBoletoSubscriptionCommand>,
    IHandler<CreatePayPalSubscriptionCommand>
    //,IHandler<CreateCreditCardSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // Fail Fast Validation
            command.Validate();
            if(command.Invalid)
            { 
                AddNotifications(command);
                return new CommandResult(false,"Não foi possivel realizar seu cadastro");
            }
            
            //Verify whether document is already registred
            if(_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF ja esta em uso");

            //Verify whether eamil is already registred
            if(_repository.EmailExists(command.Email))
                AddNotification("Email", "Este Email ja esta em uso");

            //Generate VOs
            var name = new Name(command.FirstName,command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street,command.Number, command.Neighborhood,command.City,command.State, command.Country,command.ZipCode);
            
            //Generate Entities
            var student = new Student(name,document,email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.BarCode, command.BoletoNumber, 
                                    command.PaidDate,command.ExpireDate,command.Total,
                                    command.TotalPaid,command.Payer, 
                                    new Document(command.PaymentNumber,command.PayerDocumentType),
                                    address,email);

            //Apply Relationship
            subscription.AddPayment(payment);
            student.AddSubcription(subscription);

            //Group Validation
            AddNotifications(name,document,email,address,student,subscription,payment);

            //Save information
            _repository.CreateSubscription(student);

            //Send Welcome E-mail
            _emailService.Send(student.Name.ToString(),student.Email.Address,"Bem vindo ao guilherme.io", "Sua assinatura foi criada");

            //Return information
            return new CommandResult(true,"Assinatura realizada com sucesso.");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {            
            //Verify whether document is already registred
            if(_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF ja esta em uso");

            //Verify whether eamil is already registred
            if(_repository.EmailExists(command.Email))
                AddNotification("Email", "Este Email ja esta em uso");

            //Generate VOs
            var name = new Name(command.FirstName,command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street,command.Number, command.Neighborhood,command.City,command.State, command.Country,command.ZipCode);
            
            //Generate Entities
            var student = new Student(name,document,email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PaypalPayment(command.TransactionCode, 
                                    command.PaidDate,command.ExpireDate,command.Total,
                                    command.TotalPaid,command.Payer, 
                                    new Document(command.PaymentNumber,command.PayerDocumentType),
                                    address,email);

            //Apply Relationship
            subscription.AddPayment(payment);
            student.AddSubcription(subscription);

            //Group Validation
            AddNotifications(name,document,email,address,student,subscription,payment);

            //Save information
            _repository.CreateSubscription(student);

            //Send Welcome E-mail
            _emailService.Send(student.Name.ToString(),student.Email.Address,"Bem vindo ao guilherme.io", "Sua assinatura foi criada");

            //Return information
            return new CommandResult(true,"Assinatura realizada com sucesso.");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            //Verify whether document is already registred
            if(_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF ja esta em uso");

            //Verify whether eamil is already registred
            if(_repository.EmailExists(command.Email))
                AddNotification("Email", "Este Email ja esta em uso");

            //Generate VOs
            var name = new Name(command.FirstName,command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street,command.Number, command.Neighborhood,command.City,command.State, command.Country,command.ZipCode);
            
            //Generate Entities
            var student = new Student(name,document,email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CreditCardPayment(command.CardHolderName,command.CardNumber,command.LastTransactionNumber, 
                                    command.PaidDate,command.ExpireDate,command.Total, command.TotalPaid,command.Payer, 
                                    new Document(command.PaymentNumber,command.PayerDocumentType),
                                    address,email);

            //Apply Relationship
            subscription.AddPayment(payment);
            student.AddSubcription(subscription);

            //Group Validation
            AddNotifications(name,document,email,address,student,subscription,payment);

            //Save information
            _repository.CreateSubscription(student);

            //Send Welcome E-mail
            _emailService.Send(student.Name.ToString(),student.Email.Address,"Bem vindo ao guilherme.io", "Sua assinatura foi criada");

            //Return information
            return new CommandResult(true,"Assinatura realizada com sucesso.");
        }
    }
}