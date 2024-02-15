using CarConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Service
{
    public interface IAdminService
    {
        bool Authenticate(string username, string password);

        void GetAdminById(int adminId);
        void GetAdminByUsername(string username);
        void RegisterAdmin();
        void UpdateAdmin();
        void DeleteAdmin();
    }
}
