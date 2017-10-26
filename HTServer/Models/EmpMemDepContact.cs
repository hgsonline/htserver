using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Data;

namespace HTServer.Models
{
    public class EmpMemDepContact
    {
   
        [Key]
        public int MemContactId { get; set; }
        public int MemberId { get; set; }
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

        public EmpMemDepContact(AppDb db = null)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"EmpMemDepContact_RA_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            BindParams(cmd);
            //await cmd.ExecuteNonQueryAsync();
            //MemberId = (int)cmd.LastInsertedId; 
            MemContactId =  (Int32)await cmd.ExecuteScalarAsync();

        }

        public async Task UpdateAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE `EmpMemDepContact` SET `IRS_EIN` = @IRS_EIN, `CompanyName` = @CompanyName WHERE `EmpId` = @EmpId;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `EmpMemDepContact` WHERE `EmpId` = @EmpId;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@MemContactId",
                DbType = DbType.Int32,
                Value = MemContactId,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {

            
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "MemberId",
                DbType = DbType.Int32,
                Value = MemberId,
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