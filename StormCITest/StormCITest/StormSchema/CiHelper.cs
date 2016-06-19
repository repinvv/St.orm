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
    using System.Data.SqlClient;
	using System.Collections.Generic;
    using System;
    using System.Data;

    public static class CiHelper
    {
        public static int CombineHashcodes(this IEnumerable<int> hashcodes)
        {
            unchecked
            {
                int hash = 17;
                foreach (var hashcode in hashcodes)
                {
                    hash = hash * 31 + hashcode;
                }
			    
                return hash;
            }
        }

        public static byte[] ReadBytes(this SqlDataReader reader, int index, int length)
        {
            var buffer = new byte[length];
            var resultLength = (int)reader.GetBytes(index, 0, buffer, 0, length);
            if (resultLength == 0)
            {
                return null;
            }

            if (resultLength == length)
            {
                return buffer;
            }

            var output = new byte[resultLength];
            Buffer.BlockCopy(buffer, 0, output, 0, resultLength);
            return output;
        }

        public static List<T> ExecuteSelect<T>(string query, 
                                        SqlParameter[] parms, 
                                        Func<SqlDataReader, List<T>> func, 
                                        SqlConnection conn, 
                                        SqlTransaction trans)
        {
			var wasClosed = conn.State != ConnectionState.Open;
			if(wasClosed)
			{
                conn.Open();
            }
            try
            {
                using (var command = new SqlCommand(query, conn))
                {
                    command.Transaction = trans;
                    command.Parameters.AddRange(parms);
                    using (var reader = command.ExecuteReader())
                    {
                        return func(reader);
                    }
                }
            }
			finally 
			{
                if(wasClosed)
                {
                    conn.Close();
                }
            }
        }
    }
}
