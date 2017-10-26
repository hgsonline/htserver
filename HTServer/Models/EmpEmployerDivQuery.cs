using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; 
using System;

namespace HTServer.Models
{
    public class EmpEmployerDivQuery
    {
        public readonly AppDb Db;
        public EmpEmployerDivQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<EmpEmployerDiv> FindOneAsync(int id)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `empemployerdiv` WHERE `EmpId` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<EmpEmployerDiv>> LatestPostsAsync()
        {
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `empemployerdiv` ORDER BY `EmpId` DESC LIMIT 50;";//"SELECT `Id`, `Title`, `Content` FROM `emp_employer` ORDER BY `Id` DESC LIMIT 10;";
            //cmd.CommandType = CommandType.StoredProcedure;
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

        private async Task<List<EmpEmployerDiv>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<EmpEmployerDiv>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new EmpEmployerDiv(Db)
                    {
                        EmpId = await reader.GetFieldValueAsync<int>(0),
                        IRS_EIN = await reader.GetFieldValueAsync<string>(1),
                        CompanyName = await reader.GetFieldValueAsync<string>(2),
                        EmpDivID = await reader.GetFieldValueAsync<int>(3),
                        DivisionNumber = await reader.GetFieldValueAsync<int>(4),
                        NAICS_ID = await reader.GetFieldValueAsync<string>(5),
                        DivisionRelation = await reader.GetFieldValueAsync<string>(6),
                        DivisionName = await reader.GetFieldValueAsync<string>(7),
                        DBAName = await reader.GetFieldValueAsync<string>(8),
                        ContactPriority = await reader.GetFieldValueAsync<int>(9),
                        PositionTitle = await reader.GetFieldValueAsync<string>(10),
                        LastName = await reader.GetFieldValueAsync<string>(11),
                        FirstName = await reader.GetFieldValueAsync<string>(12),
                        MiddleName = await reader.GetFieldValueAsync<string>(13),
                        NameSuffix = await reader.GetFieldValueAsync<string>(14),
                        EmailAddress = await reader.GetFieldValueAsync<string>(15),
                        PostalCode = await reader.GetFieldValueAsync<int>(16),
                        StateProvince = await reader.GetFieldValueAsync<string>(17),
                        City = await reader.GetFieldValueAsync<string>(18),
                        Street1 = await reader.GetFieldValueAsync<string>(19),
                        Street2 = await reader.GetFieldValueAsync<string>(20),
                        EntityTypeID = await reader.GetFieldValueAsync<int>(21),
                        AccountId = await reader.GetFieldValueAsync<string>(22),
                        IsActive = await reader.GetFieldValueAsync<int>(23),
                        CreatedAt = await reader.GetFieldValueAsync<DateTime>(24),
                        UpdatedAt = await reader.IsDBNullAsync(25) ? DateTime.Now.Date : await  reader.GetFieldValueAsync<DateTime>(25)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}