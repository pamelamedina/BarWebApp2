using BarWebApp2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BarWebApp2.Pages
{
	public class RecipeModel : PageModel
	{

		private readonly Models.RecipeDbContext _context;

		public List<Recipe> Recipes { get; set; }


		public RecipeModel(Models.RecipeDbContext context)
		{
			_context = context;
		}

		public  async  Task  OnGetAsync()
		{			
			 Recipes = await _context.Recipes.ToListAsync();
		}


		public  async Task<RedirectToPageResult>  OnPostdeleteRecipe(int id)
		//public  async Task<RedirectToPageResult>  OnPostClickdeleteRecipe(int id)
		{
			
			//remove recipe
			var recipeToRemove =  await  _context.Recipes.FindAsync(id);
			if (recipeToRemove != null)
			{
				  _context.Recipes.Remove(recipeToRemove);
			}

			 await _context.SaveChangesAsync();


			//remove ingredients
			var ingredientsToDelete = await _context.Ingredients
										.Where(x => x.RecipeId == id).ToListAsync();
						

			foreach (var i in ingredientsToDelete)
			{
				 _context.Ingredients.Remove(i);
			}
			
			await _context.SaveChangesAsync();



			//remove instruction
			var instructionToRemove = await _context.Instructions.FindAsync(id);
			if (recipeToRemove != null)
			{
				_context.Instructions.Remove(instructionToRemove);
			}

			await _context.SaveChangesAsync();

			return RedirectToPage("/Recipe"); // Redirect to a success page

		}
	}
}
