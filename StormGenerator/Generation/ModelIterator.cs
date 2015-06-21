namespace StormGenerator.Generation
{
    using System;
    using System.Collections.Generic;
    using StormGenerator.Models.Pregen;

    internal class ModelIterator
    {
        public void ForAllModels(List<Model> models, Action<Model> action)
        {
            foreach (var model in models.Active())
            {
                action(model);
                ForAllModels(model.ChildModels, action);
            }
        }
    }
}
