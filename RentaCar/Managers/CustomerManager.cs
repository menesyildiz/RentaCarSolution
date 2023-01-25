using RentaCar.Entities;
using RentaCar.Models;

namespace RentaCar.Managers
{
    public class CustomerManager
    {
        private DatabaseContext _databaseContext;
        public CustomerManager(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<Customer> List()
        {
            return _databaseContext.Customers.ToList();
        }

        public void Create(NewCustomerModel modelim)
        {
            Customer customer = new Customer();
            customer.IdNumber = modelim.IdNumber;
            customer.Name = modelim.Name;
            customer.Surname = modelim.Surname;
            customer.Birthday = modelim.Birthday;
            customer.PhoneNumber = modelim.PhoneNumber;
            customer.Email = modelim.Email;


            _databaseContext.Customers.Add(customer);
            _databaseContext.SaveChanges();
        }

        public Customer GetById(int customerId)
        {
            return _databaseContext.Customers.Find(customerId);
        }

        public void Update(Customer customer, EditCustomerViewModel modelim)
        {
            customer.IdNumber = modelim.IdNumber;
            customer.Name = modelim.Name;
            customer.Surname = modelim.Surname;
            customer.Birthday = modelim.Birthday;
            customer.PhoneNumber = modelim.PhoneNumber;
            customer.Email = modelim.Email;

            _databaseContext.SaveChanges();
        }

        public void Delete(Customer customer)
        {
            _databaseContext.Customers.Remove(customer);
            _databaseContext.SaveChanges();
        }
    }
}
