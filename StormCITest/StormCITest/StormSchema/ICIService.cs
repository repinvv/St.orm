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
    using System.Data.SqlClient;
    using System.Collections.Generic;

    public interface ICiService<T>
    {
        List<T> Get(string query, SqlParameter[] parms, SqlConnection conn, SqlTransaction trans);

        List<T> GetByPrimaryKey(object ids, SqlConnection conn, SqlTransaction trans);

        void Insert(List<T> entities, SqlConnection conn, SqlTransaction trans);

        void Insert(T entity, SqlConnection conn, SqlTransaction trans);

        void Update(T entity, SqlConnection conn, SqlTransaction trans);

        void Update(List<T> entities, SqlConnection conn, SqlTransaction trans);

        void Delete(T entity, SqlConnection conn, SqlTransaction trans);

        void Delete(List<T> entities, SqlConnection conn, SqlTransaction trans);

        void DeleteByPrimaryKey(object ids, SqlConnection conn, SqlTransaction trans);
    }
}
