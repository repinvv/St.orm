namespace Storm
{
    using System.Collections.Generic;

    public static class HashCodesExtension
    {
        public static int CombineHashcodes(this IEnumerable<int> hashcodes)
        {
            int hash = 17;
            foreach (var hashcode in hashcodes)
            {
                hash = hash * 31 + hashcode;
            }

            return hash;
        }
    }
}
