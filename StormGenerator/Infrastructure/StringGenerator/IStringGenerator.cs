namespace StormGenerator.Infrastructure.StringGenerator
{
    using System;
    using System.Collections.Generic;

    public interface IStringGenerator
    {
        void PushIndent(int amount = 1);

        void PopIndent(int amount = 1);

        void Braces(Action action, bool semicolon = false);

        void AppendLine(string line);

        void AppendLine();

        void AppendLinesIndented(List<string> lines);

        void AppendLines(List<string> lines);
    }
}