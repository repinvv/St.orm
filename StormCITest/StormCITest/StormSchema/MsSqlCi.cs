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
    using System.Data.SqlClient;
	using System.Data.Common;
	using System.Collections.Generic;

    using SomeSchema;
    public static class MsSqlCi
    {
        public static List<T> Materialize<T>(string query, 
                               SqlParameter[] parms, 
                               DbConnection conn, 
                               DbTransaction trans = null)
        {
            return GetService<T>().Materialize(query, parms, (SqlConnection)conn, trans as SqlTransaction);
        }

        public static List<T> GetByPrimaryKey<T>(object ids,
                       DbConnection conn,
                       DbTransaction trans = null)
        {
            return GetService<T>().GetByPrimaryKey(ids, (SqlConnection)conn, trans as SqlTransaction);
        }

        private static Dictionary<Type, object> services =
            new Dictionary<Type, object>
            {
                { typeof(EntityWithId), new EntityWithIdCiService() },
                { typeof(EntityWithGuid), new EntityWithGuidCiService() },
                { typeof(EntityWithSequence), new EntityWithSequenceCiService() },
                { typeof(EntityWithMultikey), new EntityWithMultikeyCiService() },
                { typeof(EntityWithoutKey), new EntityWithoutKeyCiService() },
           };

        private static ICiService<T> GetService<T>()
        {
            return services[typeof(T)] as ICiService<T>;
        }
    }
}
