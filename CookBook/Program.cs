using CookBook.Recipes;
using CookBook.Recipes.Ingredients;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;


const FileFormat Format = FileFormat.Json;
var ingredientsRegister = new IngredientsRegister();    

IStringsRepository stringsRepository = Format == FileFormat.Json ? new StringsJsonRepository() : new StringsTextualRepository();

const string FileName = "recipes";
var fileMetada = new FileMetadata(FileName, Format);


var cookiesRecipesApp = new CookiesRecipesApp(
    //new RecipesRepository( new StringsTextualRepository(), ingredientsRegister),
    new RecipesRepository(stringsRepository, ingredientsRegister),

    new RecipeConsoleUserInteraction(ingredientsRegister)
);

cookiesRecipesApp.Run(fileMetada.ToPath());


public class FileMetadata
{
    public string Name { get; set; }
    public FileFormat Format { get; }

    public FileMetadata(string name, FileFormat format)
    {
        Name = name;
        Format = format;
    }


    public string ToPath() => $"{Name}.{Format.AsFileExtension()}";
}


public static class FileFormatExtensions
{
    public static string AsFileExtension(this FileFormat format) => format == FileFormat.Json ? "json" : "txt";
}


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

        var ingredients = _recipesUserInteraction.ReadIngredientsFromUser();

        if (ingredients.Count() > 0)
        {
            var recipe = new Recipe(ingredients);
            allRecipes.Add(recipe);

            _recipesRepository.Write(filePath, allRecipes);

            _recipesUserInteraction.ShowMessage("Recipe added:");
            _recipesUserInteraction.ShowMessage(recipe.ToString());
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
    void PrintExistingRecipes(IEnumerable<Recipe> allRecipes);
    void PromptToCreateRecipe();
    IEnumerable<Ingredient> ReadIngredientsFromUser();
    void ShowMessage(string message);
}

public interface IRecipesRepository
{
    List<Recipe> Read(string filePath);
    void Write(string filePath, List<Recipe> allRecipes);
}

public interface IIngredientsRegister
{
    IEnumerable<Ingredient> All { get; }

    Ingredient GetById(int id);
}

public class IngredientsRegister : IIngredientsRegister
{
    public IEnumerable<Ingredient> All { get; } = new List<Ingredient>
    {
        new WheatFlour(),
        new SpeltFlour(),
        new Butter(),
        new Sugar(),
        new CocoaPowder(),
        new Cinnamon()
    };

    public Ingredient GetById(int id)
    {
        foreach (var ingredient in All)
        {
            if (ingredient.Id == id)
            {
                return ingredient;
            }
        }

        return null;
    }
}

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
        List<string> recipesFromFile = _stringsRepository.Read(filePath);
        var recipes = new List<Recipe>();

        foreach (var recipeFromFile in recipesFromFile)
        {
            var recipe = RecipeFromString(recipeFromFile);
            recipes.Add(recipe);
        }

        return recipes;
    }

    private Recipe RecipeFromString(string recipeFromFile)
    {
        var textualIds = recipeFromFile.Split(Seperator);
        var ingredients = new List<Ingredient>();

        foreach (var textualId in textualIds)
        {
            var id = int.Parse(textualId);
            var ingredient = _ingredientsRegister.GetById(id);
            ingredients.Add(ingredient);
        }

        return new Recipe(ingredients);
    }

    public void Write(string filePath, List<Recipe> allRecipes)
    {

        var recipesAsStrings = new List<string>();

        foreach(var recipe in allRecipes)
        {
            var allIds = new List<int>();

            foreach(var ingredient in recipe.Ingredients)
            {
                allIds.Add(ingredient.Id);
            }
            recipesAsStrings.Add(string.Join(Seperator, allIds));
        }
        _stringsRepository.Write(filePath, recipesAsStrings);
    }
}

public interface IStringsRepository
{
    List<string> Read(string filePath);
    void Write(string filePath, List<string> allRecipes);
}

public enum FileFormat
{
    Text,
    Json,
    CSV
}

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

