﻿namespace StormGenerator.Models.Config.Db
{
    using System.Collections.Generic;

    public class DbAssociation
    {
        public DbModel Dependent { get; set; }

        public List<DbField> ReferenceFields { get; set; }
    }
}