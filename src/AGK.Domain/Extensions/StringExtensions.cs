namespace AGK.Domain.Extensions
{
	public static class StringExtensions
    {
        public static string GetUntilOrEmpty(this string text, string stopAt = "_")
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text[0..charLocation];
                }
            }

            return string.Empty;
        }

        public static string GetLastUntilOrEmpty(this string text, string stopAt = "_")
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.LastIndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text[(charLocation + 1)..];
                }

                return text;
            }

            return string.Empty;
        }

        public static string CapitalizeFirstLetter(this string str)
        {
            if (str.Length == 1)
                return char.ToUpper(str[0]).ToString();
            else
                return char.ToUpper(str[0]) + str[1..];
        }
    }
}