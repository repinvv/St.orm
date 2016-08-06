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

    public class EntityWithMultikeyCiService : ICiService<EntityWithMultikey>
    {
        private List<EntityWithMultikey> ReadEntities(SqlDataReader reader)
        {
            var list = new List<EntityWithMultikey>();
            while (reader.Read())
            {
                var entity = new EntityWithMultikey
                {
                    Id1 = reader.GetInt32(0),
                    Id2 = reader.GetString(1),
                    Content = reader.IsDBNull(2) ? null : reader.GetString(2),
                };
                list.Add(entity);
            }
            return list;
        }

        public List<EntityWithMultikey> Get(string query, 
            SqlParameter[] parms, 
            SqlConnection conn, 
            SqlTransaction trans)
        {
            return CiHelper.ExecuteSelect(query, parms, ReadEntities, conn, trans);
        }

        #region EntityDataReaders and temp tables
        internal class EntityDataReader : BaseDataReader
        {
            private readonly List<EntityWithMultikey> entities;

            public EntityDataReader(List<EntityWithMultikey> entities) : base(entities.Count)
            {
                this.entities = entities;
            }

            public override object GetValue(int i)
            {
                switch(i)
                {
                    case 0:
                        return entities[current].Id1;
                    case 1:
                        return entities[current].Id2;
                    case 2:
                        return entities[current].Content;
                    default:
                        throw new Exception("EntityWithMultikey Can't read field " + i);
                }            
            }

            public override int FieldCount { get { return 3; } }
        }

        internal class EntityKeyDataReader : BaseDataReader
        {
            private readonly List<EntityWithMultikey> entities;

            public EntityKeyDataReader(List<EntityWithMultikey> entities) : base(entities.Count)
            {
                this.entities = entities;
            }

            public override object GetValue(int i)
            {
                switch(i)
                {
                    case 0:
                        return entities[current].Id1;
                    case 1:
                        return entities[current].Id2;
                    default:
                        throw new Exception("EntityWithMultikey Can't read field " + i);
                }            
            }

            public override int FieldCount { get { return 2; } }
        }

        private void CreateIdTempTable(string table, SqlConnection conn, SqlTransaction trans)
        {
            var sql = "CREATE TABLE " + table + @"(
                id_1 int,
                id_2 nvarchar(256)
                )";
            CiHelper.ExecuteNonQuery(sql, CiHelper.NoParameters, conn, trans);
        }

        private void CreateTempTable(string table, SqlConnection conn, SqlTransaction trans)
        {
            var sql = "CREATE TABLE " + table + @"(
                id_1 int,
                id_2 nvarchar(256),
                content nvarchar(max)
                )";
            CiHelper.ExecuteNonQuery(sql, CiHelper.NoParameters, conn, trans);
        }
        #endregion

        public List<EntityWithMultikey> GetByPrimaryKey(object ids, SqlConnection conn, SqlTransaction trans)
        {
            throw new CiException("Entity EntityWithMultikey has complex primary key, GetByPrimaryKey is not supported");
        }

        public static int MaxAmountForGroupedInsert = 45;

        public void Insert(List<EntityWithMultikey> entities, SqlConnection conn, SqlTransaction trans)
        {
            if(entities.Count > MaxAmountForGroupedInsert)
            {
                CiHelper.BulkInsert(new EntityDataReader(entities), "entity_with_multikey", conn, trans );
            }
            else
            {
                GroupInsert(entities, conn, trans);
            }
        }

        #region group insert methods
        private void GroupInsert(List<EntityWithMultikey> entities, SqlConnection conn, SqlTransaction trans)
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
            sb.AppendLine("INSERT INTO entity_with_multikey");
            sb.AppendLine("(");
            sb.AppendLine("id_1, id_2, content");
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
                sb.Append(", @parm2i"); sb.Append(i);
        }

        private IEnumerable<SqlParameter> GetInsertParameters(EntityWithMultikey entity, int i)
        {
            yield return new SqlParameter("parm0i" + i, SqlDbType.Int)
                { Value = entity.Id1 };
            yield return new SqlParameter("parm1i" + i, SqlDbType.NVarChar)
                { Value = entity.Id2 };
            yield return new SqlParameter("parm2i" + i, SqlDbType.NVarChar)
                { Value = entity.Content ?? (object)DBNull.Value };
        }
        #endregion

        public void Insert(EntityWithMultikey entity, SqlConnection conn, SqlTransaction trans)
        {
            var sql = ConstructInsertRequest(1);
            var parms = GetInsertParameters(entity, 0).ToArray();
            CiHelper.ExecuteNonQuery(sql, parms, conn, trans);
        }

        public void Update(EntityWithMultikey entity, SqlConnection conn, SqlTransaction trans)
        {
            var parms = GetUpdateParameters(entity, 0).ToArray();
            var sql = GetUpdateRequest(0);
            CiHelper.ExecuteNonQuery(sql, parms, conn, trans);
        }

        public static int MaxAmountForGroupedUpdate = 97;

        public void Update(List<EntityWithMultikey> entities, SqlConnection conn, SqlTransaction trans)
        {
            if (entities.Count > MaxAmountForGroupedUpdate)
            {
                BulkUpdate(entities, conn, trans);
            }
            else
            {
                GroupUpdate(entities, conn, trans);
            }
        }

        #region update methods
        private void BulkUpdate(List<EntityWithMultikey> entities, SqlConnection conn, SqlTransaction trans)
        {
            var table = CiHelper.CreateTempTableName();
            CreateTempTable(table, conn, trans);
            CiHelper.BulkInsert(new EntityDataReader(entities), table, conn, trans);
            var sql = @"UPDATE entity_with_multikey SET
    content = s.content
  FROM entity_with_multikey src
  INNER JOIN " + table + @" s on
    src.id_1 = s.id_1 AND src.id_2 = s.id_2;
drop table " + table + ";";
            CiHelper.ExecuteNonQuery(sql, new SqlParameter[0], conn, trans);
        }

        private void GroupUpdate(List<EntityWithMultikey> entities, SqlConnection conn, SqlTransaction trans)
        {
            int i = 0;
            var parms = entities.SelectMany(x => GetUpdateParameters(x, i++)).ToArray();
            var sql = ConstructUpdateRequest(entities.Count);
            CiHelper.ExecuteNonQuery(sql, parms, conn, trans);
        }

        private IEnumerable<SqlParameter> GetUpdateParameters(EntityWithMultikey entity, int i)
        {
            yield return new SqlParameter("parm0i" + i, SqlDbType.NVarChar)
                { Value = entity.Content ?? (object)DBNull.Value };
            yield return new SqlParameter("parm1i" + i, SqlDbType.Int)
                { Value = entity.Id1 };
            yield return new SqlParameter("parm2i" + i, SqlDbType.NVarChar)
                { Value = entity.Id2 };
        }

        private string ConstructUpdateRequest(int count)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.AppendLine(GetUpdateRequest(i));
            }

            return sb.ToString();
        }

        private string GetUpdateRequest(int index)
        {
            return @"UPDATE entity_with_multikey SET
    content = @parm0i" + index + @"
  WHERE id_1 = @parm1i" + index + @",
  AND id_2 = @parm2i" + index + ";";
        }
        #endregion

        public void Delete(EntityWithMultikey entity, SqlConnection conn, SqlTransaction trans)
        {
            
        }

        public void Delete(List<EntityWithMultikey> entities, SqlConnection conn, SqlTransaction trans)
        {

        }

        public void DeleteByPrimaryKey(object ids, SqlConnection conn, SqlTransaction trans)
        {
            throw new CiException("Will not delete EntityWithMultikey by multi key, use other deletion method instead.");
        }

        #region delete methods
        #endregion
    }
}
