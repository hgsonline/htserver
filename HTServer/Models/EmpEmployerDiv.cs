using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Data;
using HTServer.AES256Encryption;

namespace HTServer.Models
{

    public class EmpEmployerDiv 
    {
        [Key]
        public int EmpId { get; set; }
        public string IRS_EIN { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public int? EmpDivID { get; set; }
        [Required]
        public int? DivisionNumber { get; set; }
        public string NAICS_ID { get; set; }
        [Required]
        public string DivisionRelation { get; set; }
        [Required]
        public string DivisionName { get; set; }

        public string DBAName { get; set; }
        [Required]
        public int ContactPriority { get; set; }
        [Required]
        public string PositionTitle { get; set; }
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
        public int? PostalCode { get; set; }
        [Required]
        public string StateProvince { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [RegularExpression("[A-Za-z0-9].*")]
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        [Required]
        public int? EntityTypeID { get; set; }
        public string AccountId { get; set; }

        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


        [JsonIgnore]
        public AppDb Db { get; set; }

        public EmpEmployerDiv(AppDb db = null)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"EmpEmployerDiv_RA_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            BindParams(cmd);
            //await cmd.ExecuteNonQueryAsync();
            //EmpId = (int)cmd.LastInsertedId; 
            EmpId  = (Int32)await cmd.ExecuteScalarAsync(); 

            cmd.CommandText = @"UPDATE usermastertb a SET a.Password = @Password WHERE a.AccountId =  @Accountid and a.UserTypeID = 2; ";
            cmd.CommandType = CommandType.Text;
            BindParamsPwd(cmd, EmpId);
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


            public async Task UpdateAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE `emp_employer` SET `IRS_EIN` = @IRS_EIN, `CompanyName` = @CompanyName WHERE `EmpId` = @EmpId;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `EmpEmployerDiv` WHERE `EmpId` = @EmpId;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@EmpId",
                DbType = DbType.Int32,
                Value = EmpId,
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
                ParameterName = "IRS_EIN",
                DbType = DbType.String,
                Value = IRS_EIN,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "companyname",
                DbType = DbType.String,
                Value = CompanyName,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "EmpDivID",
                DbType = DbType.Int32,
                Value = EmpDivID,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "DivisionNumber",
                DbType = DbType.Int32,
                Value = DivisionNumber,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "NAICS_ID",
                DbType = DbType.String,
                Value = NAICS_ID,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "DivisionRelation",
                DbType = DbType.String,
                Value = DivisionRelation,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "DivisionName",
                DbType = DbType.String,
                Value = DivisionName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "DBAName",
                DbType = DbType.String,
                Value = DBAName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "ContactPriority",
                DbType = DbType.Int32,
                Value = ContactPriority,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "PositionTitle",
                DbType = DbType.String,
                Value = PositionTitle,
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
                DbType = DbType.Int32,
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
                ParameterName = "EntityTypeID",
                DbType = DbType.Int32,
                Value = EntityTypeID,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "AccountId",
                DbType = DbType.String,
                Value = AccountId,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "CreatedAt",
                DbType = DbType.DateTime,
                Value = CreatedAt,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "UpdatedAt",
                DbType = DbType.DateTime,
                Value = UpdatedAt,
            });
        }
                
    }
}
