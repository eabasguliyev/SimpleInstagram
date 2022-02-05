namespace Instagram.Web.Extensions
{
    public static class StringExtensions
    {
        public static string ToPluralize(this string context)
        {
            return context + "s";
        }
    }
}
