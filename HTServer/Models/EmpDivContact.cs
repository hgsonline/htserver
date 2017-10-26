using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Data;

namespace HTServer.Models
{
    public class EmpDivContact
    {
        [Key]
        public int EmpContactId { get; set; }
        public int EmpId { get; set; }
        [Required]
        public string PhoneType { get; set; }
        public int? AreaCode { get; set; }
        public int? PhoneNumber { get; set; }
        public int? PhoneExtension { get; set; }

        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        public AppDb Db { get; set; }

        public EmpDivContact(AppDb db = null)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"EmpDivContact_RA_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            BindParams(cmd); 
            EmpContactId  = (Int32)await cmd.ExecuteScalarAsync();

            //await cmd.ExecuteNonQueryAsync();
            //EmpContactId = (int)cmd.LastInsertedId;
        }

         
        public async Task UpdateAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE `EmpDivContact` SET `IRS_EIN` = @IRS_EIN, `CompanyName` = @CompanyName WHERE `EmpId` = @EmpId;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `EmpDivContact` WHERE `EmpId` = @EmpId;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@EmpContactId",
                DbType = DbType.Int32,
                Value = EmpContactId,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {

           
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "EmpId",
                DbType = DbType.Int32,
                Value = EmpId,
            });


            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "PhoneType",
                DbType = DbType.String,
                Value = PhoneType,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "AreaCode",
                DbType = DbType.Int32,
                Value = AreaCode,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "PhoneNumber",
                DbType = DbType.Int32,
                Value = PhoneNumber,
            });


            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "PhoneExtension",
                DbType = DbType.Int32,
                Value = PhoneExtension,
            });

        
        }


    }
}
