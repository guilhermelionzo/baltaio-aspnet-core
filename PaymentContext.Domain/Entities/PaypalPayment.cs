using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class PaypalPayment: Payment
    {
        public PaypalPayment(string lastTransactionNumber, DateTime paidDate, DateTime expireDate, decimal total, 
                            decimal totalPaid, string payer, Document document, Address address, Email email)
                            :base(paidDate, expireDate, total, totalPaid, payer, document, address, email)
        {
            LastTransactionNumber = lastTransactionNumber;
        }

        public string LastTransactionNumber {get; private set;}
    }
}