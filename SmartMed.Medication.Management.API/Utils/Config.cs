using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMed.MedicationManagement.API.Utils
{
    public class Config
    {
        public static string CS { get; set; }

        static Config()
        {
            string configDir = Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder().SetBasePath(configDir).AddJsonFile("appsettings.json");

            IConfiguration Configuration = builder.Build();

            CS = "Data Source = E:\\Projects\\SmartMed.Medication.Management.API\\DataBase.sqlite; Version=3;";
        }
    }
}
