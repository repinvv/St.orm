namespace BasicFeaturesTest.Tests.Helpers
{
    using System;
    using BasicFeaturesTest.StormModel;

    internal class CommentHelper
    {
        private int i = 0;

        public Comment CreateComment()
        {
            return new Comment { CommentText = "comment" + ++i, Created = DateTime.Now, Updated = DateTime.Now };
        }
    }
}
