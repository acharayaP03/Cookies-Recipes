namespace CookBook.Recipes.Ingredients
{
    public class Chocolate : Ingredient
    {
        public override int Id => 4;

        public override string Name => "Chocolate";

        public override string PreparationInstrunctions => $"Melt in a water bath. {base.PreparationInstrunctions}";

    }


}

