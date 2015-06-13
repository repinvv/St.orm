namespace StormGenerator.Generation
{
    using System;
    using System.Collections.Generic;
    using StormGenerator.Models.Pregen;

    internal class ModelIterator
    {
        public void ForAllModels(List<Model> models, Action<Model, Model> action)
        {
            foreach (var model in models)
            {
                action(model, model);
                ForAllChildren(model, action);
            }
        }

        private void ForAllChildren(Model model, Action<Model, Model> action)
        {
            foreach (var childModel in model.ChildModels)
            {
                action(childModel, model);
                ForAllChildren(childModel, action);
            }
        }
    }
}
