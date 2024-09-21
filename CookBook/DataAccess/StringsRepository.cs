namespace CookBook.DataAccess;

public abstract class StringsRepository : IStringsRepository
{

    protected abstract List<string> TextToStrings(string fileContents);
    protected abstract string? StringsToText(List<string> allRecipes);

    public List<string> Read(string filePath)
    {

        if (File.Exists(filePath))
        {
            var fileContents = File.ReadAllText(filePath);
            return TextToStrings(fileContents);
        }
        return new List<string>();
    }


    public void Write(string filePath, List<string> allRecipes)
    {
        File.WriteAllText(filePath, StringsToText(allRecipes));
    }
}
