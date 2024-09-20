namespace CookBook.Recipes.Ingredients
{
    public abstract class Flour : Ingredient
    {
        public override string PreparationInstrunctions => $"Sieve. {base.PreparationInstrunctions}";
    }


}

