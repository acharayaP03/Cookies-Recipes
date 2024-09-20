using System.ComponentModel;
using System.Runtime.CompilerServices;

var cookiesRecipesApp = new CookiesRecipesApp(
    new RecipesRepository(),
    new RecipesUserInteraction()
);

cookiesRecipesApp.Run();


public class CookiesRecipesApp
{

    private readonly IRecipesRepository _recipesRepository;
    private readonly IRecipesUserInteraction _recipesUserInteraction;

    public CookiesRecipesApp(IRecipesRepository recipesRepository, IRecipesUserInteraction recipesUserInteraction)
    {
        _recipesRepository = recipesRepository;
        _recipesUserInteraction = recipesUserInteraction;
    }
    public void Run()
    {
        var allRecipes = _recipesRepository.Read(filePath);
        _recipesUserInteraction.PrintExistingRecipes(allRecipes);
        _recipesUserInteraction.PromptToCreateRecipe();

        var ingredients = _recipesUserInteraction.ReadIngredientsFromUser();

        if (ingredients.Count > 0)
        {
            var recipes = new Recipe(ingredients);
            allRecipes.Add(recipes);

            _recipesRepository.Write(filePath, allRecipes);

            _recipesUserInteraction.ShowMessage("Recipe added:");
            _recipesUserInteraction.ShowMessage(recipes.ToString());
        }
        else
        {
            _recipesUserInteraction.ShowMessage("No ingredients have been selected. " + "Recipe will not be saved");
        }


        // Lastly once eveything is done, exit app
        _recipesUserInteraction.Exit();

    }
}


public interface IRecipesUserInteraction
{
    void Exit();
    void ShowMessage(string message);
}

public interface IRecipesRepository
{
}

public class RecipesUserInteraction : IRecipesUserInteraction
{
    public void Exit()
    {
        Console.WriteLine("Please press any key to close...");
        Console.ReadKey();
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
}
//{
//    public void Exit()
//    {
//        Console.WriteLine("Please press any key to close...");
//        Console.ReadKey();
//    }

//    internal void PrintExistingRecipes(object allRecipes)
//    {
//        throw new NotImplementedException();
//    }

//    internal void PromptToCreateRecipe()
//    {
//        throw new NotImplementedException();
//    }

//    internal RecipesRepository ReadIngredientsFromUser()
//    {
//        throw new NotImplementedException();
//    }

//    public void ShowMessage(string message)
//    {
//        Console.WriteLine(message);
//    }
//}

public class RecipesRepository : IRecipesRepository
{
    //internal object Read(object filePath)
    //{
    //    throw new NotImplementedException();
    //}

    //internal void Write(CallerFilePathAttribute callerFilePathAttribute, object allRecipes)
    //{
    //    throw new NotImplementedException();
    //}
}