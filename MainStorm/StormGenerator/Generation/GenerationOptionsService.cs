﻿namespace StormGenerator.Generation
{
    using StormGenerator.Settings;

    internal class GenerationOptionsService
    {
        private readonly Options options;
        private GenOptions genOptions;

        public GenerationOptionsService(Options options)
        {
            this.options = options;
        }

        public GenOptions GenOptions => 
            genOptions ?? (genOptions = GetGenerationOptions());

        private GenOptions GetGenerationOptions()
        {
            var opt =  options.GenOptions ?? new GenOptions();
            opt.ContextName = opt.ContextName ?? "StormContext";
            opt.OutputNamespace = opt.OutputNamespace ?? "Storm";
            opt.Visibility = opt.Visibility ?? "public";
            if (opt.MaxInsertItems == 0) opt.MaxInsertItems = 150;
            if (opt.MaxSqlParms == 0) opt.MaxSqlParms = 750;
            return opt;
        }
    }
}
