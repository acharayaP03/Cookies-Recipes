namespace CookBook.DataAccess;

public class StringsTextualRepository : StringsRepository
{
    private static readonly string Seperator = Environment.NewLine;

    protected override string? StringsToText(List<string> allRecipes)
    {
        return string.Join(Seperator, allRecipes);
    }

    protected override List<string> TextToStrings(string fileContents)
    {
        return fileContents.Split(Seperator).ToList();
    }
}
