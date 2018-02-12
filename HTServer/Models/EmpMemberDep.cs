using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Data;
using HTServer.AES256Encryption;

namespace HTServer.Models
{
    public class EmpMemberDep
    {
        [Key]
        public int MemberID { get; set; }
        [Required]
        public int? EmpId { get; set; }
        [Required]
        public int? DependentID { get; set; }
        [Required]
        public string DependentType { get; set; }        
        [Required]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        [Required]
        public string Sex { get; set; }
        [Required]
        public string GovtID { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateAssumed { get; set; } 
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string NameSuffix { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string StateProvince { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [RegularExpression("[A-Za-z0-9].*")]
        public string Street1 { get; set; }
        public string Street2 { get; set; } 


        public string AccountId { get; set; }
        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool HasMedicalData { get; set; }

        [JsonIgnore]
        public AppDb Db { get; set; }

        public EmpMemberDep(AppDb db = null)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"EmpMemberDep_RA_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            BindParams(cmd);
            //await cmd.ExecuteNonQueryAsync();
            //MemberID = (int)cmd.LastInsertedId; 
            MemberID = (Int32)await cmd.ExecuteScalarAsync();

            cmd.CommandText = @"UPDATE usermastertb a SET a.Password = @Password WHERE a.AccountId =  @Accountid and a.UserTypeID = 3; ";
            cmd.CommandType = CommandType.Text;
            BindParamsPwd(cmd, MemberID);
            await cmd.ExecuteNonQueryAsync();


        }
        public async Task UpdateAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"EmpMemberDep_RA_Update";
            cmd.CommandType = CommandType.StoredProcedure;
            BindParams(cmd);
            BindId(cmd);
            //await cmd.ExecuteNonQueryAsync();
            //EmpId = (int)cmd.LastInsertedId; 
            MemberID = (Int32)await cmd.ExecuteNonQueryAsync();


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

        //public async Task UpdateAsync()
        //{
        //    var cmd = Db.Connection.CreateCommand() as MySqlCommand;
        //    cmd.CommandText = @"UPDATE `EmpMemberDep` SET `IRS_EIN` = @IRS_EIN, `CompanyName` = @CompanyName WHERE `MemberID` = @MemberID;";
        //    BindParams(cmd);
        //    BindId(cmd);
        //    await cmd.ExecuteNonQueryAsync();
        //}

        public async Task DeleteAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE `EmpMemberDep` SET IsActive=0 WHERE `MemberID` = @MemberID;";
            //cmd.CommandText = @"DELETE FROM `EmpMemberDep` WHERE `MemberID` = @MemberID;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@MemberID",
                DbType = DbType.Int32,
                Value = MemberID,
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
                ParameterName = "DependentID",
                DbType = DbType.Int32,
                Value = DependentID,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "DependentType",
                DbType = DbType.String,
                Value = DependentType,
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
                ParameterName = "GovtID",
                DbType = DbType.String,
                Value = GovtID,
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
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "EmailAddress",
                DbType = DbType.String,
                Value = EmailAddress,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "PostalCode",
                DbType = DbType.String,
                Value = PostalCode,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "StateProvince",
                DbType = DbType.String,
                Value = StateProvince,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "City",
                DbType = DbType.String,
                Value = City,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "Street1",
                DbType = DbType.String,
                Value = Street1,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "Street2",
                DbType = DbType.String,
                Value = Street2,
            });
             
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "AccountId",
                DbType = DbType.String,
                Value = AccountId,
            });
            
        }


    }
}
