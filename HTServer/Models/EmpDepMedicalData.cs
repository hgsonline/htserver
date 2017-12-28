using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Data;
using HTServer.AES256Encryption;

namespace HTServer.Models
{
    public class EmpDepMedicalData
    {
        [Key]
        public int MemberID { get; set; }
        public bool IsSmoker { get; set; }
        public string SmokerFor { get; set; }
        public string SmokingYrs { get; set; }
        public bool IsDrinker { get; set; }
        public string DrinkerFor { get; set; }
        public string DrinkingYrs { get; set; }
        public bool IsDiabetic { get; set; }
        public bool IsRelDiabetic { get; set; }
        public bool HasHeartDisease { get; set; }
        public bool HasRelHeartDisease { get; set; }
        public bool IsHBP { get; set; }
        public bool IsRelHBP { get; set; }
        public bool HasStomachDisorder { get; set; }
        public bool HasRelStomachDisorder { get; set; }
        public bool HasLungDisorder { get; set; }
        public bool HasRelLungDisorder { get; set; }
        public bool HasCancer { get; set; }
        public bool HasRelCancer { get; set; }
        public bool HasKidneyDisease { get; set; }
        public bool HasRelKidneyDisease { get; set; }

        public bool HasThyroidDisease { get; set; }
        public bool HasRelThyroidDisease { get; set; }
        public bool HasStroke { get; set; }
        public bool HasRelStroke { get; set; }
        public bool HasRhumatoidDisease { get; set; }
        public bool HasRelRhumatoidDisease { get; set; }
        public bool HasDegenDisorders { get; set; }
        public bool HasRelDegenDisorders { get; set; }

        public bool HasSiezureDisorder { get; set; }
        public bool HasRelSiezureDisorder { get; set; }
        public bool HasHepatitis { get; set; }
        public bool HasRelHepatitis { get; set; }



        [JsonIgnore]
        public AppDb Db { get; set; }

        public EmpDepMedicalData(AppDb db = null)
        {
            Db = db;
        }

        public async Task InsertOrUpdateAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"EmpDepMedicalData_RA";
            cmd.CommandType = CommandType.StoredProcedure;
            BindParams(cmd);
            //await cmd.ExecuteNonQueryAsync();
            //MemberID = (int)cmd.LastInsertedId; 
            MemberID = (Int32)await cmd.ExecuteScalarAsync();

            await cmd.ExecuteNonQueryAsync();


        }
        public async Task UpdateAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"EmpDepMedicalData_RA";
            cmd.CommandType = CommandType.StoredProcedure;
            BindParams(cmd);
            BindId(cmd);
            //await cmd.ExecuteNonQueryAsync();
            //EmpId = (int)cmd.LastInsertedId; 
            MemberID = (Int32)await cmd.ExecuteNonQueryAsync();


        }
  
        public async Task DeleteAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `EmpDepMedicalData` WHERE `MemberID` = @MemberID;";
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
                ParameterName = "MemberID",
                DbType = DbType.Int32,
                Value = MemberID,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "IsSmoker",
                DbType = DbType.Boolean,
                Value = IsSmoker,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "SmokerFor",
                DbType = DbType.String,
                Value = SmokerFor,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "SmokingYrs",
                DbType = DbType.String,
                Value = SmokingYrs,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "IsDrinker",
                DbType = DbType.Boolean,
                Value = IsDrinker,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "DrinkerFor",
                DbType = DbType.String,
                Value = DrinkerFor,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "DrinkingYrs",
                DbType = DbType.String,
                Value = DrinkingYrs,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "IsDiabetic",
                DbType = DbType.Boolean,
                Value = IsDiabetic,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "IsRelDiabetic",
                DbType = DbType.Boolean,
                Value = IsRelDiabetic,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasHeartDisease",
                DbType = DbType.Boolean,
                Value = HasHeartDisease,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasRelHeartDisease",
                DbType = DbType.Boolean,
                Value = HasRelHeartDisease,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "IsHBP",
                DbType = DbType.Boolean,
                Value = IsHBP,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "IsRelHBP",
                DbType = DbType.Boolean,
                Value = IsRelHBP,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasStomachDisorder",
                DbType = DbType.Boolean,
                Value = HasStomachDisorder,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasRelStomachDisorder",
                DbType = DbType.Boolean,
                Value = HasRelStomachDisorder,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasLungDisorder",
                DbType = DbType.Boolean,
                Value = HasLungDisorder,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasRelLungDisorder",
                DbType = DbType.Boolean,
                Value = HasRelLungDisorder,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasCancer",
                DbType = DbType.Boolean,
                Value = HasCancer,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasRelCancer",
                DbType = DbType.Boolean,
                Value = HasRelCancer,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasKidneyDisease",
                DbType = DbType.Boolean,
                Value = HasKidneyDisease,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasRelKidneyDisease",
                DbType = DbType.Boolean,
                Value = HasRelKidneyDisease,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasThyroidDisease",
                DbType = DbType.Boolean,
                Value = HasThyroidDisease,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasRelThyroidDisease",
                DbType = DbType.Boolean,
                Value = HasRelThyroidDisease,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasStroke",
                DbType = DbType.Boolean,
                Value = HasStroke,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasRelStroke",
                DbType = DbType.Boolean,
                Value = HasRelStroke,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasRhumatoidDisease",
                DbType = DbType.Boolean,
                Value = HasRhumatoidDisease,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasRelRhumatoidDisease",
                DbType = DbType.Boolean,
                Value = HasRelRhumatoidDisease,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasDegenDisorders",
                DbType = DbType.Boolean,
                Value = HasDegenDisorders,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasRelDegenDisorders",
                DbType = DbType.Boolean,
                Value = HasRelDegenDisorders,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasSiezureDisorder",
                DbType = DbType.Boolean,
                Value = HasSiezureDisorder,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasRelSiezureDisorder",
                DbType = DbType.Boolean,
                Value = HasRelSiezureDisorder,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasHepatitis",
                DbType = DbType.Boolean,
                Value = HasHepatitis,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "HasRelHepatitis",
                DbType = DbType.Boolean,
                Value = HasRelHepatitis,
            });
        }
    }
}
