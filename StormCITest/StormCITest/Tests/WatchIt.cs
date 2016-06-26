namespace StormCITest.Tests
{
    using System;
    using System.Diagnostics;

    internal class WatchIt
    {
        public static long Watch(Action action)
        {
            var watch = Stopwatch.StartNew();
            action();
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}
