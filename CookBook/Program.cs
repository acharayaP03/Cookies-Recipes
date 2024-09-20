using CookBook.Recipes;
using CookBook.Recipes.Ingredients;
using System.ComponentModel;
using System.Runtime.CompilerServices;

var cookiesRecipesApp = new CookiesRecipesApp(
    new RecipesRepository(),
    new RecipeConsoleUserInteraction(new IngredientsRegister())
);

cookiesRecipesApp.Run("recipes.txt");


public class CookiesRecipesApp
{

    private readonly IRecipesRepository _recipesRepository;
    private readonly IRecipesUserInteraction _recipesUserInteraction;

    public CookiesRecipesApp(IRecipesRepository recipesRepository, IRecipesUserInteraction recipesUserInteraction)
    {
        _recipesRepository = recipesRepository;
        _recipesUserInteraction = recipesUserInteraction;
    }
    public void Run(string filePath)
    {
        var allRecipes = _recipesRepository.Read(filePath);
        _recipesUserInteraction.PrintExistingRecipes(allRecipes);
        _recipesUserInteraction.PromptToCreateRecipe();

        //var ingredients = _recipesUserInteraction.ReadIngredientsFromUser();

        //if (ingredients.Count > 0)
        //{
        //    var recipes = new Recipe(ingredients);
        //    allRecipes.Add(recipes);

        //    _recipesRepository.Write(filePath, allRecipes);

        //    _recipesUserInteraction.ShowMessage("Recipe added:");
        //    _recipesUserInteraction.ShowMessage(recipes.ToString());
        //}
        //else
        //{
        //    _recipesUserInteraction.ShowMessage("No ingredients have been selected. " + "Recipe will not be saved");
        //}


        // Lastly once eveything is done, exit app
        _recipesUserInteraction.Exit();

    }
}


public interface IRecipesUserInteraction
{
    void Exit();
    void PrintExistingRecipes(IEnumerable<Recipe> allRecipes);
    void PromptToCreateRecipe();
    void ShowMessage(string message);
}

public interface IRecipesRepository
{
    List<Recipe> Read(string filePath);
}


public class IngredientsRegister
{ 
    public IEnumerable<Ingredient> All {  get; } = new List<Ingredient>
    {
        new WheatFlour(),
        new SpeltFlour(),
        new Butter(),
        new Sugar(),
        new CocoaPowder(),
        new Cinnamon()
    };
}

public class RecipeConsoleUserInteraction : IRecipesUserInteraction
{

    private readonly IngredientsRegister _ingredientsRegister;


    public RecipeConsoleUserInteraction(IngredientsRegister ingredientsRegister)
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
        if(allRecipes.Count() > 0)
        {
            Console.WriteLine("Existing recipes are:" + Environment.NewLine);
            var counter = 1;
            foreach( var recipe in allRecipes)
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

    public void PrintExistingRecipes()
    {
        //Console.WriteLine("Create a new cookie recipe!. " + "Available ingredients are: ");

        //foreach(var ingredient in _ingredientsRegister.All)
        //{
        //    Console.WriteLine(ingredient);
        //}
    }

   public void PromptToCreateRecipe()
    {
        Console.WriteLine("Create a new cookie recipe!. " + "Available ingredients are: ");

        foreach (var ingredient in _ingredientsRegister.All)
        {
            Console.WriteLine(ingredient);
        }
    }

}


public class RecipesRepository : IRecipesRepository
{
    public List<Recipe> Read(string filePath)
    {

        return new List<Recipe>
        {
            new Recipe(new List<Ingredient>
                {
                    new WheatFlour(),
                    new Butter(),
                    new Sugar(),
                }),
            new Recipe(new List<Ingredient>
            {
                new CocoaPowder(),
                new SpeltFlour(),
                new Cinnamon()
            })


        };
    }
}