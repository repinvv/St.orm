namespace BasicFeaturesTest.Tests.Helpers
{
    using System;
    using BasicFeaturesTest.StormModel;

    internal class AssignmentHelper
    {
        public Assignment CreateAssignment()
        {
            return new Assignment { Created = DateTime.Now, Updated = DateTime.Now };
        }
    }
}
