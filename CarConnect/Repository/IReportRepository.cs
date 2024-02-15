using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Repository
{
    internal interface IReportRepository
    {
        void GenerateAdminReport();
        DataTable GetReservationHistory();
        DataTable GetVehicleUtilizationData();
        DataTable GetRevenueData();
        void DisplayDataTable(DataTable dataTable);

    }
}
