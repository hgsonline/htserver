using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Data;
using HTServer.AES256Encryption;

namespace HTServer.Models
{
    public class PrvProvider
    {
        [Key]
        public int ProviderID { get; set; }
        public int NPI { get; set; }
        public string GovtID { get; set; }
        public DateTime? DOB { get; set; }
        public string Sex { get; set; }
        public DateTime? DateAssumed { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string NameSuffix { get; set; }

        public string AccountId { get; set; }
        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        public AppDb Db { get; set; }

        public PrvProvider(AppDb db = null)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {

            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"PrvProvider_RA_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            BindParams(cmd);
            //await cmd.ExecuteNonQueryAsync();
            //EmpId = (int)cmd.LastInsertedId; 
            ProviderID = (Int32)await cmd.ExecuteScalarAsync();

            cmd.CommandText = @"UPDATE usermastertb a SET a.Password = @Password WHERE a.AccountId =  @Accountid; ";
            cmd.CommandType = CommandType.Text;
            BindParamsPwd(cmd, ProviderID);
            await cmd.ExecuteNonQueryAsync();


        }
        private void BindParamsPwd(MySqlCommand cmd, int AccountId)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Accountid",
                DbType = DbType.Int32,
                Value = AccountId,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Password",
                DbType = DbType.String,
                Value = EncryptionLibrary.EncryptText(AccountId.ToString()),
            });
        }
        private void BindParams(MySqlCommand cmd)
        {

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "ProviderID",
                DbType = DbType.Int32,
                Value = ProviderID,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "NPI",
                DbType = DbType.Int32,
                Value = NPI,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "GovtID",
                DbType = DbType.String,
                Value = GovtID,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "DOB",
                DbType = DbType.DateTime,
                Value = DOB,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "Sex",
                DbType = DbType.String,
                Value = Sex,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "DateAssumed",
                DbType = DbType.DateTime,
                Value = DateAssumed,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "LastName",
                DbType = DbType.String,
                Value = LastName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "FirstName",
                DbType = DbType.String,
                Value = FirstName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "MiddleName",
                DbType = DbType.String,
                Value = MiddleName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "NameSuffix",
                DbType = DbType.String,
                Value = NameSuffix,
            });

        }
    }
}
