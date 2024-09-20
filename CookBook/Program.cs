using System.Runtime.CompilerServices;

var cookiesRecipesApp = new CookiesRecipesApp();

cookiesRecipesApp.Run();


public class CookiesRecipesApp()
{

    private readonly RecipesRepository _recipesRepository;
    private readonly RecipesUserInteraction _recipesUserInteraction;


    public CookiesRecipesApp(RecipesRepository recipesRepository, RecipesUserInteraction recipesUserInteraction)
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

        if(ingredients.Count > 0)
        {
            var recipes = new CookiesRecipesApp(ingredients);
            allRecipes.Add(recipes);

            _recipesRepository.Write(CallerFilePathAttribute, allRecipes);

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

internal class RecipesUserInteraction
{
}

internal class RecipesRepository
{
}