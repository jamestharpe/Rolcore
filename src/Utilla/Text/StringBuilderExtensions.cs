namespace Utilla.Text
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Text;

    public static class StringBuilderExtensions
    {
        public static void AppendIf(this StringBuilder sb, bool condition, string s)
        {
            Contract.Requires<ArgumentNullException>(sb != null, "sb is null");

            if (condition)
            {
                sb.Append(s);
            }
        }
    }
}
