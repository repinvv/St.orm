namespace BasicFeaturesTest.StormModel
{
    using System;

    public interface IDbEntity
    {
        DateTime Created { get; set; }

        DateTime Updated { get; set; }
    }
}
