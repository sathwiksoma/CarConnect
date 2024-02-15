using CarConnect.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Service
{
    internal class ReportService : IReportService
    {
        readonly IReportRepository _reportRepository;
        public ReportService()
        {
            _reportRepository = new ReportRepository();
        }

        public void GetReservationHistory()
        {
            DisplayDataTable(_reportRepository.GetReservationHistory());
        }

        public void GetRevenueData()
        {
            DisplayDataTable(_reportRepository.GetRevenueData());
        }

        public void GetVehicleUtilizationData()
        {
            DisplayDataTable(_reportRepository.GetVehicleUtilizationData());
        }

        public void DisplayDataTable(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn column in dataTable.Columns)
                {
                    Console.Write($"{column.ColumnName}: {row[column]} | ");
                }
                Console.WriteLine();
            }
        }
    }
}
