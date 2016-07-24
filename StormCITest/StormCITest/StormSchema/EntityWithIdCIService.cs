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

    public class EntityWithIdCiService : ICiService<EntityWithId>
    {
        private List<EntityWithId> ReadEntities(SqlDataReader reader)
        {
            var list = new List<EntityWithId>();
            while (reader.Read())
            {
                var entity = new EntityWithId
                {
                    Id = reader.GetInt32(0),
                    ABigint = reader.IsDBNull(1) ? (long?)null : reader.GetInt64(1),
                    AInt = reader.GetInt32(2),
                    ANumeric = reader.IsDBNull(3) ? (decimal?)null : reader.GetDecimal(3),
                    ABit = reader.IsDBNull(4) ? (bool?)null : reader.GetBoolean(4),
                    ASmallint = reader.IsDBNull(5) ? (short?)null : reader.GetInt16(5),
                    ADecimal = reader.IsDBNull(6) ? (decimal?)null : reader.GetDecimal(6),
                    ASmallmoney = reader.IsDBNull(7) ? (decimal?)null : reader.GetDecimal(7),
                    ATinyint = reader.IsDBNull(8) ? (byte?)null : reader.GetByte(8),
                    AMoney = reader.IsDBNull(9) ? (decimal?)null : reader.GetDecimal(9),
                };
                list.Add(entity);
            }
            return list;
        }

        public List<EntityWithId> Get(string query, 
            SqlParameter[] parms, 
            SqlConnection conn, 
            SqlTransaction trans)
        {
            return CiHelper.ExecuteSelect(query, parms, ReadEntities, conn, trans);
        }

        #region EntityDataReader
        internal class EntityDataReader : BaseDataReader
        {
            private readonly List<EntityWithId> entities;

            public EntityDataReader(List<EntityWithId> entities) : base(entities.Count)
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
                        return entities[current].ABigint;
                    case 2:
                        return entities[current].AInt;
                    case 3:
                        return entities[current].ANumeric;
                    case 4:
                        return entities[current].ABit;
                    case 5:
                        return entities[current].ASmallint;
                    case 6:
                        return entities[current].ADecimal;
                    case 7:
                        return entities[current].ASmallmoney;
                    case 8:
                        return entities[current].ATinyint;
                    case 9:
                        return entities[current].AMoney;
                    default:
                        throw new Exception("EntityWithId Can't read field " + i);
                }
            }

            public override int FieldCount { get { return 10; } }
        }
        #endregion

        public static int MaxAmountForWhereIn = 300;

        public List<EntityWithId> GetByPrimaryKey(object ids, SqlConnection conn, SqlTransaction trans)
        {
            var idsArray = (int[])ids;
            return idsArray.Length > MaxAmountForWhereIn
                ? GetByTempTable(idsArray, conn, trans)
                : GetByWhereIn(idsArray, conn, trans);
        }

        #region getByPrimaryKey internal methods
        private List<EntityWithId> GetByWhereIn(int[] idsArray, SqlConnection conn, SqlTransaction trans)
        {
            var whereIn = string.Join(", ", idsArray.Select((x,i) => "@arg" + i));
            var parms = idsArray.Select((x, i) => new SqlParameter("@arg" + i, x)).ToArray();
            var sql = @"select
                id, a_bigint, a_int, a_numeric, a_bit,
                a_smallint, a_decimal, a_smallmoney, a_tinyint, a_money
            from entity_with_id where id in (" + whereIn + ")";
            return CiHelper.ExecuteSelect(sql, parms, ReadEntities, conn, trans);
        }

        private List<EntityWithId> GetByTempTable(int[] idsArray, SqlConnection conn, SqlTransaction trans)
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
                var result = CiHelper.ExecuteSelect(sql, CiHelper.NoParameters, ReadEntities, conn, trans);
                CiHelper.DropTable(table, conn, trans);
                return result;
        }

        private void CreateIdTempTable(string table, SqlConnection conn, SqlTransaction trans)
        {
            var sql = "CREATE TABLE " + table + " ( id int )";
            CiHelper.ExecuteNonQuery(sql, CiHelper.NoParameters, conn, trans);        }
        #endregion

        public void Insert(List<EntityWithId> entities, SqlConnection conn, SqlTransaction trans)
        {        
            foreach(var group in entities.SplitInGroupsBy(83))
            {
                GroupInsert(group, conn, trans);
            }
           
        }

        #region range insert methods
        private void GroupInsert(List<EntityWithId> entities, SqlConnection conn, SqlTransaction trans)
        {
            int i = 0;
            var parms = entities.SelectMany(x => GetInsertParameters(x, i++)).ToArray();
            var sql = ConstructInsertRequest(entities.Count);
            CiHelper.ExecuteSelect(sql, parms, reader => ReadKey(reader, entities), conn, trans);
        }

        private List<EntityWithId> ReadKey(SqlDataReader reader, List<EntityWithId> entities)
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

        private string ConstructInsertRequest(int count)
        {
            if(insertCacheLength == count) return insertRequestCache;

            var sb = new StringBuilder();
            sb.AppendLine("INSERT INTO entity_with_id");
            sb.AppendLine("(");
            sb.AppendLine("a_bigint, a_int, a_numeric, a_bit, a_smallint,");
            sb.AppendLine("a_decimal, a_smallmoney, a_tinyint, a_money");
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
                sb.Append("( @parm0i"); sb.Append(i);
                sb.Append(", @parm1i"); sb.Append(i);
                sb.Append(", @parm2i"); sb.Append(i);
                sb.Append(", @parm3i"); sb.Append(i);
                sb.Append(", @parm4i"); sb.Append(i);
                sb.Append(", @parm5i"); sb.Append(i);
                sb.Append(", @parm6i"); sb.Append(i);
                sb.Append(", @parm7i"); sb.Append(i);
                sb.Append(", @parm8i"); sb.Append(i);
        }

        private IEnumerable<SqlParameter> GetInsertParameters(EntityWithId entity, int i)
        {
            yield return new SqlParameter("parm0i" + i, SqlDbType.BigInt)
                { Value = entity.ABigint ?? (object)DBNull.Value };
            yield return new SqlParameter("parm1i" + i, SqlDbType.Int)
                { Value = entity.AInt };
            yield return new SqlParameter("parm2i" + i, SqlDbType.Decimal)
                { Value = entity.ANumeric ?? (object)DBNull.Value };
            yield return new SqlParameter("parm3i" + i, SqlDbType.Bit)
                { Value = entity.ABit ?? (object)DBNull.Value };
            yield return new SqlParameter("parm4i" + i, SqlDbType.SmallInt)
                { Value = entity.ASmallint ?? (object)DBNull.Value };
            yield return new SqlParameter("parm5i" + i, SqlDbType.Decimal)
                { Value = entity.ADecimal ?? (object)DBNull.Value };
            yield return new SqlParameter("parm6i" + i, SqlDbType.SmallMoney)
                { Value = entity.ASmallmoney ?? (object)DBNull.Value };
            yield return new SqlParameter("parm7i" + i, SqlDbType.TinyInt)
                { Value = entity.ATinyint ?? (object)DBNull.Value };
            yield return new SqlParameter("parm8i" + i, SqlDbType.Money)
                { Value = entity.AMoney ?? (object)DBNull.Value };
        }
        #endregion

        public void Insert(EntityWithId entity, SqlConnection conn, SqlTransaction trans)
        {        
            var sql = ConstructInsertRequest(1);    
            var parms = GetInsertParameters(entity, 0).ToArray();
            Func<IDataReader, List<EntityWithId>> readId = reader =>
                {
                    if(reader.Read()) entity.Id = reader.GetInt32(0);
                    return null;
                };
            CiHelper.ExecuteSelect(sql, parms, readId, conn, trans);
        }

        public void Update(EntityWithId entity, SqlConnection conn, SqlTransaction trans)
        {
            var parms = GetUpdateParameters(entity, 0).ToArray();
            var sql = GetUpdateRequest(0);
            CiHelper.ExecuteNonQuery(sql, parms, conn, trans);
        }

        public static int MaxAmountForGroupedUpdate = 40;

        public void Update(List<EntityWithId> entities, SqlConnection conn, SqlTransaction trans)
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

        #region update members
        private void BulkUpdate(List<EntityWithId> entities, SqlConnection conn, SqlTransaction trans)
        {
            var table = CiHelper.CreateTempTableName();
            CreateTempTable(table, conn, trans);
            CiHelper.BulkInsert(new EntityDataReader(entities), table, conn, trans);
            var sql = @"UPDATE entity_with_id SET
    a_bigint = s.a_bigint,
    a_int = s.a_int,
    a_numeric = s.a_numeric,
    a_bit = s.a_bit,
    a_smallint = s.a_smallint,
    a_decimal = s.a_decimal,
    a_smallmoney = s.a_smallmoney,
    a_tinyint = s.a_tinyint,
    a_money = s.a_money
  FROM entity_with_id src
  INNER JOIN " + table + @" s 
    ON src.id = s.id
";
            CiHelper.ExecuteNonQuery(sql, new SqlParameter[0], conn, trans);
            CiHelper.DropTable(table, conn, trans);
        }
        
        private void CreateTempTable(string table, SqlConnection conn, SqlTransaction trans)
        {
            var sql = "CREATE TABLE " + table + @"(
                id int,
                a_bigint bigint,
                a_int int,
                a_numeric numeric(6, 2),
                a_bit bit,
                a_smallint smallint,
                a_decimal decimal(8, 3),
                a_smallmoney smallmoney,
                a_tinyint tinyint,
                a_money money
                )";
            CiHelper.ExecuteNonQuery(sql, CiHelper.NoParameters, conn, trans);
        }

        private void GroupUpdate(List<EntityWithId> entities, SqlConnection conn, SqlTransaction trans)
        {
            int i = 0;
            var parms = entities.SelectMany(x => GetUpdateParameters(x, i++)).ToArray();
            var sql = ConstructUpdateRequest(entities.Count);
            CiHelper.ExecuteNonQuery(sql, parms, conn, trans);
        }

        private IEnumerable<SqlParameter> GetUpdateParameters(EntityWithId entity, int i)
        {
            yield return new SqlParameter("parm0i" + i, SqlDbType.BigInt)
                { Value = entity.ABigint ?? (object)DBNull.Value };
            yield return new SqlParameter("parm1i" + i, SqlDbType.Int)
                { Value = entity.AInt };
            yield return new SqlParameter("parm2i" + i, SqlDbType.Decimal)
                { Value = entity.ANumeric ?? (object)DBNull.Value };
            yield return new SqlParameter("parm3i" + i, SqlDbType.Bit)
                { Value = entity.ABit ?? (object)DBNull.Value };
            yield return new SqlParameter("parm4i" + i, SqlDbType.SmallInt)
                { Value = entity.ASmallint ?? (object)DBNull.Value };
            yield return new SqlParameter("parm5i" + i, SqlDbType.Decimal)
                { Value = entity.ADecimal ?? (object)DBNull.Value };
            yield return new SqlParameter("parm6i" + i, SqlDbType.SmallMoney)
                { Value = entity.ASmallmoney ?? (object)DBNull.Value };
            yield return new SqlParameter("parm7i" + i, SqlDbType.TinyInt)
                { Value = entity.ATinyint ?? (object)DBNull.Value };
            yield return new SqlParameter("parm8i" + i, SqlDbType.Money)
                { Value = entity.AMoney ?? (object)DBNull.Value };
            yield return new SqlParameter("parm9i" + i, SqlDbType.Int)
                { Value = entity.Id };
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
            return @"UPDATE entity_with_id SET
    a_bigint = @parm0i" + index + @",
    a_int = @parm1i" + index + @",
    a_numeric = @parm2i" + index + @",
    a_bit = @parm3i" + index + @",
    a_smallint = @parm4i" + index + @",
    a_decimal = @parm5i" + index + @",
    a_smallmoney = @parm6i" + index + @",
    a_tinyint = @parm7i" + index + @",
    a_money = @parm8i" + index + @"
  WHERE id = @parm9i" + index + ";";
        }
        #endregion
    }
}
