namespace StormGenerator.Generation.Generators
{
    using StormGenerator.Infrastructure;
    using StormGenerator.Settings;

    internal class RazorResolve
    {
        private readonly IResolve resolve;
        private readonly Options options;

        public RazorResolve(IResolve resolve, Options options)
        {
            this.resolve = resolve;
            this.options = options;
        }

        public class Parametrized<TParam>
        {
            private readonly TParam param;
            private readonly IResolve resolve;
            private readonly RazorResolve razorResolve;
            private readonly Options options;

            public Parametrized(TParam param,
                IResolve resolve,
                RazorResolve razorResolve,
                Options options)
            {
                this.param = param;
                this.resolve = resolve;
                this.razorResolve = razorResolve;
                this.options = options;
            }

            private TRazor Execute<TRazor>()
                where TRazor : TemplateBase<TParam>
            {
                var razor = resolve.Get<TRazor>();
                razor.Model = param;
                razor.Resolve = razorResolve;
                razor.Options = options.GenerationOptions;
                razor.Execute();
                return razor;
            }

            public string Run<TRazor>()
                where TRazor : TemplateBase<TParam>
            {
                return Execute<TRazor>()
                    .ToString();
            }

            public GeneratedFile CreateFile<TRazor>()
                where TRazor : TemplateBase<TParam>
            {

                var razor = Execute<TRazor>();
                return new GeneratedFile
                       {
                           Name = razor.FileName,
                           Content = razor.ToString()
                       };
            }
        }

        public Parametrized<TParam> With<TParam>(TParam param)
        {
            return new Parametrized<TParam>(param, resolve, this, options);
        }
    }
}
