using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
//using Pomelo.Data.MySql;

using Newtonsoft.Json;
using System.Data;


namespace HTServer.Models
{
    public class emp_employer
    {
        public int EmpID { get; set; }
        public string IRS_EIN { get; set; }
        public string CompanyName { get; set; }

        [JsonIgnore]
        public AppDb Db { get; set; }

        public emp_employer(AppDb db = null)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            //cmd.CommandText = @"INSERT INTO `emp_employer` (`Title`, `Content`) VALUES (@title, @content);";
            cmd.CommandText = @"insertemp_employer";
            cmd.CommandType = CommandType.StoredProcedure;
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            EmpID = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE `emp_employer` SET `IRS_EIN` = @IRS_EIN, `CompanyName` = @CompanyName WHERE `EmpID` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `emp_employer` WHERE `EmpID` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@EmpID",
                DbType = DbType.Int32,
                Value = EmpID,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@IRS_EIN",
                DbType = DbType.String,
                Value = IRS_EIN,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@CompanyName",
                DbType = DbType.String,
                Value = CompanyName,
            });
        }


    }
}
