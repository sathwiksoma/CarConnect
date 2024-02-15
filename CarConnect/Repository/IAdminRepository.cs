using CarConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Repository
{
    internal interface IAdminRepository
    {
        Admin Authenticate(string username, string password);
        Admin GetAdminById(int adminId);

        Admin GetAdminByUsername(string username);

        bool RegisterAdmin(Admin admin);

        bool UpdateAdmin(Admin admindata, string username);

        bool DeleteAdminByID(int id);

    }
}
