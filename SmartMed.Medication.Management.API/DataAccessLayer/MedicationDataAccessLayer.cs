using SmartMed.MedicationManagement.API.Models;
using SmartMed.MedicationManagement.API.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMed.MedicationManagement.API.DataAccessLayer
{
    public class MedicationDataAccessLayer
    {
        public int AddMedication(string name, int quantity)
        {
            int medicationID = 0;

            if(name != null && name != "" )
            {
                try
                {
                    using (var m_dbConnection = new SQLiteConnection(Config.CS))
                    {
                        m_dbConnection.Open();

                        string query = @"INSERT INTO medication(name, quantity) VALUES (@name, @quantity)";

                        using (var cmd = new SQLiteCommand(query, m_dbConnection))
                        {
                            cmd.Parameters.Add(new SQLiteParameter() { ParameterName = "name", DbType = DbType.String, Value = name });
                            cmd.Parameters.Add(new SQLiteParameter() { ParameterName = "quantity", DbType = DbType.Int32, Value = quantity > 0 ? quantity : 1 });

                            medicationID = cmd.ExecuteNonQuery();
                        }
                    }

                }
                catch (Exception ex)
                {
                   Console.WriteLine("Unhandled exception: {0}", ex.ToString());
                }
            }
            return medicationID;
        }

        public List<Medication> ListMedication()
        {
            List<Medication> listMedication = null;
            try
            {
                using (var m_dbConnection = new SQLiteConnection(Config.CS))
                {
                    m_dbConnection.Open();

                    string query = @"
SELECT * 
FROM medication";

                    using (var cmd = new SQLiteCommand(query, m_dbConnection))
                    {

                        using (var rdr = cmd.ExecuteReader())
                        {
                            listMedication = new List<Medication>();

                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    listMedication.Add(new Medication()
                                    {
                                        MedicationID = Convert.ToInt32(rdr["medication_id"]),
                                        Name = rdr["name"].ToString(),
                                        Quantity = Convert.ToInt32(rdr["quantity"].ToString()),
                                        CreationDate = DateTime.Parse(rdr["creation_date"].ToString())
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unhandled exception: {0}", ex.ToString());
            }

            return listMedication;
        }


        public bool DeleteMedication(int medicationID)
        {
            bool deleted = false;

            if (medicationID > 0)
            {
                try
                {
                    using (var m_dbConnection = new SQLiteConnection(Config.CS))
                    {
                        m_dbConnection.Open();

                        string query = @"DELETE FROM medication where medication_id = @medicationID";

                        using (var cmd = new SQLiteCommand(query, m_dbConnection))
                        {
                            cmd.Parameters.Add(new SQLiteParameter() { ParameterName = "medicationID", DbType = DbType.Int32, Value = medicationID });

                            deleted = cmd.ExecuteNonQuery() > 0;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unhandled exception: {0}", ex.ToString());
                }
            }
            return deleted;
        }

    }
}
