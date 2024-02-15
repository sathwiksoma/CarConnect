using System.Net;
using System.Security.Authentication;
using CarConnect.Model;
using CarConnect.Repository;
using CarConnect.Service;

namespace carconnect.test
{
    public class Tests
    {
        CustomerRepository _customerRepository;
        CustomerService _customerService;
        [SetUp]
        public void Setup()
        {
            _customerRepository = new CustomerRepository();
        }

        [Test]
        public void CustomerAuthenticationTestWhenFail()
        {
            Assert.IsFalse(_customerRepository.AuthenticateCustomer("1", "fcgvha"));
        }


        [Test]
        public void UpdateCustomerTestWhenNotNull()
        {
            Customer updateCustomer = new Customer() { FirstName = "Rohith", LastName = "Pasham", PhoneNumber = "8234567890", Email = "sample@email.com", UserName = "vasanth", Address = "Hyderabad", Password = "password", RegistrationDate = DateTime.Now };

            Assert.That(_customerRepository.RegisterCustomer(updateCustomer));
        }


    }
}
