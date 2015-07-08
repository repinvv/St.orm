namespace StormTestProject.Tests.Helpers
{
    using System;
    using System.Collections.Generic;
    using StormTestProject.StormModel;

    internal class PolicyHelper
    {
        private readonly CurrencyHelper currencyHelper;
        private readonly CountryHelper countryHelper;
        private readonly AssignmentHelper assignmentHelper;
        private readonly TaxHelper taxHelper;
        private readonly CommentHelper commentHelper;

        public PolicyHelper(CurrencyHelper currencyHelper, 
            CountryHelper countryHelper,
            AssignmentHelper assignmentHelper,
            TaxHelper taxHelper,
            CommentHelper commentHelper)
        {
            this.currencyHelper = currencyHelper;
            this.countryHelper = countryHelper;
            this.assignmentHelper = assignmentHelper;
            this.taxHelper = taxHelper;
            this.commentHelper = commentHelper;
        }

        public Policy CreatePolicy()
        {
            return new Policy
                   {
                       Name = "my policy",
                       CountryId = countryHelper.CreateAndSaveCountry().CountryId,
                       CurrencyId = currencyHelper.CreateAndSaveCurrency().CurrencyId,
                       Created = DateTime.Now, 
                       Updated = DateTime.Now,
                   };
        }

        public Policy CreatePolicyWith5ChildsEach()
        {
            var policy = CreatePolicy();
            policy.Assignments =
                new List<Assignment>
                {
                    assignmentHelper.CreateAssignment(),
                    assignmentHelper.CreateAssignment(),
                    assignmentHelper.CreateAssignment(),
                    assignmentHelper.CreateAssignment(),
                    assignmentHelper.CreateAssignment(),
                };
            policy.Taxes =
                new List<Tax>
                {
                    taxHelper.CreateTax(),
                    taxHelper.CreateTax(),
                    taxHelper.CreateTax(),
                    taxHelper.CreateTax(),
                    taxHelper.CreateTax(),
                };
            policy.Comments =
                new List<Comment>
                {
                    commentHelper.CreateComment(),
                    commentHelper.CreateComment(),
                    commentHelper.CreateComment(),
                    commentHelper.CreateComment(),
                    commentHelper.CreateComment(),
                };
            return policy;
        }

        public Policy CreatePolicyWith2Taxes()
        {
            var policy = CreatePolicy();
            policy.Taxes = new List<Tax>
                           {
                               taxHelper.CreateTax(),
                               taxHelper.CreateTax()
                           };
            return policy;
        }
    }
}
