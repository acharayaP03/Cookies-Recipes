

using CookBook.Recipes.Ingredients;
using CookBook.Recipes;

namespace CookBook.App;

public interface IRecipesUserInteraction
{
    void Exit();
    void PrintExistingRecipes(IEnumerable<Recipe> allRecipes);
    void PromptToCreateRecipe();
    IEnumerable<Ingredient> ReadIngredientsFromUser();
    void ShowMessage(string message);
}
