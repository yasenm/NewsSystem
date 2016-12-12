using System.Web;

namespace NewsSystem.Common.Extensions
{
    public static class StringExtensions
    {
        public static string PadCenter(this string s, int width, char c)
        {
            if (s == null || width <= s.Length) return s;

            int padding = width - s.Length;
            return s.PadLeft(s.Length + padding / 2, c).PadRight(width, c);
        }

        public static HtmlString ToHTMLString(this string s)
        {
            return new HtmlString(s.Replace(" ", "&nbsp;"));
        }
    }
}
