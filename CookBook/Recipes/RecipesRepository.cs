﻿using CookBook.DataAccess;
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

        var recipesAsStrings = new List<string>();

        foreach (var recipe in allRecipes)
        {
            var allIds = new List<int>();

            foreach (var ingredient in recipe.Ingredients)
            {
                allIds.Add(ingredient.Id);
            }
            recipesAsStrings.Add(string.Join(Seperator, allIds));
        }
        _stringsRepository.Write(filePath, recipesAsStrings);
    }
}

