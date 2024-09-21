using CookBook.App;
using CookBook.DataAccess;
using CookBook.FileAccess;
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

