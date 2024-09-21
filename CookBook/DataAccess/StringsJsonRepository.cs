

using System.Text.Json;

namespace CookBook.DataAccess;

public class StringsJsonRepository : StringsRepository
{

    protected override string? StringsToText(List<string> allRecipes)
    {
        return JsonSerializer.Serialize(allRecipes);
    }

    protected override List<string> TextToStrings(string fileContents)
    {
        return JsonSerializer.Deserialize<List<string>>(fileContents);
    }
}
