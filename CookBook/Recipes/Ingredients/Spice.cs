namespace CookBook.Recipes.Ingredients;

public abstract class Spice : Ingredient
{
    public override string PreparationInstrunctions => $"Take half a teaspoon. {base.PreparationInstrunctions}";

}

