using BarWebApp2.Models;

namespace BarWebApp2.ViewModels
{
	public class RecipeViewModel
	{
		public List<Recipe> Recipes {get; set;}
		public List<Ingredient> Ingredients { get; set;}
		public List<Instruction> Instructions { get; set;}

	}
}
