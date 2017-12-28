using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;

namespace HTServer.Models
{
    public class EmpDepMedicalDataQuery

    {
        public readonly AppDb Db;
        public EmpDepMedicalDataQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<EmpDepMedicalData> FindOneAsync(int id)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `empdepmedicaldata` WHERE `MemberID` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<EmpDepMedicalData>> FindEmployerMemberAsync(int id)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `empdepmedicaldata` WHERE `EmpId` = @id  and `DependentID` = 0";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
            //return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<EmpDepMedicalData>> LatestPostsAsync()
        {
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `empdepmedicaldata` ORDER BY `MemberID` DESC LIMIT 50;";//"SELECT `Id`, `Title`, `Content` FROM `emp_employer` ORDER BY `Id` DESC LIMIT 10;";
            //cmd.CommandType = CommandType.StoredProcedure;
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }
 
        private async Task<List<EmpDepMedicalData>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<EmpDepMedicalData>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new EmpDepMedicalData(Db)
                    {
                        MemberID = await reader.IsDBNullAsync(1) ? 0 : await reader.GetFieldValueAsync<int>(1),
                        IsSmoker = await reader.IsDBNullAsync(2) ? false : await reader.GetFieldValueAsync<int>(2) == 1 ? true : false,
                        SmokerFor = await reader.IsDBNullAsync(3) ? "" : await reader.GetFieldValueAsync<string>(3),
                        SmokingYrs = await reader.IsDBNullAsync(4) ? "" : await reader.GetFieldValueAsync<string>(4),
                        IsDrinker = await reader.IsDBNullAsync(5) ? false : await reader.GetFieldValueAsync<int>(5) == 1 ? true : false,
                        DrinkerFor = await reader.IsDBNullAsync(6) ? "" : await reader.GetFieldValueAsync<string>(6),
                        DrinkingYrs = await reader.IsDBNullAsync(7) ? "" : await reader.GetFieldValueAsync<string>(7),
                        IsDiabetic = await reader.IsDBNullAsync(8) ? false : await reader.GetFieldValueAsync<int>(8) == 1 ? true : false,
                        IsRelDiabetic = await reader.IsDBNullAsync(9) ? false : await reader.GetFieldValueAsync<int>(9) == 1 ? true : false,
                        HasHeartDisease = await reader.IsDBNullAsync(10) ? false : await reader.GetFieldValueAsync<int>(10) == 1 ? true : false,
                        HasRelHeartDisease = await reader.IsDBNullAsync(11) ? false : await reader.GetFieldValueAsync<int>(11) == 1 ? true : false,
                        IsHBP = await reader.IsDBNullAsync(12) ? false : await reader.GetFieldValueAsync<int>(12) == 1 ? true : false,

                        IsRelHBP = await reader.IsDBNullAsync(13) ? false : await reader.GetFieldValueAsync<int>(13) == 1 ? true : false,
                        HasStomachDisorder = await reader.IsDBNullAsync(14) ? false : await reader.GetFieldValueAsync<int>(14) == 1 ? true : false,
                        HasRelStomachDisorder = await reader.IsDBNullAsync(15) ? false : await reader.GetFieldValueAsync<int>(15) == 1 ? true : false,
                        HasLungDisorder = await reader.IsDBNullAsync(16) ? false : await reader.GetFieldValueAsync<int>(16) == 1 ? true : false,
                        HasRelLungDisorder = await reader.IsDBNullAsync(17) ? false : await reader.GetFieldValueAsync<int>(17) == 1 ? true : false,

                        HasCancer = await reader.IsDBNullAsync(18) ? false : await reader.GetFieldValueAsync<int>(18) == 1 ? true : false,
                        HasRelCancer = await reader.IsDBNullAsync(19) ? false : await reader.GetFieldValueAsync<int>(19) == 1 ? true : false,
                        HasKidneyDisease = await reader.IsDBNullAsync(20) ? false : await reader.GetFieldValueAsync<int>(20) == 1 ? true : false,
                        HasRelKidneyDisease = await reader.IsDBNullAsync(21) ? false : await reader.GetFieldValueAsync<int>(21) == 1 ? true : false,
                        HasThyroidDisease = await reader.IsDBNullAsync(22) ? false : await reader.GetFieldValueAsync<int>(22) == 1 ? true : false,

                        HasRelThyroidDisease = await reader.IsDBNullAsync(23) ? false : await reader.GetFieldValueAsync<int>(23) == 1 ? true : false,
                        HasStroke = await reader.IsDBNullAsync(24) ? false : await reader.GetFieldValueAsync<int>(24) == 1 ? true : false,
                        HasRelStroke = await reader.IsDBNullAsync(25) ? false : await reader.GetFieldValueAsync<int>(25) == 1 ? true : false,
                        HasRhumatoidDisease = await reader.IsDBNullAsync(26) ? false : await reader.GetFieldValueAsync<int>(26) == 1 ? true : false,
                        HasRelRhumatoidDisease = await reader.IsDBNullAsync(27) ? false : await reader.GetFieldValueAsync<int>(27) == 1 ? true : false,
                        HasDegenDisorders = await reader.IsDBNullAsync(28) ? false : await reader.GetFieldValueAsync<int>(28) == 1 ? true : false,
                        HasRelDegenDisorders = await reader.IsDBNullAsync(29) ? false : await reader.GetFieldValueAsync<int>(29) == 1 ? true : false,
                        HasSiezureDisorder = await reader.IsDBNullAsync(30) ? false : await reader.GetFieldValueAsync<int>(30) == 1 ? true : false,
                        HasRelSiezureDisorder = await reader.IsDBNullAsync(31) ? false : await reader.GetFieldValueAsync<int>(31) == 1 ? true : false,
                        HasHepatitis = await reader.IsDBNullAsync(32) ? false : await reader.GetFieldValueAsync<int>(32) == 1 ? true : false,
                        HasRelHepatitis = await reader.IsDBNullAsync(33) ? false : await reader.GetFieldValueAsync<int>(33) == 1 ? true : false,
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
