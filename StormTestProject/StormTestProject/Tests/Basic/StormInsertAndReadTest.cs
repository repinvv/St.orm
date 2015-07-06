﻿namespace StormTestProject.Tests.Basic
{
    using System.Linq;
    using StormTestProject.StormModel;
    using StormTestProject.Tests.Helpers;

    internal class StormInsertAndReadTest
    {
        private readonly StormTestContext context;
        private readonly PolicyHelper policyHelper;

        public StormInsertAndReadTest(StormTestContext context, PolicyHelper policyHelper)
        {
            this.context = context;
            this.policyHelper = policyHelper;
        }

        public void Storm_InsertsAndReadsModels()
        {
            using (context.Database.BeginTransaction())
            {
                var policy = policyHelper.CreatePolicyWith5ChildsEach();
                context.Storm.Save(policy);

                var result = context.Storm.GetById<Policy>(policy.PolicyId);
                var a = result.Assignments;
                var t = result.Taxes;
                var c = result.Comments;
            }
        }
    }
}