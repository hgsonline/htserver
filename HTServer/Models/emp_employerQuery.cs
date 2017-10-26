using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
//using Pomelo.Data.MySql;


namespace HTServer.Models
{
    public class emp_employerQuery
    {
        public readonly AppDb Db;
        public emp_employerQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<emp_employer> FindOneAsync(int id)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `emp_employer` WHERE `EmpId` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<emp_employer>> LatestPostsAsync()
        {
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"getAllemp_employer";//"SELECT `Id`, `Title`, `Content` FROM `emp_employer` ORDER BY `Id` DESC LIMIT 10;";
            cmd.CommandType = CommandType.StoredProcedure;
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

        private async Task<List<emp_employer>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<emp_employer>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new emp_employer(Db)
                    {
                        EmpID = await reader.GetFieldValueAsync<int>(0),
                        IRS_EIN = await reader.GetFieldValueAsync<string>(1),
                        CompanyName = await reader.GetFieldValueAsync<string>(2)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}