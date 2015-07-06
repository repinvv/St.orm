namespace StormTestProject.Tests.Helpers
{
    using System;
    using StormTestProject.StormModel;

    internal class AssignmentHelper
    {
        public Assignment CreateAssignment()
        {
            return new Assignment { Created = DateTime.Now, Updated = DateTime.Now };
        }
    }
}
