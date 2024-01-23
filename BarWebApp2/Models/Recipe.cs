using System.ComponentModel.DataAnnotations;

namespace BarWebApp2.Models
{
	public class Recipe
	{
		    [Required]			
		    public int RecipeId { get; set; }

	        [Required]
			public string Title { get; set; }

	        [Required]		    
			public string Description { get; set; }


		public Recipe() 
		{ 		   
		   Title = string.Empty;
		   Description = string.Empty;
            
        }

	}
}
