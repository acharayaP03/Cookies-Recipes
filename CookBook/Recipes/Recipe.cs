﻿using CookBook.Recipes.Ingredients;

namespace CookBook.Recipes;

public class Recipe
{
    public IEnumerable<Ingredient> Ingredients { get; }

    public Recipe(IEnumerable<Ingredient> ingredients )
    {
        Ingredients = ingredients;
    }

    public override string ToString()
    {
        //var steps = new List<string>();

        //foreach (var ingredient in Ingredients)
        //{
        //    steps.Add($"{ingredient.Name}. {ingredient.PreparationInstrunctions}");
        //}

        //return string.Join(Environment.NewLine, steps);

        return string.Join(Environment.NewLine, Ingredients.Select(ingredient => $"{ingredient.Name}. {ingredient.PreparationInstrunctions}"));
    }
}

