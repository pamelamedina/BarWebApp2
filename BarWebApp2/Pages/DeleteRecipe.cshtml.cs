using BarWebApp2.Models;
using BarWebApp2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BarWebApp2.Pages.Shared
{
    public class DeleteRecipeModel : PageModel
    {
		private readonly Models.RecipeDbContext _context;

		public int Id { get; set; }
		
		public RecipeViewModel RecipeData { get; set; }
		public Recipe _Recipe { get; set; }
		public List<Ingredient> _Ingredients { get; set; }
		public List<Instruction> _Instructions { get; set; }


		public DeleteRecipeModel(Models.RecipeDbContext context)
		{
			_context = context;			
		}
	
		public async Task OnGet(int id)
        {			
			Id = id;
			
			_Recipe = await _context.Recipes.FirstOrDefaultAsync(x => x.RecipeId == Id);

			_Ingredients = await _context.Ingredients.Where(x => x.RecipeId == Id).ToListAsync();

			_Instructions = await _context.Instructions.Where(x => x.RecipeId == Id).ToListAsync();

		}

		public IActionResult  OnPostCancel()
		{
			return RedirectToPage("/Recipe"); // Redirect to a Recipe page
		}

       
		public async Task<RedirectToPageResult> OnPostDelete(int id)
		{

				//remove recipe
				var recipeToRemove = await _context.Recipes.FindAsync(id);
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
				if (instructionToRemove != null)
				{
					_context.Instructions.Remove(instructionToRemove);
				}

				await _context.SaveChangesAsync();

				return RedirectToPage("/Recipe"); 

		}		
	}
}
    
