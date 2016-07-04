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
    using System.Linq;
    using System.Text;

    public class EntityWithoutKeyCiService : ICiService<EntityWithoutKey>
    {
        private List<EntityWithoutKey> ReadEntities(SqlDataReader reader)
        {
            var list = new List<EntityWithoutKey>();
            while (reader.Read())
            {
                var entity = new EntityWithoutKey
                {
                    Value = reader.GetInt32(0),
                    Content = reader.IsDBNull(1) ? null : reader.GetString(1),
                };
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

        #region EntityDataReader
        internal class EntityDataReader : BaseDataReader
        {
            private readonly List<EntityWithoutKey> entities;

            public EntityDataReader(List<EntityWithoutKey> entities) : base(entities.Count)
            {
                this.entities = entities;
            }

            public override object GetValue(int i)
            {
                switch(i)
                {
                    case 0:
                        return entities[current].Value;
                    case 1:
                        return entities[current].Content;
                    default:
                        throw new Exception("EntityWithoutKey Can't read field " + i);
                }
            }

            public override int FieldCount { get { return 2; } }
        }
        #endregion

        public static int MinAmountForBulk = 10;

        public void Insert(List<EntityWithoutKey> entities, SqlConnection conn, SqlTransaction trans)
        {
            using (new ConnectionHandler(conn))
            {
                if(entities.Count >= MinAmountForBulk)
                {
                    CiHelper.BulkInsert(new EntityDataReader(entities), "entity_without_key", conn, trans );
                }
                else
                {
                    RangeInsert(entities, conn, trans);
                }
            }
        }

        #region range insert methods
        private void RangeInsert(List<EntityWithoutKey> entities, SqlConnection conn, SqlTransaction trans)
        {
            int i = 0;
            var parms = entities.SelectMany(x => GetInsertParameters(x, i++)).ToArray();
            var sql = ConstructInsertRequest(entities.Count);
            CiHelper.ExecuteNonQuery(sql, parms, conn, trans);
        }

        private string insertRequestCache;
        private int insertCacheLength;

        private string ConstructInsertRequest(int count)
        {
            if(insertCacheLength == count) return insertRequestCache;

            var sb = new StringBuilder();
            sb.AppendLine("INSERT INTO entity_without_key");
            sb.AppendLine("(");
            sb.AppendLine("value, content");
            sb.AppendLine(") VALUES");

            AppendInsertKeys(sb, 0);
            for (int i = 1; i < count; i++)
            {
                sb.AppendLine("), ");
                AppendInsertKeys(sb, i);
            }
            
            sb.AppendLine(")");
            insertCacheLength = count;
            return insertRequestCache = sb.ToString();
        }

        private void AppendInsertKeys(StringBuilder sb, int i)
        {
                sb.Append("( @parm0i"); sb.Append(i);
                sb.Append(", @parm1i"); sb.Append(i);
        }

        private IEnumerable<SqlParameter> GetInsertParameters(EntityWithoutKey entity, int i)
        {
            yield return new SqlParameter("parm0i" + i, entity.Value);
            yield return new SqlParameter("parm1i" + i, entity.Content ?? (object)DBNull.Value);
        }
        #endregion
    }
}
