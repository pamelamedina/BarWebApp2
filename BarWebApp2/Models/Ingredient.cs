using System.ComponentModel.DataAnnotations;

namespace BarWebApp2.Models
{
	public class Ingredient
	{
		[Required]
		public int IngredientId { get; set; }

		[Required]
		public int RecipeId { get; set; }
		            
		[Required]
		public string IngredientName { get; set; }

		[Required]
		public double Quantity { get; set; }

		[Required]
		public string Unit { get; set; }

		public Ingredient() 
		{
			RecipeId = 0;
			IngredientName = "default";
			Quantity = 0;
			Unit = "oz";
		}


	}
}
