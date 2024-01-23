using BarWebApp2.Models;
using BarWebApp2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BarWebApp2.Pages
{
    public class DisplayRecipesModel : PageModel
    {
        public RecipeViewModel RecipeData { get; set; } 
        public Recipe               _Recipe { get; set; }
        public List<Ingredient>    _Ingredients { get; set; }
        public  List<Instruction>  _Instructions { get; set; }
		public int Id { get; set; }

		private readonly Models.RecipeDbContext _context;


		public DisplayRecipesModel(Models.RecipeDbContext context) 
        { 
           _context = context;   
        }

		public async Task  OnGet(int id)
        {
            Id = id;

            _Recipe =  await  _context.Recipes.FirstOrDefaultAsync(x => x.RecipeId == Id);

            _Ingredients = await _context.Ingredients.Where(x => x.RecipeId == Id).ToListAsync();

			_Instructions = await _context.Instructions.Where(x => x.RecipeId == Id).ToListAsync();

          }
    }
}
