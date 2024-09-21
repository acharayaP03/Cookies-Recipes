namespace CookBook.Recipes.Ingredients;

public class Butter : Ingredient
{
    public override int Id => 3;

    public override string Name => "Butter";

    public override string PreparationInstrunctions => $"Melt in a water bath. {base.PreparationInstrunctions}";
}

