using CookBook.DataAccess;
using CookBook.Recipes.Ingredients;

namespace CookBook.Recipes;

public class RecipesRepository : IRecipesRepository
{

    private readonly IStringsRepository _stringsRepository;
    private readonly IIngredientsRegister _ingredientsRegister;
    private const string Seperator = ",";

    public RecipesRepository(IStringsRepository stringsRepository, IIngredientsRegister ingredientsRegister)
    {
        _stringsRepository = stringsRepository;
        _ingredientsRegister = ingredientsRegister;
    }

    public List<Recipe> Read(string filePath)
    {
        return _stringsRepository.Read(filePath)
            .Select(RecipeFromString)
            .ToList();
    }

    private Recipe RecipeFromString(string recipeFromFile)
    {
        var ingredients = recipeFromFile.Split(Seperator)
            .Select(textualId => int.Parse(textualId))
            .Select(id => _ingredientsRegister.GetById(id))
            .ToList();


        return new Recipe(ingredients);
    }

    public void Write(string filePath, List<Recipe> allRecipes)
    {

        //var recipesAsStrings = allRecipes.Select(recipe => string.Join(Seperator, recipe.Ingredients.Select(ingredient => ingredient.Id)));

        var recipesAsStrings = allRecipes.Select(recipe =>
        {
            var allIds = recipe.Ingredients.Select(ingredient => ingredient.Id);

            return string.Join(Seperator, allIds);
        }).ToList();

        _stringsRepository.Write(filePath, recipesAsStrings);
    }
}

