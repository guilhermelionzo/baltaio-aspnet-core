using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Repositories
{
    //Realiza a abstração entre o projeto e o repositorio
    public interface IStudentRepository
    {
        bool DocumentExists(string document);
        bool EmailExists(string email);
        void CreateSubscription(Student student);
    }
}