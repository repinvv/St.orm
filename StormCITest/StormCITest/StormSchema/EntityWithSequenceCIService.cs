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
    using SomeSchema;

    public class EntityWithSequenceCiService : ICiService<EntityWithSequence>
    {
        #region internal artefacts
        private List<EntityWithSequence> ReadEntities(SqlDataReader reader)
        {
            var list = new List<EntityWithSequence>();
            while (reader.Read())
            {
                var entity = new EntityWithSequence
                {
                    Id = reader.GetInt32(0),
                    AChar = reader.IsDBNull(1) ? null : reader.GetString(1),
                    AVarchar = reader.GetString(2),
                    AText = reader.IsDBNull(3) ? null : reader.GetString(3),
                    ANchar = reader.IsDBNull(4) ? null : reader.GetString(4),
                    ANvarchar = reader.IsDBNull(5) ? null : reader.GetString(5),
                    ANtext = reader.IsDBNull(6) ? null : reader.GetString(6),
                    AXml = reader.IsDBNull(7) ? null : reader.GetString(7),
                    ABinary = reader.IsDBNull(8) ? null : reader.ReadBytes(8),
                    AVarbinary = reader.IsDBNull(9) ? null : reader.ReadBytes(9),
                    AImage = reader.IsDBNull(10) ? null : reader.ReadBytes(10),
                };
                list.Add(entity);
            }
            return list;
        }

        internal class EntityDataReader : BaseDataReader
        {
            private readonly List<EntityWithSequence> entities;

            public EntityDataReader(List<EntityWithSequence> entities) : base(entities.Count)
            {
                this.entities = entities;
            }

            public override object GetValue(int i)
            {
                switch(i)
                {
                    case 0:
                        return entities[current].Id;
                    case 1:
                        return entities[current].AChar;
                    case 2:
                        return entities[current].AVarchar;
                    case 3:
                        return entities[current].AText;
                    case 4:
                        return entities[current].ANchar;
                    case 5:
                        return entities[current].ANvarchar;
                    case 6:
                        return entities[current].ANtext;
                    case 7:
                        return entities[current].AXml;
                    case 8:
                        return entities[current].ABinary;
                    case 9:
                        return entities[current].AVarbinary;
                    case 10:
                        return entities[current].AImage;
                    default:
                        throw new Exception("EntityWithSequence Can't read field " + i);
                }            
            }

            public override int FieldCount { get { return 11; } }
        }

        internal class EntityKeyDataReader : BaseDataReader
        {
            private readonly List<EntityWithSequence> entities;

            public EntityKeyDataReader(List<EntityWithSequence> entities) : base(entities.Count)
            {
                this.entities = entities;
            }

            public override object GetValue(int i)
            {
                return entities[current].Id;
            }

            public override int FieldCount { get { return 1; } }
        }

        private void CreateIdTempTable(string table, SqlConnection conn, SqlTransaction trans)
        {
            var sql = "CREATE TABLE " + table + " ( id int )";
            CiHelper.ExecuteNonQuery(sql, CiHelper.NoParameters, conn, trans);
        }

        private void CreateTempTable(string table, SqlConnection conn, SqlTransaction trans)
        {
            var sql = "CREATE TABLE " + table + @"(
                id int,
                a_char char(1),
                a_varchar varchar(2000),
                a_text text,
                a_nchar nchar(10),
                a_nvarchar nvarchar(max),
                a_ntext ntext,
                a_xml xml,
                a_binary binary(1000),
                a_varbinary varbinary(max),
                a_image image
                )";
            CiHelper.ExecuteNonQuery(sql, CiHelper.NoParameters, conn, trans);
        }
        #endregion

        #region Get
        public List<EntityWithSequence> Get(string query, 
            SqlParameter[] parms, 
            SqlConnection conn, 
            SqlTransaction trans)
        {
            return CiHelper.ExecuteSelect(query, parms, ReadEntities, conn, trans);
        }

        public static int MaxAmountForWhereIn = 300;

        public List<EntityWithSequence> GetByPrimaryKey(object ids, SqlConnection conn, SqlTransaction trans)
        {
            var idsArray = (int[])ids;
            return idsArray.Length > MaxAmountForWhereIn
                ? GetByTempTable(idsArray, conn, trans)
                : GetByWhereIn(idsArray, conn, trans);
        }

        #region getByPrimaryKey internal methods
        private List<EntityWithSequence> GetByWhereIn(int[] idsArray, SqlConnection conn, SqlTransaction trans)
        {
            var whereIn = string.Join(", ", idsArray.Select((x,i) => "@arg" + i));
            var parms = idsArray.Select((x, i) => new SqlParameter("@arg" + i, x)).ToArray();
            var sql = @"select
                id, a_char, a_varchar, a_text, a_nchar, a_nvarchar,
                a_ntext, a_xml, a_binary, a_varbinary, a_image
            from some_schema.entity_with_sequence where id in (" + whereIn + ")";
            return CiHelper.ExecuteSelect(sql, parms, ReadEntities, conn, trans);
        }

        private List<EntityWithSequence> GetByTempTable(int[] idsArray, SqlConnection conn, SqlTransaction trans)
        {
            var table = CiHelper.CreateTempTableName();
            CreateIdTempTable(table, conn, trans);
            CiHelper.BulkInsert(new SingleKeyDataReader<int>(idsArray), table, conn, trans);
            var sql = @"select 
    e.id, e.a_char, e.a_varchar, e.a_text, e.a_nchar, e.a_nvarchar,
    e.a_ntext, e.a_xml, e.a_binary, e.a_varbinary, e.a_image
  from some_schema.entity_with_sequence e
  inner join " + table + @" t on 
    e.id = t.id;
drop table " + table + ";";
            return CiHelper.ExecuteSelect(sql, CiHelper.NoParameters, ReadEntities, conn, trans);
        }
        #endregion
        #endregion

        #region insert
        public static int MaxAmountForGroupedInsert = 10;

        public void Insert(List<EntityWithSequence> entities, SqlConnection conn, SqlTransaction trans)
        {
            if(entities.Count > MaxAmountForGroupedInsert)
            {
                var seq = CiHelper.GetSequenceValues<int>("some_schema.entity_seq", entities.Count, conn, trans);
                entities.ForEach(x => x.Id = seq++);
                CiHelper.BulkInsert(new EntityDataReader(entities), "some_schema.entity_with_sequence", conn, trans );
            }
            else
            {
                GroupInsert(entities, conn, trans);
            }
        }

        public void Insert(EntityWithSequence entity, SqlConnection conn, SqlTransaction trans)
        {        
            var sql = ConstructInsertRequest(1);    
            var parms = GetInsertParameters(entity, 0);
            Func<IDataReader, List<EntityWithSequence>> readId = reader =>
                {
                    if(reader.Read()) entity.Id = reader.GetInt32(0);
                    return null;
                };
            CiHelper.ExecuteSelect(sql, parms, readId, conn, trans);
        }

        #region group insert methods
        private void GroupInsert(List<EntityWithSequence> entities, SqlConnection conn, SqlTransaction trans)
        {
            var parms = entities.SelectMany((x, i) => GetInsertParameters(x, i)).ToArray();
            var sql = ConstructInsertRequest(entities.Count);
            CiHelper.ExecuteSelect(sql, parms, reader => ReadKey(reader, entities), conn, trans);
        }

        private List<EntityWithSequence> ReadKey(SqlDataReader reader, List<EntityWithSequence> entities)
        {
            int i = 0;
            while (reader.Read())
            {
                entities[i++].Id = reader.GetInt32(0);
            }

            return entities;
        }

        private string insertRequestCache;
        private int insertCacheLength;
        private const string SingleInsertRequest = @"INSERT INTO some_schema.entity_with_sequence (
  id, a_char, a_varchar, a_text, a_nchar, a_nvarchar,
  a_ntext, a_xml, a_binary, a_varbinary, a_image
) OUTPUT inserted.id VALUES
  @parm0i0,
  @parm1i0,
  @parm2i0,
  @parm3i0,
  @parm4i0,
  @parm5i0,
  @parm6i0,
  @parm7i0,
  @parm8i0,
  @parm9i0,
  @parm10i0)";

        private string ConstructInsertRequest(int count)
        {
            if(insertCacheLength == count) return insertRequestCache;

            var sb = new StringBuilder();
            sb.AppendLine("INSERT INTO some_schema.entity_with_sequence");
            sb.AppendLine("(");
            sb.AppendLine("id, a_char, a_varchar, a_text, a_nchar, a_nvarchar,");
            sb.AppendLine("a_ntext, a_xml, a_binary, a_varbinary, a_image");
            sb.AppendLine(")OUTPUT inserted.id VALUES");

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
                sb.Append("( NEXT VALUE FOR some_schema.entity_seq");
                sb.Append(", @parm0i"); sb.Append(i);
                sb.Append(", @parm1i"); sb.Append(i);
                sb.Append(", @parm2i"); sb.Append(i);
                sb.Append(", @parm3i"); sb.Append(i);
                sb.Append(", @parm4i"); sb.Append(i);
                sb.Append(", @parm5i"); sb.Append(i);
                sb.Append(", @parm6i"); sb.Append(i);
                sb.Append(", @parm7i"); sb.Append(i);
                sb.Append(", @parm8i"); sb.Append(i);
                sb.Append(", @parm9i"); sb.Append(i);
        }

        private SqlParameter[] GetInsertParameters(EntityWithSequence entity, int i)
        {
            return new[]
            {
                new SqlParameter("parm0i" + i, SqlDbType.Char)
                { Value = entity.AChar ?? (object)DBNull.Value },
                new SqlParameter("parm1i" + i, SqlDbType.VarChar)
                { Value = entity.AVarchar },
                new SqlParameter("parm2i" + i, SqlDbType.Text)
                { Value = entity.AText ?? (object)DBNull.Value },
                new SqlParameter("parm3i" + i, SqlDbType.NChar)
                { Value = entity.ANchar ?? (object)DBNull.Value },
                new SqlParameter("parm4i" + i, SqlDbType.NVarChar)
                { Value = entity.ANvarchar ?? (object)DBNull.Value },
                new SqlParameter("parm5i" + i, SqlDbType.NText)
                { Value = entity.ANtext ?? (object)DBNull.Value },
                new SqlParameter("parm6i" + i, SqlDbType.Xml)
                { Value = entity.AXml ?? (object)DBNull.Value },
                new SqlParameter("parm7i" + i, SqlDbType.Binary)
                { Value = entity.ABinary ?? (object)DBNull.Value },
                new SqlParameter("parm8i" + i, SqlDbType.VarBinary)
                { Value = entity.AVarbinary ?? (object)DBNull.Value },
                new SqlParameter("parm9i" + i, SqlDbType.Image)
                { Value = entity.AImage ?? (object)DBNull.Value },
            };
        }
        #endregion
        #endregion

        #region update
        public void Update(EntityWithSequence entity, SqlConnection conn, SqlTransaction trans)
        {
            var parms = GetUpdateParameters(entity, 0);
            var sql = GetUpdateRequest(0);
            CiHelper.ExecuteNonQuery(sql, parms, conn, trans);
        }

        public static int MaxAmountForGroupedUpdate = 20;

        public void Update(List<EntityWithSequence> entities, SqlConnection conn, SqlTransaction trans)
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
        private const string SingleUpdateRequest = @"UPDATE some_schema.entity_with_sequence SET
    a_char = @parm0i0,
    a_varchar = @parm1i0,
    a_text = @parm2i0,
    a_nchar = @parm3i0,
    a_nvarchar = @parm4i0,
    a_ntext = @parm5i0,
    a_xml = @parm6i0,
    a_binary = @parm7i0,
    a_varbinary = @parm8i0,
    a_image = @parm9i0
  WHERE id = @parm10i0;";

        private void BulkUpdate(List<EntityWithSequence> entities, SqlConnection conn, SqlTransaction trans)
        {
            var table = CiHelper.CreateTempTableName();
            CreateTempTable(table, conn, trans);
            CiHelper.BulkInsert(new EntityDataReader(entities), table, conn, trans);
            var sql = @"UPDATE some_schema.entity_with_sequence SET
    a_char = s.a_char,
    a_varchar = s.a_varchar,
    a_text = s.a_text,
    a_nchar = s.a_nchar,
    a_nvarchar = s.a_nvarchar,
    a_ntext = s.a_ntext,
    a_xml = s.a_xml,
    a_binary = s.a_binary,
    a_varbinary = s.a_varbinary,
    a_image = s.a_image
  FROM some_schema.entity_with_sequence src
  INNER JOIN " + table + @" s on
    src.id = s.id;
drop table " + table + ";";
            CiHelper.ExecuteNonQuery(sql, new SqlParameter[0], conn, trans);
        }

        private void GroupUpdate(List<EntityWithSequence> entities, SqlConnection conn, SqlTransaction trans)
        {
            var parms = entities.SelectMany((x, i) => GetUpdateParameters(x, i)).ToArray();
            var sql = ConstructUpdateRequest(entities.Count);
            CiHelper.ExecuteNonQuery(sql, parms, conn, trans);
        }

        private SqlParameter[] GetUpdateParameters(EntityWithSequence entity, int i)
        {
            return new[]
            {
                new SqlParameter("parm0i" + i, SqlDbType.Char)
                { Value = entity.AChar ?? (object)DBNull.Value },
                new SqlParameter("parm1i" + i, SqlDbType.VarChar)
                { Value = entity.AVarchar },
                new SqlParameter("parm2i" + i, SqlDbType.Text)
                { Value = entity.AText ?? (object)DBNull.Value },
                new SqlParameter("parm3i" + i, SqlDbType.NChar)
                { Value = entity.ANchar ?? (object)DBNull.Value },
                new SqlParameter("parm4i" + i, SqlDbType.NVarChar)
                { Value = entity.ANvarchar ?? (object)DBNull.Value },
                new SqlParameter("parm5i" + i, SqlDbType.NText)
                { Value = entity.ANtext ?? (object)DBNull.Value },
                new SqlParameter("parm6i" + i, SqlDbType.Xml)
                { Value = entity.AXml ?? (object)DBNull.Value },
                new SqlParameter("parm7i" + i, SqlDbType.Binary)
                { Value = entity.ABinary ?? (object)DBNull.Value },
                new SqlParameter("parm8i" + i, SqlDbType.VarBinary)
                { Value = entity.AVarbinary ?? (object)DBNull.Value },
                new SqlParameter("parm9i" + i, SqlDbType.Image)
                { Value = entity.AImage ?? (object)DBNull.Value },
                new SqlParameter("parm10i" + i, SqlDbType.Int)
                { Value = entity.Id },
            };
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
            return @"UPDATE some_schema.entity_with_sequence SET
    a_char = @parm0i" + index + @",
    a_varchar = @parm1i" + index + @",
    a_text = @parm2i" + index + @",
    a_nchar = @parm3i" + index + @",
    a_nvarchar = @parm4i" + index + @",
    a_ntext = @parm5i" + index + @",
    a_xml = @parm6i" + index + @",
    a_binary = @parm7i" + index + @",
    a_varbinary = @parm8i" + index + @",
    a_image = @parm9i" + index + @"
  WHERE id = @parm10i" + index + ";";
        }
        #endregion
        #endregion

        #region delete
        public void Delete(EntityWithSequence entity, SqlConnection conn, SqlTransaction trans)
        {
            var parms = GetDeleteParameters(entity);
            CiHelper.ExecuteNonQuery(SingleDeleteRequest, parms, conn, trans);
        }

        public void Delete(List<EntityWithSequence> entities, SqlConnection conn, SqlTransaction trans)
        {
            if (entities.Count > MaxAmountForWhereIn)
            {
                DeleteByTempTable(entities, conn, trans);
            }
            else
            {
                DeleteByWhereIn(entities, conn, trans);
            }
        }

        public void DeleteByPrimaryKey(object ids, SqlConnection conn, SqlTransaction trans)
        { 
            var idsArray = (int[])ids;
            if (idsArray.Length > MaxAmountForWhereIn)
            {
                DeleteByTempTable(idsArray, conn, trans);
            }
            else
            {
                DeleteByWhereIn(idsArray, conn, trans);
            }
        }

        #region delete methods
        private const string SingleDeleteRequest = @"DELETE FROM some_schema.entity_with_sequence 
  WHERE id = @parm0i0;";

        private void DeleteByTempTable(List<EntityWithSequence> entities, SqlConnection conn, SqlTransaction trans)
        {
            var table = CiHelper.CreateTempTableName();
            CreateIdTempTable(table, conn, trans);
            CiHelper.BulkInsert(new EntityKeyDataReader(entities), table, conn, trans);
            var sql = @"delete e from some_schema.entity_with_sequence e
  inner join " + table + @" t on 
    e.id = t.id;
drop table " + table + ";";
            CiHelper.ExecuteNonQuery(sql, CiHelper.NoParameters, conn, trans);
        }

        private void DeleteByWhereIn(List<EntityWithSequence> entities, SqlConnection conn, SqlTransaction trans)
        {
            var whereIn = string.Join(", ", entities.Select((x, i) => "@arg" + i));
            var parms = entities.Select((x, i) => new SqlParameter("@arg" + i, x.Id)).ToArray();
            var sql = @"delete from some_schema.entity_with_sequence where id in (" + whereIn + ")";
            CiHelper.ExecuteNonQuery(sql, parms, conn, trans);
        }

        private void DeleteByTempTable(int[] idsArray, SqlConnection conn, SqlTransaction trans)
        {
            var table = CiHelper.CreateTempTableName();
            CreateIdTempTable(table, conn, trans);
            CiHelper.BulkInsert(new SingleKeyDataReader<int>(idsArray), table, conn, trans);
            var sql = @"delete e from some_schema.entity_with_sequence e
  inner join " + table + @" t on 
    e.id = t.id;
drop table " + table + ";";
            CiHelper.ExecuteNonQuery(sql, CiHelper.NoParameters, conn, trans);         
        }

        private void DeleteByWhereIn(int[] idsArray, SqlConnection conn, SqlTransaction trans)
        { 
            var whereIn = string.Join(", ", idsArray.Select((x, i) => "@arg" + i));
            var parms = idsArray.Select((x, i) => new SqlParameter("@arg" + i, x)).ToArray();
            var sql = @"delete from some_schema.entity_with_sequence where id in (" + whereIn + ")";
            CiHelper.ExecuteNonQuery(sql, parms, conn, trans);
        }

        private SqlParameter[] GetDeleteParameters(EntityWithSequence entity)
        {
            return new[]
            {
            new SqlParameter("parm0i0", SqlDbType.Int)
                { Value = entity.Id },
            };
        }      
        #endregion
        #endregion
    }
}
