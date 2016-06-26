//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StormTestProject.StormSchema
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
	using System.Collections.Generic;

    public class EntityWithIdCiService : ICiService<EntityWithId>
    {
        private List<EntityWithId> ReadEntities(SqlDataReader reader)
        {
            var list = new List<EntityWithId>();
            while (reader.Read())
            {
                var entity = new EntityWithId();
                entity.Id = reader.GetInt32(0);
                entity.ABigint = reader.IsDBNull(1) ? (long?)null : reader.GetInt64(1);
                entity.AInt = reader.GetInt32(2);
                entity.ANumeric = reader.IsDBNull(3) ? (decimal?)null : reader.GetDecimal(3);
                entity.ABit = reader.IsDBNull(4) ? (bool?)null : reader.GetBoolean(4);
                entity.ASmallint = reader.IsDBNull(5) ? (short?)null : reader.GetInt16(5);
                entity.ADecimal = reader.IsDBNull(6) ? (decimal?)null : reader.GetDecimal(6);
                entity.ASmallmoney = reader.IsDBNull(7) ? (decimal?)null : reader.GetDecimal(7);
                entity.ATinyint = reader.IsDBNull(8) ? (byte?)null : reader.GetByte(8);
                entity.AMoney = reader.IsDBNull(9) ? (decimal?)null : reader.GetDecimal(9);
                list.Add(entity);
            }
            return list;
        }

        public List<EntityWithId> Materialize(string query, 
                            SqlParameter[] parms, 
                            SqlConnection conn, 
                            SqlTransaction trans)
        {
            using (new ConnectionHandler(conn))
            {
                return CiHelper.ExecuteSelect(query, parms, ReadEntities, conn, trans);
            }
        }

        public List<EntityWithId> GetByPrimaryKey(object ids, SqlConnection conn, SqlTransaction trans)
        {
            var idsArray = (int[])ids;
            using (new ConnectionHandler(conn))
            {
                var table = CiHelper.CreateTempTableName();
                CreateIdTempTable(table, conn, trans);
                CiHelper.BulkInsert(new SingleKeyDataReader<int>(idsArray), table, conn, trans);
                var sql = @"select 
                e.id, e.a_bigint, e.a_int, e.a_numeric, e.a_bit,
                e.a_smallint, e.a_decimal, e.a_smallmoney, e.a_tinyint, e.a_money
				from entity_with_id e
                inner join " + table + @" t on 
                e.id = t.id";
                var result = CiHelper.ExecuteSelect(sql, new SqlParameter[0], ReadEntities, conn, trans);
                CiHelper.DropTable(table, conn, trans);
                return result;
            }
        }

		private void CreateIdTempTable(string table, SqlConnection conn, SqlTransaction trans)
        {
            var sql = "CREATE TABLE " + table + @"(
                id int
                )";
            CiHelper.ExecuteNonQuery(sql, new SqlParameter[0], conn, trans);
        }
    }
}
