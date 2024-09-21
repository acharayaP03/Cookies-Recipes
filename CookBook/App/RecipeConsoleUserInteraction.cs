

using CookBook.Recipes.Ingredients;
using CookBook.Recipes;

namespace CookBook.App;

public class RecipeConsoleUserInteraction : IRecipesUserInteraction
{

    private readonly IIngredientsRegister _ingredientsRegister;


    public RecipeConsoleUserInteraction(IIngredientsRegister ingredientsRegister)
    {
        _ingredientsRegister = ingredientsRegister;
    }
    public void Exit()
    {
        Console.WriteLine("Please press any key to close...");
        Console.ReadKey();
    }

    public void PrintExistingRecipes(IEnumerable<Recipe> allRecipes)
    {
        if (allRecipes.Count() > 0)
        {
            Console.WriteLine("Existing recipes are:" + Environment.NewLine);
            var counter = 1;
            foreach (var recipe in allRecipes)
            {
                //Console.WriteLine($"{recipeIndex + 1}. {allRecipes.ElementAt(recipeIndex)}");
                Console.WriteLine($"*******{counter}***********");
                Console.WriteLine(recipe);
                Console.WriteLine();
                ++counter;
            }
        }

    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }



    public void PromptToCreateRecipe()
    {
        Console.WriteLine("Create a new cookie recipe!. " + "Available ingredients are: ");

        foreach (var ingredient in _ingredientsRegister.All)
        {
            Console.WriteLine(ingredient);
        }
    }

    public IEnumerable<Ingredient> ReadIngredientsFromUser()
    {
        bool shallStop = false;
        var ingredients = new List<Ingredient>();


        while (!shallStop)
        {
            Console.WriteLine("Please enter the ingredient number or type 'exit' to finish: ");
            var userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int id))
            {
                var selectedIngredient = _ingredientsRegister.GetById(id);
                if (selectedIngredient is not null)
                {

                    ingredients.Add(selectedIngredient);
                }
                else
                {
                    Console.WriteLine("Invalid ingredient number. Please try again.");
                }
            }
            else
            {
                shallStop = true;
            }
        }


        return ingredients;
    }
}
