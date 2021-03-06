﻿namespace BasicFeaturesTest.StormModel
{
    using System;

    public static class PersistenceEvents
    {
        public static IDbEntity BeforeInsert(IDbEntity entity)
        {
            entity.Created = DateTime.Now;
            entity.Updated = DateTime.Now;
            return entity;
        }

        public static IDbEntity BeforeUpdate(IDbEntity entity)
        {
            entity.Updated = DateTime.Now;
            return entity;
        }
    }
}
