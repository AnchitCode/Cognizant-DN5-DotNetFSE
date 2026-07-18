using System;

namespace DependencyInjectionExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Repository
            ICustomerRepository repository = new CustomerRepositoryImpl();

            // Inject Repository into Service
            CustomerService customerService = new CustomerService(repository);

            // Use the Service
            customerService.GetCustomer(101);
        }
    }
}
