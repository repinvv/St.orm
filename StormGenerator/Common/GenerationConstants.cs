﻿namespace StormGenerator.Common
{
    public static class GenerationConstants
    {
        public const string OracleProvider = "oracle";

        public const string MsSqlProvider = "mssql";

        public const string GenerationMark =
@"//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------";

        public const int IndentSize = 4;

        public const int MaximumAutoOneToManyFields = 4;
    }
}
