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

    public class EntityWithoutKeyCiService : ICiService<EntityWithoutKey>
    {
        private List<EntityWithoutKey> ReadEntities(SqlDataReader reader)
        {
            var list = new List<EntityWithoutKey>();
            while (reader.Read())
            {
                var entity = new EntityWithoutKey();
                entity.Value = reader.GetInt32(0);
                entity.Content = reader.IsDBNull(1) ? null : reader.GetString(1);
                list.Add(entity);
            }
            return list;
        }

        public List<EntityWithoutKey> Get(string query, 
            SqlParameter[] parms, 
            SqlConnection conn, 
            SqlTransaction trans)
        {
            using (new ConnectionHandler(conn))
            {
                return CiHelper.ExecuteSelect(query, parms, ReadEntities, conn, trans);
            }
        }

        public List<EntityWithoutKey> GetByPrimaryKey(object ids, SqlConnection conn, SqlTransaction trans)
        {
            throw new Exception("Entity EntityWithoutKey has no primary key");
        }

		private void CreateIdTempTable(string table, SqlConnection conn, SqlTransaction trans)
        {
            var sql = "CREATE TABLE " + table + @"(
                )";
            CiHelper.ExecuteNonQuery(sql, new SqlParameter[0], conn, trans);
        }
    }
}
