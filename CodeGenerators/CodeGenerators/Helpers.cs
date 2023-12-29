namespace CodeGenerators;

public static class Helpers
{
    private static string CapitalizeFirstLetter(string text) => string.IsNullOrWhiteSpace(text) ?
        string.Empty : text[..1].ToUpper() + text[1..];
    
    public static string SanitizeName(string text)
    {
        var onlyLetters = new string(text.Where(char.IsLetter).ToArray());
        return CapitalizeFirstLetter(onlyLetters);
    }
}