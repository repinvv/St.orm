namespace StormTestProject.Tests.Helpers
{
    using System;
    using StormTestProject.StormModel;

    internal class CommentHelper
    {
        private int i = 0;

        public Comment CreateComment()
        {
            return new Comment { CommentText = "comment" + ++i, Created = DateTime.Now, Updated = DateTime.Now };
        }
    }
}
