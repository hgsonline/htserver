using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;

namespace HTServer.Models
{
    public class PrvProviderQuery
    {
        public readonly AppDb Db;
        public PrvProviderQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<PrvProvider> FindOneAsync(int id)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `prvprovider` WHERE `ProviderID` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        //public async Task<PrvProvider> FindEmployerAsync(int id)
        //{
        //    var cmd = Db.Connection.CreateCommand() as MySqlCommand;
        //    cmd.CommandText = @"SELECT * FROM `empemployerdiv` WHERE `EmpId` = @id or `EmpDivID` = @id";
        //    cmd.Parameters.Add(new MySqlParameter
        //    {
        //        ParameterName = "@id",
        //        DbType = DbType.Int32,
        //        Value = id,
        //    });
        //    var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
        //    return result.Count > 0 ? result[0] : null;
        //}


        public async Task<List<PrvProvider>> LatestPostsAsync()
        {
            var cmd = Db.Connection.CreateCommand();
             
               
            cmd.CommandText = @"SELECT * FROM `prvprovider` ORDER BY `ProviderID` DESC LIMIT 50;";//"SELECT `Id`, `Title`, `Content` FROM `emp_employer` ORDER BY `Id` DESC LIMIT 10;";
                      
             
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        //public async Task DeleteAllAsync()
        //{
        //    var txn = await Db.Connection.BeginTransactionAsync();
        //    try
        //    {
        //        var cmd = Db.Connection.CreateCommand();
        //        cmd.CommandText = @"DELETE FROM `emp_employer`";
        //        await cmd.ExecuteNonQueryAsync();
        //        await txn.Commit();
        //    }
        //    catch
        //    {
        //        await txn.RollbackAsync();
        //        throw;
        //    }
        //}

        private async Task<List<PrvProvider>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<PrvProvider>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new PrvProvider(Db)
                    {
                        ProviderID = await reader.GetFieldValueAsync<int>(0),
                        NPI = await reader.GetFieldValueAsync<int>(1),
                        GovtID = await reader.GetFieldValueAsync<string>(2),
                        DOB = await reader.GetFieldValueAsync<DateTime>(3),
                        Sex = await reader.GetFieldValueAsync<string>(4),
                        DateAssumed = await reader.GetFieldValueAsync<DateTime>(5),
                        LastName = await reader.GetFieldValueAsync<string>(6),
                        FirstName = await reader.GetFieldValueAsync<string>(7),
                        MiddleName = await reader.GetFieldValueAsync<string>(8),
                        NameSuffix = await reader.GetFieldValueAsync<string>(9),
                        AccountId = await reader.GetFieldValueAsync<string>(10),
                        IsActive = await reader.GetFieldValueAsync<int>(11),
                        CreatedAt = await reader.GetFieldValueAsync<DateTime>(12),
                        UpdatedAt = await reader.IsDBNullAsync(13) ? DateTime.Now.Date : await reader.GetFieldValueAsync<DateTime>(13)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

    }
}
