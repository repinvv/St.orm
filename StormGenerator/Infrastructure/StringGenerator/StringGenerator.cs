namespace StormGenerator.Infrastructure.StringGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using StormGenerator.Common;

    internal class StringGenerator : IStringGenerator
    {
        private StringBuilder builder = new StringBuilder();
        private bool appended;
        private bool emptyAppend;
        private int indentCount = 0;

        public void PushIndent(int amount = 1)
        {
            indentCount += amount;
        }

        public void PopIndent(int amount = 1)
        {
            indentCount -= amount;
            if (indentCount < 0)
            {
                throw new Exception("Indent error");
            }
        }

        public void Braces(Action action, bool semicolon = false)
        {
            AppendLine("{");
            PushIndent();
            action();
            PopIndent();
            AppendLine(semicolon ? "};" : "}");
        }

        public void AppendLine(string line)
        {
            var indent = indentCount * GenerationConstants.IndentSize;
            if (indent > 0)
            {
                builder.Append(' ', indent);
            }

            appended = true;
            emptyAppend = line == string.Empty;
            builder.AppendLine(line);
        }

        public void AppendLine()
        {
            appended = true;
            emptyAppend = true;
            builder.AppendLine();
        }

        public void AppendLinesIndented(List<string> lines)
        {
            AppendLine(lines[0]);
            PushIndent();
            for (var n = 1; n < lines.Count; n++)
            {
                AppendLine(lines[n]);
            }

            PopIndent();
        }

        public void AppendLines(List<string> lines)
        {
            foreach (var line in lines)
            {
                AppendLine(line);
            }
        }

        public void Region(string regionName, Action action)
        {
            AppendLine("#region " + regionName);
            AppendLine();
            appended = false;
            action();
            if (appended && !emptyAppend)
            {
                AppendLine();
            }

            AppendLine("#endregion");
        }

        public override string ToString()
        {
            if (indentCount != 0)
            {
                throw new Exception("Indent error");
            }

            var output = builder.ToString();
            builder = new StringBuilder();
            return output;
        }
    }
}