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

    public class EntityWithGuidCiService : ICiService<EntityWithGuid>
    {
        private List<EntityWithGuid> ReadEntities(SqlDataReader reader)
        {
            var list = new List<EntityWithGuid>();
            while (reader.Read())
            {
                var entity = new EntityWithGuid();
                entity.Id = reader.GetGuid(0);
                entity.AFloat = reader.IsDBNull(1) ? (double?)null : reader.GetDouble(1);
                entity.AReal = reader.GetFloat(2);
                entity.ADate = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3);
                entity.ATime = reader.IsDBNull(4) ? (TimeSpan?)null : reader.GetTimeSpan(4);
                entity.AOffset = reader.IsDBNull(5) ? (DateTimeOffset?)null : reader.GetDateTimeOffset(5);
                entity.ADatetime = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6);
                entity.ADatetime2 = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7);
                entity.ASmalldatetime = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8);
                list.Add(entity);
            }
            return list;
        }

        public List<EntityWithGuid> Get(string query, 
            SqlParameter[] parms, 
            SqlConnection conn, 
            SqlTransaction trans)
        {
            using (new ConnectionHandler(conn))
            {
                return CiHelper.ExecuteSelect(query, parms, ReadEntities, conn, trans);
            }
        }

        public List<EntityWithGuid> GetByPrimaryKey(object ids, SqlConnection conn, SqlTransaction trans)
        {
            var idsArray = (Guid[])ids;
            using (new ConnectionHandler(conn))
            {
                var table = CiHelper.CreateTempTableName();
                CreateIdTempTable(table, conn, trans);
                CiHelper.BulkInsert(new SingleKeyDataReader<Guid>(idsArray), table, conn, trans);
                var sql = @"select 
                e.id, e.a_float, e.a_real, e.a_date, e.a_time,
                e.a_offset, e.a_datetime, e.a_datetime2, e.a_smalldatetime
				from entity_with_guid e
                inner join " + table + @" t on 
                e.id = t.id";
                var result = CiHelper.ExecuteSelect(sql, new SqlParameter[0], ReadEntities, conn, trans);
                CiHelper.DropTable(table, conn, trans);
                return result;
            }
        }

		private void CreateIdTempTable(string table, SqlConnection conn, SqlTransaction trans)
        {
            var sql = "CREATE TABLE " + table + " ( id uniqueidentifier )";
            CiHelper.ExecuteNonQuery(sql, new SqlParameter[0], conn, trans);
        }

		public void Insert(List<EntityWithGuid> entities, SqlConnection conn, SqlTransaction trans)
        {
		}
    }
}
