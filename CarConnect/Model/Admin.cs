using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Model
{
    public class Admin
    {
        private int _adminID;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private string _userName;
        private string _password;
        private string _role;
        private DateTime _joinDate;

        public Admin() { }

        public Admin(int adminID, string firstName, string lastName, string email,string phoneNumber, string userName, string password,string role, DateTime joinDate)
        {
            _adminID = adminID;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _phoneNumber = phoneNumber;
            _userName = userName;
            _password = password;
            _role = role;
            _joinDate = joinDate;
        }

        public int AdminID
        {
            get { return _adminID; }
            set { _adminID = value; }
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

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public DateTime JoinDate
        {
            get { return _joinDate; }
            set { _joinDate = value; }
        }




        public override string ToString()
        {
            return $"AdminID: {_adminID}, " +
                   $"FirstName: {_firstName}, " +
                   $"LastName: {_lastName}, " +
                   $"Email: {_email}, " +
                   $"PhoneNumber: {_phoneNumber}, " +
                   $"UserName: {_userName}, " +
                   $"Role: {_role}, " +
                   $"JoinDate: {_joinDate}";
        }
    }
}
