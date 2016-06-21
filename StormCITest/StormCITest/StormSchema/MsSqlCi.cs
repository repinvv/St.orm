//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StormTestProject.StormModel
{
    using System;
    using System.Data.SqlClient;
	using System.Collections.Generic;
    using SomeSchema;
    public static class MsSqlCi
    {
        public static List<T> Materialize<T>(string query, 
                               SqlParameter[] parms, 
                               SqlConnection conn, 
                               SqlTransaction trans)
        {
            return GetService<T>().Materialize(query, parms, conn, trans);
        }

        private static Dictionary<Type, object> services =
            new Dictionary<Type, object>
            {
                { typeof(EntityWithId), new EntityWithId() },
                { typeof(EntityWithGuid), new EntityWithGuid() },
                { typeof(EntityWithSequence), new EntityWithSequence() },
                { typeof(EntityWithMultikey), new EntityWithMultikey() },
                { typeof(EntityWithoutKey), new EntityWithoutKey() },
           };

        private static ICiService<T> GetService<T>()
        {
            return services[typeof(T)] as ICiService<T>;
        }
    }
}
