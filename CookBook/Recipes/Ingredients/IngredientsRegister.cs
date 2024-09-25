

namespace CookBook.Recipes.Ingredients;

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

        var alIngredientsWithGivenId = All.Where(ingredient => ingredient.Id == id);

        if(alIngredientsWithGivenId.Count() > 1)
        {
            throw new InvalidOperationException($"There are more than one ingredient with id {id}");
        }

        //if(All.Select(ingredient => ingredient.Id).Distinct().Count() != All.Count())
        //{
        //    throw new InvalidOperationException($"Tsome ingredients have duplicate id.");
        //}

        return alIngredientsWithGivenId.FirstOrDefault();
    }
}

