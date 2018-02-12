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

        public async Task<List<EmpEmployerDiv>> FindEmployerAsync(int id)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"getAllemp_memcountdata";
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = @"SELECT * FROM `empemployerdiv` WHERE `EmpId` = @id or `EmpDivID` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
            //return result.Count > 0 ? result[0] : null;
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
                        IRS_EIN =  await reader.IsDBNullAsync(1) ? "" : await reader.GetFieldValueAsync<string>(1),
                        CompanyName = await reader.IsDBNullAsync(2) ? "" : await reader.GetFieldValueAsync<string>(2),
                        EmpDivID = await reader.IsDBNullAsync(3) ? 0 :  await reader.GetFieldValueAsync<int>(3),
                        DivisionNumber = await reader.IsDBNullAsync(4) ? 0 :  await reader.GetFieldValueAsync<int>(4),
                        NAICS_ID = await reader.IsDBNullAsync(5) ? "" : await reader.GetFieldValueAsync<string>(5),
                        DivisionRelation = await reader.IsDBNullAsync(6) ? "" : await reader.GetFieldValueAsync<string>(6),
                        DivisionName = await reader.IsDBNullAsync(7) ? "" :  await reader.GetFieldValueAsync<string>(7),
                        DBAName = await reader.IsDBNullAsync(8) ? "" : await reader.GetFieldValueAsync<string>(8),
                        ContactPriority = await reader.IsDBNullAsync(9) ? 0 :  await reader.GetFieldValueAsync<int>(9),
                        PositionTitle = await reader.IsDBNullAsync(10) ? "" : await reader.GetFieldValueAsync<string>(10),
                        LastName = await reader.IsDBNullAsync(11) ? "" : await reader.GetFieldValueAsync<string>(11),
                        FirstName = await reader.IsDBNullAsync(12) ? "" : await reader.GetFieldValueAsync<string>(12),
                        MiddleName = await reader.IsDBNullAsync(13) ? "" : await reader.GetFieldValueAsync<string>(13),
                        NameSuffix = await reader.IsDBNullAsync(14) ? "" : await reader.GetFieldValueAsync<string>(14),
                        EmailAddress = await reader.IsDBNullAsync(15) ? "" : await reader.GetFieldValueAsync<string>(15),
                        PostalCode = await reader.IsDBNullAsync(16) ? "" : await reader.GetFieldValueAsync<string>(16),
                        StateProvince = await reader.IsDBNullAsync(17) ? "" : await reader.GetFieldValueAsync<string>(17),
                        City = await reader.IsDBNullAsync(18) ? "" : await reader.GetFieldValueAsync<string>(18),
                        Street1 = await reader.IsDBNullAsync(19) ? "" : await reader.GetFieldValueAsync<string>(19),
                        Street2 = await reader.IsDBNullAsync(20) ? "" : await reader.GetFieldValueAsync<string>(20),
                        EntityTypeID = await reader.IsDBNullAsync(21) ? 0 : await reader.GetFieldValueAsync<int>(21),
                        AccountId = await reader.IsDBNullAsync(22) ? "" : await reader.GetFieldValueAsync<string>(22),
                        IsActive = await reader.GetFieldValueAsync<int>(23),
                        CreatedAt = await reader.GetFieldValueAsync<DateTime>(24),
                        UpdatedAt = await reader.IsDBNullAsync(25) ? DateTime.Now.Date : await  reader.GetFieldValueAsync<DateTime>(25),
                        HasDependents = await reader.IsDBNullAsync(26) ? false : await reader.GetFieldValueAsync<Int32>(26) == 0 ? false : true
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}