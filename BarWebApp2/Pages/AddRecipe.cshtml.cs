using BarWebApp2.Models;
using BarWebApp2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace BarWebApp2.Pages
{
    public class AddRecipeModel : PageModel
    {
		//public RecipeViewModel RecipeData { get; set; }

		[BindProperty]
		public Recipe Recipe{ get; set; }		

		[BindProperty]
		public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

		//[BindProperty]
		//public Instruction Instruction { get; set; }

		[BindProperty]
		public List<Instruction> Instructions { get; set; } = new List<Instruction>();

		private readonly Models.RecipeDbContext _context;


		public AddRecipeModel(Models.RecipeDbContext context)
		{
			_context = context;
		}


		public void OnGet()
        {
			Recipe = new Recipe();			
			//Ingredient = new Ingredient();			
			//Instruction = new Instruction();			

			Ingredients.Add(new Ingredient()); 
			Instructions.Add(new Instruction());
        }


	
	   
	      public  async   Task<IActionResult> OnPostAsync()
	      { 
			
			if (!ModelState.IsValid)
			{
                foreach (var entry in ModelState)
                {
                    string key = entry.Key;
                    var errors = entry.Value.Errors;

                    // You can inspect and handle errors for each key
                    foreach (var error in errors)
                    {
                        // Access error messages
                        string errorMessage = error.ErrorMessage;

                        // Handle or log the error messages as needed
                        //return Page();

                        TempData["ErrorMessage"] = "The entered information was not saved. There were errors.";

                        // Redirect back to the page
                        return  RedirectToPage();

                    }
                }
            }


			// Add Recipe to the database			 
		   await  _context.Recipes.AddAsync(Recipe);

			try
			{
				await _context.SaveChangesAsync();			    	
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
			


			// Add Ingredient to the database

			var index = 0;
			foreach (var i in Ingredients)
			{
				i.RecipeId = Recipe.RecipeId;
				await _context.Ingredients.AddAsync(i);

				try
				{
					await _context.SaveChangesAsync();					
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					throw;
				}
				index++;
			}

	

            // Add Instructions to the database

            int indexInstruction = 1;
            foreach (var i in Instructions)
            {
				i.StepNumber = indexInstruction;
                i.RecipeId   = Recipe.RecipeId;
                await _context.Instructions.AddAsync(i);

                try
                {
                   await  _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                indexInstruction++;
            }

            return RedirectToPage("/Recipe"); // Redirect to a success page

		}
	}
}
