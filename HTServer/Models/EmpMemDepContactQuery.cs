using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;

namespace HTServer.Models
{
    public class EmpMemDepContactQuery
    {
        public readonly AppDb Db;
        public EmpMemDepContactQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<EmpMemDepContact> FindOneAsync(int id)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `EmpMemDepContact` WHERE `MemContactId` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<EmpMemDepContact>> LatestPostsAsync()
        {
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `EmpMemDepContact` ORDER BY `EmpId` DESC LIMIT 50;";//"SELECT `Id`, `Title`, `Content` FROM `emp_employer` ORDER BY `Id` DESC LIMIT 10;";
            //cmd.CommandType = CommandType.StoredProcedure;
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<EmpMemDepContact>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<EmpMemDepContact>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new EmpMemDepContact(Db)
                    {
                        MemContactId = await reader.GetFieldValueAsync<int>(0),
                        MemberId = await reader.GetFieldValueAsync<int>(1),
                        PhoneType = await reader.GetFieldValueAsync<string>(2),
                        AreaCode = await reader.GetFieldValueAsync<int>(3),
                        PhoneNumber = await reader.GetFieldValueAsync<int>(4),
                        PhoneExtension = await reader.GetFieldValueAsync<int>(5),
                        IsActive = await reader.GetFieldValueAsync<int>(6),
                        CreatedAt = await reader.GetFieldValueAsync<DateTime>(7),
                       // UpdatedAt = await reader.GetFieldValueAsync<DateTime>(8)
                          UpdatedAt = await reader.IsDBNullAsync(8) ? DateTime.Now.Date : await reader.GetFieldValueAsync<DateTime>(8)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
