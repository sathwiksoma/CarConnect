using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Service
{
    internal interface IReportService
    {
        void GetReservationHistory();
        void GetVehicleUtilizationData();
        void GetRevenueData();
    }
}
