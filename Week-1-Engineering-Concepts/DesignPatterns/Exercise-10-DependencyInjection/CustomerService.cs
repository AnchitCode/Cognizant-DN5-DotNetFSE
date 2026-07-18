namespace DependencyInjectionExample
{
    public class CustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public void GetCustomer(int id)
        {
            string customer = customerRepository.FindCustomerById(id);
            System.Console.WriteLine(customer);
        }
    }
}
