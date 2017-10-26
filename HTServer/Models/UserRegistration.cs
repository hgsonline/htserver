using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
//using Pomelo.Data.MySql;
using Newtonsoft.Json;
using System.Data;
using HTServer.AES256Encryption;

namespace HTServer.Models
{
    public class UserRegistration
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AccountId { get; set; }
        public string Email { get; set; }
        public int UserTypeID { get; set; }

        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        public AppDb Db { get; set; }

        public UserRegistration(AppDb db = null)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand; 
            //cmd.CommandText = @"INSERT INTO `UserMasterTB` (`Username`, `Password`, `Address`, `Birthdate`, `Contact_No`, `Email`, `UserTypeID`, `CreatedOn`) VALUES (@Username, @Password, @Address, @Birthdate, @Contact_No, @Email, @UserTypeID, @CreatedOn);";
            cmd.CommandText = @"UserMasterTB_RA_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            UserID = (int)cmd.LastInsertedId;
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "username",
                DbType = DbType.String,
                Value = Username,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "password",
                DbType = DbType.String,
                Value = EncryptionLibrary.EncryptText(Password),
            });
 

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "AccountId",
                DbType = DbType.String,
                Value = AccountId,
            });
             
 
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "email",
                DbType = DbType.String,
                Value = Email,
            });
             
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "usertypeid",
                DbType = DbType.Int64,
                Value = 2,  //2 = 'User'
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "createdon",
                DbType = DbType.DateTime,
                Value = DateTime.Now,
            });

        }


    }
}
