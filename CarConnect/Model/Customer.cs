using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Model
{
    public class Customer
    {
        private int _customerID;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private string _userName;
        private string _address;
        private string _password;
        private DateTime _registrationDate;

        public int CustomerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public DateTime RegistrationDate
        {
            get { return _registrationDate; }
            set { _registrationDate = value; }
        }

        public Customer() { }

        public Customer(int customerID, string firstName, string lastName, string email, string phoneNumber, string userName, string address, string password, DateTime registrationDate)
        {
            _customerID = customerID;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _phoneNumber = phoneNumber;
            _userName = userName;
            _address = address;
            _password = password;
            _registrationDate = registrationDate;
        }

        public override string ToString()
        {
            return $"  CustomerID       :  {_customerID}\n" +
                   $"  FirstName        :  {_firstName}\n" +
                   $"  LastName         :  {_lastName}\n" +
                   $"  Email            :  {_email}\n" +
                   $"  PhoneNumber      :  {_phoneNumber}\n" +
                   $"  UserName         :  {_userName}\n" +
                   $"  Address          :  {_address}\n" +
                   $"  Password         :  {_password}\n" +
                   $"  RegistrationDate :  {_registrationDate.ToShortDateString()}";
        }
    }

}
