﻿namespace StormGenerator.Common
{
    using System;
    using St.Orm;
    using St.Orm.Interfaces;

    internal static class GenerationConstants
    {
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

        public static class AutoModelCreation
        {
            public const int MaximumOneToManyFields = 5;
        }

        public static class ModelGeneration
        {
            public const string RepositorySuffix = "DalRepository";
            public const string ContextExtensionSuffix = "Extension";

            public static readonly Type[] DottedTypes = { typeof(decimal), typeof(double), typeof(float) };

            public static readonly string[] RepositoryUsings =
            {
                "System.Collections.Generic",
                "System.Linq",
                "System.Data",
                typeof(EmptyRepositoryExtension<>).Namespace,
                typeof(IDalRepository<,>).Namespace
            };

            public static readonly string[] ModelClassUsings =
            {
                "System.Collections.Generic",
                "System.Linq",
                typeof(ILoadService).Namespace,
                typeof(HelpfulExtensions).Namespace
            };

            public static readonly string[] EfAttributesUsings =
            {
                "System.ComponentModel.DataAnnotations",
                "System.ComponentModel.DataAnnotations.Schema"
            };

            public static readonly string[] ContextUsings =
            {
                "System.Data.Entity",
                typeof(IStormContext).Namespace
            };
        }
    }
}
