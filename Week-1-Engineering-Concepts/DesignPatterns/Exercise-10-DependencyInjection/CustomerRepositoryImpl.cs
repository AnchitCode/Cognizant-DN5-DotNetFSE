namespace DependencyInjectionExample
{
    public class CustomerRepositoryImpl : ICustomerRepository
    {
        public string FindCustomerById(int id)
        {
            return $"Customer with ID {id}: John Doe";
        }
    }
}
